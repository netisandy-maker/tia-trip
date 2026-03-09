import { expect } from '@playwright/test';
import { test } from '../e2e-ts/fixtures/custom-test';
import { defineFeature } from './framework';

defineFeature(test, 'Feature: Expense settlement in TIA Trip', [
  {
    title: 'Create trip and split one expense among three people',
    tags: ['happy', 'smoke'],
    steps: [
      {
        title: 'Given I open the TIA Trip application',
        run: async ({ homePage }) => {
          await homePage.goto();
        }
      },
      {
        title: 'When I create a trip named Goa Trip',
        run: async ({ homePage }) => {
          await homePage.createTrip('Goa Trip');
        }
      },
      {
        title: 'And I add members Alice, Bob, Charlie',
        run: async ({ homePage }) => {
          await homePage.addMember('Alice');
          await homePage.addMember('Bob');
          await homePage.addMember('Charlie');
        }
      },
      {
        title: 'And I add an expense Dinner paid by Alice of 90',
        run: async ({ homePage }) => {
          await homePage.addExpense('Dinner', 'Alice', '90');
        }
      },
      {
        title: 'Then Bob and Charlie should pay Alice 30 each',
        run: async ({ homePage }) => {
          await homePage.assertSettlement('Bob pays Alice: 30.00');
          await homePage.assertSettlement('Charlie pays Alice: 30.00');
        }
      }
    ]
  },
  {
    title: 'Adding member before creating trip shows validation message',
    tags: ['validation'],
    steps: [
      {
        title: 'Given I open the TIA Trip application',
        run: async ({ homePage }) => {
          await homePage.goto();
        }
      },
      {
        title: 'When I try to add member without trip',
        run: async ({ page }) => {
          await page.getByTestId('member-name').fill('NoTripUser');
          await page.getByTestId('add-member-btn').click();
        }
      },
      {
        title: 'Then I should see Create a trip first message',
        run: async ({ page }) => {
          await expect(page.getByTestId('status')).toHaveText('Create a trip first.');
        }
      }
    ]
  }
]);
