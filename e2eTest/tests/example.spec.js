// @ts-check
const { test, expect } = require('@playwright/test');

test('has title', async ({ page }) => {
  await page.goto('http://localhost:3000/');

  // Expect a title "to contain" a substring.
  await expect(page).toHaveTitle(/Todo App/);
});
test('add a todo item', async ({ page }) => {
  await page.goto('http://localhost:3000/');

  await page.getByRole('textbox').first().fill('todo123');
  await page.getByRole('textbox').nth(1).click();
  await page.getByRole('textbox').nth(1).fill('2024-01-01T07:00');
  await page.getByRole('textbox').nth(2).click();
  await page.getByRole('textbox').nth(2).fill('2024-01-02T07:10');
  await page.getByRole('button', { name: 'Add' }).click();
  await expect(page.getByText('Title: todo123Start Date')).toBeVisible();
  await expect(page.getByRole('listitem')).toContainText('Title:  todo123Start Date:  1/1/2024, 7:00:00 AMEnd Date:  1/2/2024, 7:10:00 AMDeleteUpdate');
});

// test('get started link', async ({ page }) => {
//   await page.goto('http://localhost:3000/');

//   // Click the get started link.
//   await page.getByRole('link', { name: 'Get started' }).click();

//   // Expects page to have a heading with the name of Installation.
//   await expect(page.getByRole('heading', { name: 'Installation' })).toBeVisible();
// });
