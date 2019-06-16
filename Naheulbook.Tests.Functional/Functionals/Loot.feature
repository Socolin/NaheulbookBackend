Feature: Loot

  Scenario: Create a loot
    Given a JWT for a user
    Given a group

    When performing a POST to the url "/api/v2/groups/${Group.Id}/loots" with the following json content and the current jwt
    """
    {
      "name": "some-loot-name",
    }
    """
    Then the response status code is 201
    And the response should contains the following json
    """
    {
      "id": {"__match": {"type": "integer"}},
      "visibleForPlayer": false,
      "name": "some-loot-name",
      "items": [],
      "monsters": []
    }
    """