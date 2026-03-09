import { expect, Locator, Page } from '@playwright/test';

export class HomePage {
  private readonly page: Page;
  readonly tripName: Locator;
  readonly createTripButton: Locator;
  readonly memberName: Locator;
  readonly addMemberButton: Locator;
  readonly description: Locator;
  readonly paidBy: Locator;
  readonly amount: Locator;
  readonly addExpenseButton: Locator;
  readonly settlementsList: Locator;

  constructor(page: Page) {
    this.page = page;
    this.tripName = page.getByTestId('trip-name');
    this.createTripButton = page.getByTestId('create-trip-btn');
    this.memberName = page.getByTestId('member-name');
    this.addMemberButton = page.getByTestId('add-member-btn');
    this.description = page.getByTestId('expense-description');
    this.paidBy = page.getByTestId('paid-by');
    this.amount = page.getByTestId('amount');
    this.addExpenseButton = page.getByTestId('add-expense-btn');
    this.settlementsList = page.getByTestId('settlements-list');
  }

  async goto() {
    await this.page.goto('/');
  }

  async createTrip(name: string) {
    await this.tripName.fill(name);
    await this.createTripButton.click();
  }

  async addMember(name: string) {
    await this.memberName.fill(name);
    await this.addMemberButton.click();
  }

  async addExpense(description: string, payerName: string, amount: string) {
    await this.description.fill(description);
    await this.paidBy.selectOption({ label: payerName });
    await this.amount.fill(amount);
    await this.addExpenseButton.click();
  }

  async assertSettlement(text: string) {
    await expect(this.settlementsList).toContainText(text);
  }
}
