Feature: Expense settlement in TIA Trip
  As a trip organizer
  I want to track shared expenses
  So that everyone knows who should pay whom

  Background:
    Given I open the TIA Trip application

  @happy
  Scenario: Create trip and split one expense among three people
    When I create a trip named "Goa Trip"
    And I add member "Alice"
    And I add member "Bob"
    And I add member "Charlie"
    And I add an expense "Dinner" paid by "Alice" of "90"
    Then I should see settlement "Bob pays Alice: 30.00"
    And I should see settlement "Charlie pays Alice: 30.00"

  @validation
  Scenario: Adding member before creating trip shows error
    When I add member "NoTripUser"
    Then I should see status message "Create a trip first."

  @regression
  Scenario Outline: Verify settlements for different totals
    When I create a trip named "<trip>"
    And I add member "Alice"
    And I add member "Bob"
    And I add member "Charlie"
    And I add an expense "<description>" paid by "<payer>" of "<amount>"
    Then I should see settlement "<settlement1>"
    And I should see settlement "<settlement2>"

    Examples:
      | trip      | description | payer | amount | settlement1              | settlement2                  |
      | Team Lunch| Lunch       | Alice | 60     | Bob pays Alice: 20.00    | Charlie pays Alice: 20.00    |
      | Taxi Ride | Taxi        | Bob   | 75     | Alice pays Bob: 25.00    | Charlie pays Bob: 25.00      |
