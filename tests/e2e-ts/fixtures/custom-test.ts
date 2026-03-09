import { test as base } from '@playwright/test';
import { HomePage } from '../pages/home.page';
import { expenseScenario, type ExpenseScenario } from './test-data';

type TestFixtures = {
  homePage: HomePage;
  scenarioData: ExpenseScenario;
};

export const test = base.extend<TestFixtures>({
  homePage: async ({ page }, use) => {
    await use(new HomePage(page));
  },
  scenarioData: async ({}, use) => {
    await use(expenseScenario);
  }
});

export { expect } from '@playwright/test';
