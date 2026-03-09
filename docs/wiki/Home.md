# TIA Trip Wiki

TIA Trip is an expense-splitting app with:
- ASP.NET Core API (`src/api/TiaTrip.Api`)
- Web UI (`src/web/index.html`)
- Playwright tests in TypeScript (`tests/e2e-ts`)
- Playwright tests in C# (`tests/e2e-dotnet/TiaTrip.E2E.Dotnet`)

## Quick Start

1. Start API:

```bash
npm run api:start
```

2. Start UI (new terminal):

```bash
npm run web:start
```

3. Open UI:

`http://127.0.0.1:4173`

4. Run TypeScript tests:

```bash
npm run test:ts
```

5. Run C# tests:

```bash
dotnet test tests/e2e-dotnet/TiaTrip.E2E.Dotnet/TiaTrip.E2E.Dotnet.csproj
```

## Important Files

- API entry + endpoints: `src/api/TiaTrip.Api/Program.cs`
- UI screen + form logic: `src/web/index.html`
- TS page object: `tests/e2e-ts/pages/home.page.ts`
- TS tests: `tests/e2e-ts/home.spec.ts`
- C# tests: `tests/e2e-dotnet/TiaTrip.E2E.Dotnet/HomeTests.cs`
- CI workflow: `.github/workflows/ci.yml`

## Development Rule

For every new feature:
1. Implement API endpoint.
2. Update UI flow.
3. Add TypeScript Playwright test.
4. Add C# Playwright test.

This keeps behavior consistent across both test stacks.

## Next Page

- [Learning Map](Learning-Map)
