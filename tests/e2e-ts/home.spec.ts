import { test } from './fixtures/custom-test';

test.describe('Expense splitter e2e', () => {
  test('@smoke should create trip, add members, and compute settlement', async ({ homePage, scenarioData }) => {
    await homePage.goto();
    await homePage.createTrip(scenarioData.tripName);

    for (const member of scenarioData.members) {
      await homePage.addMember(member);
    }

    await homePage.addExpense(
      scenarioData.expense.description,
      scenarioData.expense.payer,
      scenarioData.expense.amount
    );

    for (const settlement of scenarioData.expectedSettlements) {
      await homePage.assertSettlement(settlement);
    }
  });
});
