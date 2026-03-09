import type { TestType } from '@playwright/test';

export type BddStep = {
  title: string;
  run: (fixtures: any) => Promise<void>;
};

export type BddScenario = {
  title: string;
  tags?: string[];
  steps: BddStep[];
};

export function defineFeature(
  test: TestType<any, any>,
  featureTitle: string,
  scenarios: BddScenario[]
) {
  test.describe(featureTitle, () => {
    for (const scenario of scenarios) {
      const testTitle = `${scenario.tags?.map((tag) => `@${tag}`).join(' ') ?? ''} ${scenario.title}`.trim();

      test(testTitle, async (fixtures) => {
        for (const step of scenario.steps) {
          await test.step(step.title, async () => {
            await step.run(fixtures);
          });
        }
      });
    }
  });
}
