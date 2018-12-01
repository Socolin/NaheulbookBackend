Feature: Effect

  Scenario: Listing effects by category
    When performing a GET to the url "/api/v2/effectCategories/1/effects"
    Then the response status code is 200
    And the response should contains a json array containing the following element identified by id
    """
    {
        "id": 5,
        "name": "Allongement des bras",
        "description": "Signe distinctif difficile à cacher, handicapant.",
        "durationType": "forever",
        "duration": null,
        "combatCount": null,
        "lapCount": null,
        "timeDuration": null,
        "dice": 5,
        "categoryId": 1,
        "modifiers": [
            {
                "stat": "AD",
                "value": -1,
                "type": "ADD"
            },
            {
                "stat": "CHA",
                "value": -1,
                "type": "ADD"
            }
        ]
    }
    """

  Scenario: Listing effect categories
    When performing a GET to the url "/api/v2/effectCategories/"
    Then the response status code is 200
    And the response should contains a json array containing the following element identified by id
    """
    {
        "id": 1,
        "name": "Mutation",
        "categories": [
            {
                "id": 1,
                "name": "Mutation mineure",
                "diceCount": 2,
                "diceSize": 6,
                "note": "La mutation magique survient en cinq minutes. Pour se débarrasser d'une mutation, lui ou un allié, un mage peut essayer un dispel magic, une seule fois.\nEn cas d'échec, il convient de se rendre dans une clinique de mages, à Glargh ou bien à Waldorg. Le résultat n'est pas garanti.\nDans le cas d'une mutation non visible immédiatement, le MJ doit se débrouiller pour permettre au héros de découvrir sa mutation, cela peut prendre du temps.",
                "typeId": 1
            },
            {
                "id": 2,
                "name": "Mutation majeure",
                "diceCount": 1,
                "diceSize": 20,
                "note": "La mutation magique survient en cinq minutes. Pour se débarrasser d'une mutation, lui ou un allié, un mage peut essayer un dispel magic, une seule fois.\nEn cas d'échec, il convient de se rendre dans une clinique de mages, à Glargh ou bien à Waldorg. Le résultat n'est pas garanti.\nDans le cas d'une mutation non visible immédiatement, le MJ doit se débrouiller pour permettre au héros de découvrir sa mutation, cela peut prendre du temps.",
                "typeId": 1
            }
        ]
    }
    """
