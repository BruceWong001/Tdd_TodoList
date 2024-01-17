const { Given, When, Then,After } = require('@cucumber/cucumber');
const { chromium } = require('playwright');
const assert = require('assert');

let browser;
let page;


Given('I open the browser', async () => {
  browser = await chromium.launch();
  page = await browser.newPage();
});

When('I navigate to {string}', async (url) => {
  await page.goto(url);
});


Then('the page title should be {string}', async (expectedTitle) => {
  const pageTitle = await page.title();
  assert.strictEqual(pageTitle, expectedTitle);
});
After(async () => {
  await browser.close();
});

When('I add a new {string}', async (newItem) => {
  const pageTitle = await page.title();

  await page.getByRole('textbox').first().fill(newItem);
  await page.getByRole('textbox').nth(1).click();
  await page.getByRole('textbox').nth(1).fill('2024-01-01T07:00');
  await page.getByRole('textbox').nth(2).click();
  await page.getByRole('textbox').nth(2).fill('2024-01-02T07:10');
  await page.getByRole('button', { name: 'Add' }).click();
});

Then('the new item should be there {string}', async (newItem) => {

  //const item_title = await page.getByRole('listitem').innerText();
  const item_title = await page.getByRole('listitem').first().innerText();

  assert(item_title.includes(newItem), `Item title should contain "${newItem}"`);
  await page.getByRole('button', { name: 'Delete' }).first().click();
});


