# 30-Day Interview-Ready Plan (TIA Trip)

Goal: become confident in TypeScript + C# + API + Playwright + interviews by building and testing TIA Trip every day.

## Time Budget

- Weekdays: 2 to 3 hours/day
- Weekends: 4 to 5 hours/day

## Core Learning Resources (high-quality)

### TypeScript
- TypeScript docs: https://www.typescriptlang.org/docs/
- TypeScript Handbook: https://www.typescriptlang.org/docs/handbook/intro.html

### C# and .NET
- C# docs (Microsoft): https://learn.microsoft.com/en-us/dotnet/csharp/
- ASP.NET Core docs: https://learn.microsoft.com/en-us/aspnet/core/

### Playwright
- Playwright intro (TS): https://playwright.dev/docs/intro
- Playwright .NET intro: https://playwright.dev/dotnet/docs/intro

### GitHub Actions
- Workflow syntax docs: https://docs.github.com/en/actions/reference/workflows-and-actions

### Interview Practice
- LeetCode problemset: https://leetcode.com/problemset/
- HackerRank Interview Prep Kit: https://www.hackerrank.com/interview/interview-preparation-kit
- System Design Primer: https://github.com/donnemartin/system-design-primer

## Weekly Outcomes

- Week 1: Strong basics + understand existing TIA Trip code
- Week 2: Build features + add tests in both TS and C#
- Week 3: Add database + improve quality + CI confidence
- Week 4: Interview-focused drills + mock interviews + portfolio polish

## Day-by-Day Plan

### Week 1: Foundations and repo fluency

Day 1
- Read `src/web/index.html` and run UI flow manually.
- Read `src/api/TiaTrip.Api/Program.cs` endpoint by endpoint.
- Run both test suites once.

Day 2
- Study TS basics: types, interfaces, async/await.
- Update one small TS helper in `tests/e2e-ts/pages/home.page.ts`.

Day 3
- Study C# basics: classes, records, LINQ, async.
- Refactor one small method in `Program.cs` for readability.

Day 4
- Study REST API fundamentals (request/response/status codes).
- Add one API validation error path and surface it in UI.

Day 5
- Study Playwright TS docs.
- Add 1 negative TS test (for example: no participants).

Day 6
- Study Playwright .NET docs.
- Add matching negative C# Playwright test.

Day 7
- Review and document what you changed in README/Wiki.
- Push and verify CI green.

### Week 2: Product features + test discipline

Day 8
- Add expense category field in API + UI.
- Update TS and C# tests for category.

Day 9
- Add expense history list to UI.
- Assert history entries in both test stacks.

Day 10
- Add duplicate-member prevention in API.
- Add TS/C# tests asserting error message.

Day 11
- Add edit expense endpoint + UI action.
- Add TS/C# tests for edit.

Day 12
- Add delete expense endpoint + UI action.
- Add TS/C# tests for delete.

Day 13
- Improve settlement rounding and edge-case handling.
- Add focused tests with uneven splits.

Day 14
- Code cleanup + docs update + CI review.

### Week 3: Real-world engineering skills

Day 15
- Introduce SQLite persistence (replace in-memory data).

Day 16
- Add migration strategy and startup checks.

Day 17
- Add seed/dev data helpers.

Day 18
- Add API error contract consistency (`error` format).

Day 19
- Add loading/disabled states in UI forms.

Day 20
- Strengthen test isolation (data setup per test).

Day 21
- Add one CI enhancement (artifact/log/debug step).

### Week 4: Interview-ready mode

Day 22
- DSA: Arrays/Hashing (3 to 5 problems).

Day 23
- DSA: Two pointers + sliding window.

Day 24
- DSA: Stack/Queue + binary search.

Day 25
- DSA: Trees/graphs fundamentals.

Day 26
- DSA: Dynamic programming basics.

Day 27
- System design: read and summarize one design topic (cache, load balancer, queues).

Day 28
- Behavioral interview prep: write 6 STAR stories from your TIA Trip work.

Day 29
- Mock interview day:
  - 1 coding mock (45 min)
  - 1 system design mock (45 min)
  - 1 behavioral mock (30 min)

Day 30
- Portfolio polish:
  - final README screenshots
  - architecture diagram
  - feature list + roadmap
  - create `v0.1.0` tag and release notes

## Daily Interview Routine (from Day 8 onward)

- 45 minutes coding practice (DSA)
- 60 to 90 minutes TIA Trip feature work
- 15 minutes review of mistakes and notes

## Interview Readiness Checklist

- [ ] Can explain TIA Trip architecture in 3 minutes.
- [ ] Can explain one API endpoint design decision and tradeoffs.
- [ ] Can write one Playwright test in TS and one in C# without reference.
- [ ] Can debug failing CI workflow quickly.
- [ ] Solved 100+ DSA problems with revision notes.
- [ ] Prepared 6 behavioral STAR stories with measurable outcomes.

## Your weekly demo habit

At the end of each week, record a 3 to 5 minute demo:
1. Feature built
2. Test evidence (TS + C#)
3. CI status
4. What you learned

This becomes portfolio proof and interview material.
