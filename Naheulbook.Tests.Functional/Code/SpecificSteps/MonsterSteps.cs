using System;
using System.Collections.Generic;
using Naheulbook.Data.Models;
using Naheulbook.TestUtils;
using TechTalk.SpecFlow;

namespace Naheulbook.Tests.Functional.Code.SpecificSteps
{
    [Binding]
    public class MonsterSteps
    {
        private readonly TestDataUtil _testDataUtil;

        public MonsterSteps(TestDataUtil testDataUtil)
        {
            _testDataUtil = testDataUtil;
        }

        [Given("a monster category type")]
        public void GivenAMonsterCategoryType()
        {
            _testDataUtil.AddMonsterType();
        }

        [Given("a monster category")]
        public void GivenAMonsterCategory()
        {
            _testDataUtil.AddMonsterCategory();
        }

        [Given("a monster trait")]
        public void GivenAMonsterTrait()
        {
            _testDataUtil.AddMonsterTrait();
        }

        [Given("a monster template")]
        public void GivenAMonsterTemplate()
        {
            if (!_testDataUtil.Contains<MonsterType>())
                _testDataUtil.AddMonsterType();
            if (!_testDataUtil.Contains<MonsterCategory>())
                _testDataUtil.AddMonsterCategory();
            _testDataUtil.AddMonsterTemplate();
        }

        [Given("a monster template with locations and inventory")]
        public void GivenAMonsterTemplateWithLocationAndInventory()
        {
            if (!_testDataUtil.Contains<MonsterType>())
                _testDataUtil.AddMonsterType();
            if (!_testDataUtil.Contains<MonsterCategory>())
                _testDataUtil.AddMonsterCategory();

            _testDataUtil.AddItemTemplateSection().AddItemTemplateCategory().AddItemTemplate();
            _testDataUtil.AddLocation();
            _testDataUtil.AddMonsterTemplate(m =>
            {
                m.Locations = new List<MonsterLocation>
                {
                    new MonsterLocation {Location = _testDataUtil.GetLast<Location>()}
                };
                m.Items = new List<MonsterTemplateSimpleInventory>
                {
                    new MonsterTemplateSimpleInventory
                    {
                        Chance = 0.5f,
                        ItemTemplate = _testDataUtil.GetLast<ItemTemplate>(),
                        MinCount = 1,
                        MaxCount = 3
                    }
                };
            });
        }

        [Given("a monster")]
        public void GivenAMonster()
        {
            _testDataUtil.AddMonster();
        }

        [Given("a dead monster")]
        public void GivenADeadMonster()
        {
            _testDataUtil.AddMonster(m => m.Dead = new DateTime(2042, 8, 6, 12, 23, 24, DateTimeKind.Utc));
        }

        [Given("a monster with an item in its inventory")]
        public void GivenAMonsterWithAnItemInItsInventory()
        {
            _testDataUtil.AddMonster();
            _testDataUtil.AddItem(_testDataUtil.GetLast<Monster>());
        }
    }
}