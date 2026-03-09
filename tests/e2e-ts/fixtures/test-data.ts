export type ExpenseScenario = {
  tripName: string;
  members: string[];
  expense: {
    description: string;
    payer: string;
    amount: string;
  };
  expectedSettlements: string[];
};

export const expenseScenario: ExpenseScenario = {
  tripName: 'Goa Trip',
  members: ['Alice', 'Bob', 'Charlie'],
  expense: {
    description: 'Dinner',
    payer: 'Alice',
    amount: '90'
  },
  expectedSettlements: ['Bob pays Alice: 30.00', 'Charlie pays Alice: 30.00']
};
