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
        var status = page.GetByTestId("status");

        await page.GetByTestId("trip-name").FillAsync("Goa Trip");
        await page.GetByTestId("create-trip-btn").ClickAsync();
        await WaitForContainsAsync(status, "Trip created:");

        await page.GetByTestId("member-name").FillAsync("Alice");
        await page.GetByTestId("add-member-btn").ClickAsync();
        await WaitForContainsAsync(status, "Member added: Alice");

        await page.GetByTestId("member-name").FillAsync("Bob");
        await page.GetByTestId("add-member-btn").ClickAsync();
        await WaitForContainsAsync(status, "Member added: Bob");

        await page.GetByTestId("member-name").FillAsync("Charlie");
        await page.GetByTestId("add-member-btn").ClickAsync();
        await WaitForContainsAsync(status, "Member added: Charlie");

        await page.GetByTestId("expense-description").FillAsync("Dinner");
        await page.GetByTestId("paid-by").SelectOptionAsync(new[] { new SelectOptionValue { Label = "Alice" } });
        await page.GetByTestId("amount").FillAsync("90");
        await page.GetByTestId("add-expense-btn").ClickAsync();
        await WaitForContainsAsync(status, "Expense added and summary updated.");

        var settlements = page.GetByTestId("settlements-list");
        await WaitForContainsAsync(settlements, "Bob pays Alice: 30.00");
        await WaitForContainsAsync(settlements, "Charlie pays Alice: 30.00");
    }

    private static async Task WaitForContainsAsync(ILocator locator, string expected, int timeoutMs = 20_000)
    {
        var start = DateTime.UtcNow;

        while ((DateTime.UtcNow - start).TotalMilliseconds < timeoutMs)
        {
            var text = await locator.InnerTextAsync();
            if (text.Contains(expected, StringComparison.Ordinal))
            {
                return;
            }

            await Task.Delay(200);
        }

        var final = await locator.InnerTextAsync();
        Assert.Contains(expected, final);
    }
}
