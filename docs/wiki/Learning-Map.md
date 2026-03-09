# Learning Map

This is a practical day-by-day path to learn and build TIA Trip.

## Week 1 Plan

### Day 1: Understand the current flow
- Run API + UI locally.
- Create a trip, add members, add one expense.
- Read these files:
  - `src/web/index.html`
  - `src/api/TiaTrip.Api/Program.cs`
  - `tests/e2e-ts/home.spec.ts`
  - `tests/e2e-dotnet/TiaTrip.E2E.Dotnet/HomeTests.cs`

### Day 2: Add API validation
- Add checks in `Program.cs`:
  - block duplicate member names
  - block invalid amount values
- Return clear error messages.
- Add one negative test in TS + one in C#.

### Day 3: Add expense history in UI
- Show a list of all expenses in `index.html`.
- Include description, payer, amount, participants.
- Add assertions for history visibility in TS and C# tests.

### Day 4: Improve settlement logic
- Add sorting and clean rounding in `Program.cs`.
- Add tests for tricky splits (e.g., uneven totals).

### Day 5: Add edit/delete operations
- New endpoints in `Program.cs`:
  - edit expense
  - delete expense
- Add UI controls.
- Add e2e tests for edit + delete.

### Day 6: Add persistence (SQLite)
- Replace in-memory store with DB persistence.
- Keep endpoint contracts the same.
- Ensure existing tests still pass.

### Day 7: Polish + release
- Update README and Wiki docs.
- Add screenshots to Wiki.
- Tag release `v0.1.0`.

## How to Add a New Feature (Template)

1. API first
- Add/modify endpoint in `src/api/TiaTrip.Api/Program.cs`.
- Verify with browser or curl.

2. UI update
- Add form/controls in `src/web/index.html`.
- Show success + error states.

3. TS Playwright test
- Add page object methods in `tests/e2e-ts/pages/home.page.ts`.
- Add scenario in `tests/e2e-ts/home.spec.ts`.

4. C# Playwright test
- Add matching scenario in `tests/e2e-dotnet/TiaTrip.E2E.Dotnet/HomeTests.cs`.

5. Run all

```bash
npm run test:ts
dotnet test tests/e2e-dotnet/TiaTrip.E2E.Dotnet/TiaTrip.E2E.Dotnet.csproj
```

## Test Strategy

- Happy path: create trip, add members, add expense, verify settlement.
- Negative path: invalid amount, missing participants, duplicate members.
- Regression path: edit/delete expense updates summary correctly.

## Definition of Done (per feature)

- API behavior implemented.
- UI behavior implemented.
- TS test added and passing.
- C# test added and passing.
- CI green.
