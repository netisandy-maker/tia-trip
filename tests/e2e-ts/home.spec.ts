import { test } from '@playwright/test';
import { HomePage } from './pages/home.page';

test.describe('Expense splitter e2e', () => {
  test('@smoke should create trip, add members, and compute settlement', async ({ page }) => {
    const home = new HomePage(page);
    await home.goto();

    await home.createTrip('Goa Trip');
    await home.addMember('Alice');
    await home.addMember('Bob');
    await home.addMember('Charlie');

    await home.addExpense('Dinner', 'Alice', '90');

    await home.assertSettlement('Bob pays Alice: 30.00');
    await home.assertSettlement('Charlie pays Alice: 30.00');
  });
});
