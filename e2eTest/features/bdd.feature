Feature: open TodoList

Scenario: Visit TodoList Home Page
  Given I open the browser
  When I navigate to "http://localhost:3000"
  Then the page title should be "Todo App"

Scenario: create a new Todo Item
  Given I open the browser
  Given I navigate to "http://localhost:3000"
  When I add a new "Todo-123"
  Then the new item should be there "Todo-123"

