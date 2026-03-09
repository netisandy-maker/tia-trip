using Microsoft.Playwright;
using Xunit;

namespace TiaTrip.E2E.Dotnet;

public class HomeTests
{
    [Fact]
    public async Task ExpenseFlowShowsExpectedSettlement()
    {
        var baseUrl = Environment.GetEnvironmentVariable("BASE_URL") ?? "http://127.0.0.1:4173";

        using var playwright = await Playwright.CreateAsync();
        await using var browser = await playwright.Chromium.LaunchAsync(new BrowserTypeLaunchOptions
        {
            Headless = true
        });

        var page = await browser.NewPageAsync();
        await page.GotoAsync(baseUrl);

        await page.GetByTestId("trip-name").FillAsync("Goa Trip");
        await page.GetByTestId("create-trip-btn").ClickAsync();

        await page.GetByTestId("member-name").FillAsync("Alice");
        await page.GetByTestId("add-member-btn").ClickAsync();

        await page.GetByTestId("member-name").FillAsync("Bob");
        await page.GetByTestId("add-member-btn").ClickAsync();

        await page.GetByTestId("member-name").FillAsync("Charlie");
        await page.GetByTestId("add-member-btn").ClickAsync();

        await page.GetByTestId("expense-description").FillAsync("Dinner");
        await page.GetByTestId("paid-by").SelectOptionAsync(new[] { new SelectOptionValue { Label = "Alice" } });
        await page.GetByTestId("amount").FillAsync("90");
        await page.GetByTestId("add-expense-btn").ClickAsync();

        var settlements = page.GetByTestId("settlements-list");
        Assert.Contains("Bob pays Alice: 30.00", await settlements.InnerTextAsync());
        Assert.Contains("Charlie pays Alice: 30.00", await settlements.InnerTextAsync());
    }
}
