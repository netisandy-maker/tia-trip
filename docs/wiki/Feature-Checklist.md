# Feature Checklist

Use this checklist for every new TIA Trip feature.

## 1) Scope

- [ ] Feature name:
- [ ] User problem it solves:
- [ ] API endpoints affected:
- [ ] UI areas affected:

## 2) API (C#)

File: `src/api/TiaTrip.Api/Program.cs`

- [ ] Request validation added
- [ ] Response contract defined
- [ ] Error responses handled clearly
- [ ] Happy path tested manually (curl/browser)

## 3) UI

File: `src/web/index.html`

- [ ] Form/controls added
- [ ] Success state visible
- [ ] Error state visible
- [ ] Summary/list rendering updated

## 4) TypeScript Playwright

Files:
- `tests/e2e-ts/pages/home.page.ts`
- `tests/e2e-ts/home.spec.ts`

- [ ] Page object methods added/updated
- [ ] Happy-path test added
- [ ] Negative-path test added
- [ ] Local run passes (`npm run test:ts`)

## 5) C# Playwright

File: `tests/e2e-dotnet/TiaTrip.E2E.Dotnet/HomeTests.cs`

- [ ] Matching happy-path test added
- [ ] Matching negative-path test added
- [ ] Local run passes (`dotnet test tests/e2e-dotnet/TiaTrip.E2E.Dotnet/TiaTrip.E2E.Dotnet.csproj`)

## 6) CI + Docs

- [ ] GitHub Actions is green
- [ ] README updated if commands/behavior changed
- [ ] Wiki updated (Home / Learning-Map if needed)

## 7) Done Criteria

- [ ] Feature works manually in UI
- [ ] TS tests passing
- [ ] C# tests passing
- [ ] CI passing on `main`

## Copy Template (per feature)

### Feature: <name>
- Goal:
- API changes:
- UI changes:
- TS tests added:
- C# tests added:
- Risks/edge cases:
- Status: In Progress / Done
