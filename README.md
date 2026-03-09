# TIA Trip (Playwright + TypeScript + C#)

A practical starter app where you can build and test a real workflow in two automation stacks:
- Playwright tests in TypeScript
- Playwright tests in C# (.NET)
- ASP.NET Core API backend
- Static web frontend

## Structure

```text
.
├── src/
│   ├── api/TiaTrip.Api/
│   │   ├── Program.cs
│   │   └── TiaTrip.Api.csproj
│   └── web/index.html
├── tests/
│   ├── e2e-ts/
│   │   ├── pages/home.page.ts
│   │   └── home.spec.ts
│   └── e2e-dotnet/TiaTrip.E2E.Dotnet/
│       ├── HomeTests.cs
│       └── TiaTrip.E2E.Dotnet.csproj
├── .github/workflows/ci.yml
├── package.json
└── playwright.config.ts
```

## Prerequisites

- Node.js 20+
- npm 10+
- .NET SDK 10.0+
- PowerShell (`pwsh`) for C# Playwright browser install

## Setup

```bash
npm install
npm run install:browsers
cp .env.example .env
```

## Run the app manually

Terminal 1:

```bash
npm run api:start
```

Terminal 2:

```bash
npm run web:start
```

Open `http://127.0.0.1:4173`.

## Run TypeScript Playwright tests

`npm run test:ts` will auto-start API and web app.

```bash
npm run test:ts
npm run test:ts:headed
npm run test:ts:smoke
```

## Run C# Playwright tests

```bash
dotnet restore tests/e2e-dotnet/TiaTrip.E2E.Dotnet/TiaTrip.E2E.Dotnet.csproj
dotnet build tests/e2e-dotnet/TiaTrip.E2E.Dotnet/TiaTrip.E2E.Dotnet.csproj
pwsh tests/e2e-dotnet/TiaTrip.E2E.Dotnet/bin/Debug/net10.0/playwright.ps1 install chromium
pwsh tests/e2e-dotnet/TiaTrip.E2E.Dotnet/bin/Debug/net10.0/playwright.ps1 install chromium-headless-shell
```

Then run API + web (as above), and in another terminal:

```bash
dotnet test tests/e2e-dotnet/TiaTrip.E2E.Dotnet/TiaTrip.E2E.Dotnet.csproj
```

## API endpoints included

- `POST /api/trips`
- `POST /api/trips/{tripId}/members`
- `POST /api/trips/{tripId}/expenses`
- `GET /api/trips/{tripId}/summary`
- `GET /health`

## Learning path (recommended)

1. Add validations and error messages to API requests.
2. Persist trips to a database (SQLite or PostgreSQL).
3. Add auth and per-user trips.
4. Add negative tests in TS and C#.
5. Add monthly reports and export.
