using System.Collections.Concurrent;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(policy =>
    {
        policy
            .WithOrigins("http://127.0.0.1:4173", "http://localhost:4173")
            .AllowAnyHeader()
            .AllowAnyMethod();
    });
});

var app = builder.Build();
app.UseCors();

var store = new ConcurrentDictionary<string, Trip>();

app.MapGet("/health", () => Results.Ok(new { status = "ok", time = DateTimeOffset.UtcNow }));

app.MapPost("/api/trips", (CreateTripRequest request) =>
{
    if (string.IsNullOrWhiteSpace(request.Name))
    {
        return Results.BadRequest(new { error = "Trip name is required." });
    }

    var trip = new Trip
    {
        Id = Guid.NewGuid().ToString("N"),
        Name = request.Name.Trim(),
        Members = new List<Member>(),
        Expenses = new List<Expense>()
    };

    store[trip.Id] = trip;
    return Results.Ok(trip);
});

app.MapPost("/api/trips/{tripId}/members", (string tripId, AddMemberRequest request) =>
{
    if (!store.TryGetValue(tripId, out var trip))
    {
        return Results.NotFound(new { error = "Trip not found." });
    }

    if (string.IsNullOrWhiteSpace(request.Name))
    {
        return Results.BadRequest(new { error = "Member name is required." });
    }

    var member = new Member
    {
        Id = Guid.NewGuid().ToString("N"),
        Name = request.Name.Trim()
    };

    trip.Members.Add(member);
    return Results.Ok(member);
});

app.MapPost("/api/trips/{tripId}/expenses", (string tripId, AddExpenseRequest request) =>
{
    if (!store.TryGetValue(tripId, out var trip))
    {
        return Results.NotFound(new { error = "Trip not found." });
    }

    if (request.Amount <= 0)
    {
        return Results.BadRequest(new { error = "Expense amount must be greater than zero." });
    }

    if (!trip.Members.Any(m => m.Id == request.PaidByMemberId))
    {
        return Results.BadRequest(new { error = "Payer is not part of this trip." });
    }

    var participants = request.ParticipantMemberIds?.Distinct().ToList() ?? new List<string>();
    if (participants.Count == 0)
    {
        return Results.BadRequest(new { error = "At least one participant is required." });
    }

    if (participants.Any(memberId => !trip.Members.Any(m => m.Id == memberId)))
    {
        return Results.BadRequest(new { error = "One or more participants are invalid for this trip." });
    }

    var expense = new Expense
    {
        Id = Guid.NewGuid().ToString("N"),
        Description = string.IsNullOrWhiteSpace(request.Description) ? "Expense" : request.Description.Trim(),
        PaidByMemberId = request.PaidByMemberId,
        Amount = decimal.Round(request.Amount, 2, MidpointRounding.AwayFromZero),
        ParticipantMemberIds = participants
    };

    trip.Expenses.Add(expense);
    return Results.Ok(expense);
});

app.MapGet("/api/trips/{tripId}/summary", (string tripId) =>
{
    if (!store.TryGetValue(tripId, out var trip))
    {
        return Results.NotFound(new { error = "Trip not found." });
    }

    var summary = BuildSummary(trip);
    return Results.Ok(summary);
});

app.Run();

static TripSummaryResponse BuildSummary(Trip trip)
{
    var balances = trip.Members.ToDictionary(m => m.Id, _ => 0m);

    foreach (var expense in trip.Expenses)
    {
        balances[expense.PaidByMemberId] += expense.Amount;

        var split = expense.Amount / expense.ParticipantMemberIds.Count;
        split = decimal.Round(split, 2, MidpointRounding.AwayFromZero);

        foreach (var participantId in expense.ParticipantMemberIds)
        {
            balances[participantId] -= split;
        }
    }

    var balancesList = trip.Members
        .Select(m => new BalanceItem(m.Id, m.Name, decimal.Round(balances[m.Id], 2, MidpointRounding.AwayFromZero)))
        .ToList();

    var creditors = balancesList
        .Where(b => b.Amount > 0)
        .Select(b => new SettlementCursor(b.MemberId, b.MemberName, b.Amount))
        .OrderByDescending(b => b.Amount)
        .ToList();

    var debtors = balancesList
        .Where(b => b.Amount < 0)
        .Select(b => new SettlementCursor(b.MemberId, b.MemberName, Math.Abs(b.Amount)))
        .OrderByDescending(b => b.Amount)
        .ToList();

    var settlements = new List<SettlementItem>();
    var c = 0;
    var d = 0;

    while (c < creditors.Count && d < debtors.Count)
    {
        var amount = Math.Min(creditors[c].Amount, debtors[d].Amount);
        amount = decimal.Round(amount, 2, MidpointRounding.AwayFromZero);

        if (amount > 0)
        {
            settlements.Add(new SettlementItem(
                debtors[d].MemberId,
                debtors[d].MemberName,
                creditors[c].MemberId,
                creditors[c].MemberName,
                amount
            ));

            creditors[c].Amount -= amount;
            debtors[d].Amount -= amount;
        }

        if (creditors[c].Amount <= 0.009m)
        {
            c++;
        }

        if (debtors[d].Amount <= 0.009m)
        {
            d++;
        }
    }

    return new TripSummaryResponse(trip.Id, trip.Name, balancesList, settlements);
}

sealed class Trip
{
    public string Id { get; set; } = string.Empty;
    public string Name { get; set; } = string.Empty;
    public List<Member> Members { get; set; } = new();
    public List<Expense> Expenses { get; set; } = new();
}

sealed class Member
{
    public string Id { get; set; } = string.Empty;
    public string Name { get; set; } = string.Empty;
}

sealed class Expense
{
    public string Id { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public string PaidByMemberId { get; set; } = string.Empty;
    public decimal Amount { get; set; }
    public List<string> ParticipantMemberIds { get; set; } = new();
}

sealed class SettlementCursor
{
    public SettlementCursor(string memberId, string memberName, decimal amount)
    {
        MemberId = memberId;
        MemberName = memberName;
        Amount = amount;
    }

    public string MemberId { get; }
    public string MemberName { get; }
    public decimal Amount { get; set; }
}

record CreateTripRequest(string Name);
record AddMemberRequest(string Name);
record AddExpenseRequest(string Description, string PaidByMemberId, decimal Amount, List<string> ParticipantMemberIds);
record BalanceItem(string MemberId, string MemberName, decimal Amount);
record SettlementItem(string FromMemberId, string FromMemberName, string ToMemberId, string ToMemberName, decimal Amount);
record TripSummaryResponse(string TripId, string TripName, List<BalanceItem> Balances, List<SettlementItem> Settlements);
