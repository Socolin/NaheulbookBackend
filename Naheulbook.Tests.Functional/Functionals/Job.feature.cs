// ------------------------------------------------------------------------------
//  <auto-generated>
//      This code was generated by SpecFlow (http://www.specflow.org/).
//      SpecFlow Version:3.0.0.0
//      SpecFlow Generator Version:3.0.0.0
// 
//      Changes to this file may cause incorrect behavior and will be lost if
//      the code is regenerated.
//  </auto-generated>
// ------------------------------------------------------------------------------
#region Designer generated code
#pragma warning disable
namespace Naheulbook.Tests.Functional.Functionals
{
    using TechTalk.SpecFlow;
    
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("TechTalk.SpecFlow", "3.0.0.0")]
    [System.Runtime.CompilerServices.CompilerGeneratedAttribute()]
    [NUnit.Framework.TestFixtureAttribute()]
    [NUnit.Framework.DescriptionAttribute("Job")]
    public partial class JobFeature
    {
        
        private TechTalk.SpecFlow.ITestRunner testRunner;
        
#line 1 "Job.feature"
#line hidden
        
        [NUnit.Framework.OneTimeSetUpAttribute()]
        public virtual void FeatureSetup()
        {
            testRunner = TechTalk.SpecFlow.TestRunnerManager.GetTestRunner();
            TechTalk.SpecFlow.FeatureInfo featureInfo = new TechTalk.SpecFlow.FeatureInfo(new System.Globalization.CultureInfo("en-US"), "Job", null, ProgrammingLanguage.CSharp, ((string[])(null)));
            testRunner.OnFeatureStart(featureInfo);
        }
        
        [NUnit.Framework.OneTimeTearDownAttribute()]
        public virtual void FeatureTearDown()
        {
            testRunner.OnFeatureEnd();
            testRunner = null;
        }
        
        [NUnit.Framework.SetUpAttribute()]
        public virtual void TestInitialize()
        {
        }
        
        [NUnit.Framework.TearDownAttribute()]
        public virtual void ScenarioTearDown()
        {
            testRunner.OnScenarioEnd();
        }
        
        public virtual void ScenarioInitialize(TechTalk.SpecFlow.ScenarioInfo scenarioInfo)
        {
            testRunner.OnScenarioInitialize(scenarioInfo);
            testRunner.ScenarioContext.ScenarioContainer.RegisterInstanceAs<NUnit.Framework.TestContext>(NUnit.Framework.TestContext.CurrentContext);
        }
        
        public virtual void ScenarioStart()
        {
            testRunner.OnScenarioStart();
        }
        
        public virtual void ScenarioCleanup()
        {
            testRunner.CollectScenarioErrors();
        }
        
        [NUnit.Framework.TestAttribute()]
        [NUnit.Framework.DescriptionAttribute("Listing jobs")]
        public virtual void ListingJobs()
        {
            TechTalk.SpecFlow.ScenarioInfo scenarioInfo = new TechTalk.SpecFlow.ScenarioInfo("Listing jobs", null, ((string[])(null)));
#line 3
  this.ScenarioInitialize(scenarioInfo);
            this.ScenarioStart();
#line 4
    testRunner.When("performing a GET to the url \"/api/v2/jobs\"", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "When ");
#line 5
    testRunner.Then("the response status code be 200", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Then ");
#line hidden
#line 6
    testRunner.And("the response should contains a json array containing the following element identi" +
                    "fied by id", "{\n    \"id\": 10,\n    \"name\": \"Mage, Sorcier\",\n    \"internalname\": null,\n    \"infor" +
                    "mations\": \"Aptitudes à la magie : peut lancer tous les sortilèges. Peut choisir " +
                    "une spécialité magique ou une affiliation à un dieu ou\\ndémon. Peut faire une cu" +
                    "re (payante) pour gagner des points astraux. Peut utiliser un bâton magique. Peu" +
                    "t pratiquer des\\ninvocations ou enchanter des objets.\",\n    \"playerDescription\":" +
                    " \"Mon truc, c\'est la maîtrise de la puissance astrale, et l\'apprentissage de mon" +
                    " art. Je n\'ai pas fait des études compliquées pendant des années pour me la coul" +
                    "er douce dans un bureau ou au milieu des champs de navets ! Alors attention, ça " +
                    "va chauffer pour les miches des gobelins.\",\n    \"playerSummary\": \"Personnage qui" +
                    " lance des sorts variés, qui sait parlementer, qui peut résoudre les énigmes et " +
                    "détecter la magie.\\r\\nArmes utilisables : bâton, poignard, dague, gourdin (corps" +
                    " à corps). Pas d\'arc ou arbalètes. N’utilise pas de bouclier.\\r\\nPeut utiliser t" +
                    "oute forme de magie, parchemin, protections magiques, potions, etc. Bâton non ob" +
                    "ligatoire.\\r\\nTransport de charge limitée, dépendant de l\'origine. Armure max : " +
                    "PR2.\",\n    \"maxLoad\": 10,\n    \"maxArmorPR\": 2,\n    \"isMagic\": true,\n    \"baseEv\"" +
                    ": null,\n    \"factorEv\": 0.7,\n    \"bonusEv\": null,\n    \"baseEa\": 30,\n    \"diceEaL" +
                    "evelUp\": 6,\n    \"baseAT\": null,\n    \"basePRD\": null,\n    \"parentJobId\": null,\n  " +
                    "  \"flags\": [\n        {\n            \"type\": \"SELECT_SPECIALITY_LVL_5_10\",\n       " +
                    "     \"data\": null\n        }\n    ],\n    \"skillIds\": [\n        22,\n        41,\n   " +
                    "     43\n    ],\n    \"availableSkillIds\": [\n        3,\n        13,\n        15,\n   " +
                    "     26,\n        32,\n        39\n    ],\n    \"originsWhitelist\": [],\n    \"originsB" +
                    "lacklist\": [\n        {\n            \"id\": 1,\n            \"name\": \"Humain\"\n       " +
                    " }\n    ],\n    \"bonuses\": [],\n    \"requirements\": [\n        {\n            \"stat\":" +
                    " \"INT\",\n            \"min\": 12,\n            \"max\": null\n        }\n    ],\n    \"res" +
                    "tricts\": [\n        {\n            \"description\": \"ne peut utiliser autre chose qu" +
                    "e : bâton, poignard, dague, gourdin comme arme de combat rapproché\",\n           " +
                    " \"flags\": [\n                {\n                    \"type\": \"NO_WEAPON_TYPE\",\n    " +
                    "                \"data\": \"EPEE\"\n                },\n                {\n            " +
                    "        \"type\": \"NO_WEAPON_TYPE\",\n                    \"data\": \"HACHE\"\n          " +
                    "      }\n            ]\n        },\n        {\n            \"description\": \"n’utilise" +
                    " pas les arcs ou arbalètes\",\n            \"flags\": [\n                {\n          " +
                    "          \"type\": \"NO_WEAPON_TYPE\",\n                    \"data\": \"ARBALETE\"\n     " +
                    "           },\n                {\n                    \"type\": \"NO_WEAPON_TYPE\",\n  " +
                    "                  \"data\": \"ARC\"\n                }\n            ]\n        },\n     " +
                    "   {\n            \"description\": \"n’utilise pas les boucliers\",\n            \"flag" +
                    "s\": [\n                {\n                    \"type\": \"NO_WEAPON_TYPE\",\n          " +
                    "          \"data\": \"BOUCLIER\"\n                }\n            ]\n        },\n        " +
                    "{\n            \"description\": \"transport de charge limitée à 10 kilos (ou moins s" +
                    "elon race)\",\n            \"flags\": []\n        },\n        {\n            \"descripti" +
                    "on\": \"n’utilise pas d’armure supérieure à PR2 (sauf magique)\",\n            \"flag" +
                    "s\": []\n        }\n    ],\n    \"specialities\": [\n        {\n            \"id\": 1,\n   " +
                    "         \"name\": \"Invocation\",\n            \"description\": \"Le mage invocateur a " +
                    "délaissé la magie généraliste pour s\'adonner à son passe-temps favori : faire ap" +
                    "paraître dans le\\nmonde des humains des créatures étranges, dangereuses et à moi" +
                    "tié folles, dans le seul but de se faire servir et d\'accéder\\nrapidement à la pu" +
                    "issance. Ce n\'est pas une spécialité très populaire car elle a un côté aléatoire" +
                    " très prononcé. Néanmoins,\\nelle peut être vraiment passionnante pour peu qu\'on " +
                    "ait un peu d\'instinct de survie. L\'invocateur sait aussi lancer\\nquelques sortil" +
                    "èges normaux, mais ça l\'ennuie... Et il ne pourra utiliser qu’un seul grimoire t" +
                    "out au long de sa carrière !\",\n            \"modifiers\": [],\n            \"special" +
                    "s\": [\n                {\n                    \"id\": 10,\n                    \"isBon" +
                    "us\": false,\n                    \"description\": \"le mage ne peut choisir aucune a" +
                    "utre spécialisation que l\'invocation – sa panoplie de sorts est réduite\",\n      " +
                    "              \"flags\": [\n                        {\n                            \"" +
                    "type\": \"ONE_SPECIALITY\",\n                            \"data\": null\n              " +
                    "          }\n                    ]\n                },\n                {\n         " +
                    "           \"id\": 11,\n                    \"isBonus\": false,\n                    \"" +
                    "description\": \"le mage doit faire attention à son charisme\",\n                   " +
                    " \"flags\": []\n                },\n                {\n                    \"id\": 12,\n" +
                    "                    \"isBonus\": false,\n                    \"description\": \"Le mag" +
                    "e doit renvoyer toutes ses créatures (hors familier, compagnon ou double) avant " +
                    "de se reposer, ou bien elles\\néchapperont à son contrôle\",\n                    \"" +
                    "flags\": []\n                },\n                {\n                    \"id\": 13,\n  " +
                    "                  \"isBonus\": true,\n                    \"description\": \"le mage n" +
                    "\'a pas forcément besoin d\'être adroit\",\n                    \"flags\": []\n        " +
                    "        },\n                {\n                    \"id\": 14,\n                    \"" +
                    "isBonus\": true,\n                    \"description\": \"le mage a accès à quelques s" +
                    "orts de magie généraliste, mais seulement ceux de son grimoire\",\n               " +
                    "     \"flags\": [\n                        {\n                            \"type\": \"N" +
                    "O_GENERIC_MAGIC\",\n                            \"data\": null\n                     " +
                    "   }\n                    ]\n                },\n                {\n                " +
                    "    \"id\": 15,\n                    \"isBonus\": true,\n                    \"descript" +
                    "ion\": \"le mage commence sa carrière avec le « Manuel de l\'Invocateur Inconscient" +
                    " » qui lui servira jusqu\'au niveau 10*\",\n                    \"flags\": []\n       " +
                    "         },\n                {\n                    \"id\": 16,\n                    " +
                    "\"isBonus\": true,\n                    \"description\": \"le mage a accès aux sortilè" +
                    "ges inédits réservés aux invocateurs\",\n                    \"flags\": []\n         " +
                    "       }\n            ],\n            \"flags\": []\n        },\n        {\n           " +
                    " \"id\": 2,\n            \"name\": \"Magie Noire de Tzinntch\",\n            \"descriptio" +
                    "n\": \"Pour pratiquer cette discipline, il faut adhérer à la secte. En récompense " +
                    "de vos bons offices (et d\'une contribution non\\nnégligeable en monnaie), le cult" +
                    "e vous permet d\'accéder à des sortilèges de combat inédits, plus puissants et pl" +
                    "us\\néconomiques en énergie astrale. Vous avez aussi accès à du matériel exceptio" +
                    "nnel à des prix relativement intéressants.\\nC\'est une carrière qui ne vous rendr" +
                    "a pas populaire – tout comme celle de nécromant – mais qui propose une escalade\\" +
                    "nrapide vers la puissance, comme une sorte de « côté obscur de la force », si vo" +
                    "us voyez ce que je veux dire. Les sortilèges de\\nTzinntch sont très prisés par l" +
                    "es mages vicieux désirant s\'illustrer au combat. Le gain de puissance se fait en" +
                    " revanche au\\ndétriment d\'une partie de votre santé.\",\n            \"modifiers\": " +
                    "[\n                {\n                    \"stat\": \"EA\",\n                    \"type\"" +
                    ": \"Add\",\n                    \"value\": 5\n                },\n                {\n   " +
                    "                 \"stat\": \"FO\",\n                    \"type\": \"Add\",\n              " +
                    "      \"value\": -2\n                },\n                {\n                    \"stat" +
                    "\": \"EV\",\n                    \"type\": \"Add\",\n                    \"value\": -5\n    " +
                    "            }\n            ],\n            \"specials\": [\n                {\n       " +
                    "             \"id\": 1,\n                    \"isBonus\": false,\n                    " +
                    "\"description\": \"le mage ne peut choisir aucune autre spécialisation que celle de" +
                    " Tzinntch\",\n                    \"flags\": [\n                        {\n           " +
                    "                 \"type\": \"ONE_SPECIALITY\",\n                            \"data\": n" +
                    "ull\n                        }\n                    ]\n                },\n         " +
                    "       {\n                    \"id\": 2,\n                    \"isBonus\": false,\n    " +
                    "                \"description\": \"le mage doit préserver 50% de ses gains en or, n" +
                    "ature et joyaux pour les donner au culte\",\n                    \"flags\": []\n     " +
                    "           },\n                {\n                    \"id\": 3,\n                   " +
                    " \"isBonus\": false,\n                    \"description\": \"le mage commence sa carri" +
                    "ère avec -5 points d\'EV et -2 en FO sur ses compétences d\'origine\",\n            " +
                    "        \"flags\": []\n                },\n                {\n                    \"id" +
                    "\": 4,\n                    \"isBonus\": false,\n                    \"description\": \"" +
                    "le mage gagne 1D6-1 en EV au changement de niveau s\'il choisit d\'augmenter l\'EV " +
                    "(minimum 1)\",\n                    \"flags\": [\n                        {\n         " +
                    "                   \"type\": \"LEVELUP_DICE_EV_-1\",\n                            \"da" +
                    "ta\": null\n                        }\n                    ]\n                },\n   " +
                    "             {\n                    \"id\": 5,\n                    \"isBonus\": false" +
                    ",\n                    \"description\": \"le mage doit obligatoirement porter la rob" +
                    "e noire de l\'ordre – il peut porter quelque chose en-dessous, coquins\",\n        " +
                    "            \"flags\": []\n                },\n                {\n                   " +
                    " \"id\": 6,\n                    \"isBonus\": true,\n                    \"description\"" +
                    ": \"le mage a accès à tous les sorts de « magie généraliste », à 1 niveau au-dess" +
                    "us du sien\",\n                    \"flags\": [\n                        {\n          " +
                    "                  \"type\": \"GENERIC_MAGIC_+1_LEVEL\",\n                            " +
                    "\"data\": null\n                        }\n                    ]\n                },\n" +
                    "                {\n                    \"id\": 7,\n                    \"isBonus\": tr" +
                    "ue,\n                    \"description\": \"le mage commence sa carrière avec +5 PA\"" +
                    ",\n                    \"flags\": []\n                },\n                {\n         " +
                    "           \"id\": 8,\n                    \"isBonus\": true,\n                    \"de" +
                    "scription\": \"le mage dépense 1 PA de moins pour chaque sort de « magie généralis" +
                    "te » (minimum 1)\",\n                    \"flags\": [\n                        {\n    " +
                    "                        \"type\": \"GENERIC_MAGIC_-1_MANA\",\n                       " +
                    "     \"data\": null\n                        }\n                    ]\n              " +
                    "  },\n                {\n                    \"id\": 9,\n                    \"isBonus" +
                    "\": false,\n                    \"description\": \"le mage a accès aux sortilèges iné" +
                    "dits réservés aux cultistes de Tzinntch\",\n                    \"flags\": []\n      " +
                    "          }\n            ],\n            \"flags\": []\n        },\n        {\n        " +
                    "    \"id\": 3,\n            \"name\": \"Magie de combat\",\n            \"description\": \"" +
                    "Premier stade de la magie dédiée à la violence, et jusqu\'au niveau 14 uniquement" +
                    ". Plus tard, le mage de combat peut\\névoluer vers la « Magie de bataille » ou mê" +
                    "me la « Magie de guerre ». On retrouve dans cette spécialité un grand nombre\\nde" +
                    " sortilèges réservés aux aventuriers, c\'est pourquoi la plupart d\'entre eux choi" +
                    "sissent cette voie. Sorts à destination d\'une\\ncible, sorts de masse, améliorati" +
                    "ons personnelles, la magie de combat offre un grand festival de choix variés.\",\n" +
                    "            \"modifiers\": [],\n            \"specials\": [],\n            \"flags\": []" +
                    "\n        },\n        {\n            \"id\": 4,\n            \"name\": \"Magie du feu\",\n " +
                    "           \"description\": \"C\'est une discipline particulièrement tournée vers la" +
                    " violence, contrairement à ce que peuvent en dire certains professeurs\\négocentr" +
                    "iques. Le feu brûle, détruit et cause en général de gros dégâts. Il n\'a aucune i" +
                    "ntelligence et se trouve rarement\\nsélectif, ce qui aura des conséquences sur le" +
                    "s dommages causés aux équipiers. Néanmoins, il existe dans la spécialité\\nquelqu" +
                    "es sortilèges anodins, purement utilitaires ou simplement esthétiques. On peut m" +
                    "ême y trouver des sorts pour\\namuser les amis. En situation de combat, c\'est idé" +
                    "al pour faire de gros dégâts en consommant le moins d\'énergie astrale\\npossible," +
                    " surtout dans les premiers niveaux. On notera que peu de créatures résistent au " +
                    "feu, en dehors des dragons et des\\ndémons : ce n\'est pas le cas pour la magie de" +
                    " la glace par exemple, contre laquelle on peut se prémunir avec un gros pull.\\nE" +
                    "n revanche, à moins d\'être complètement obsédé par le feu, c\'est assez rapidemen" +
                    "t lassant.\",\n            \"modifiers\": [],\n            \"specials\": [],\n          " +
                    "  \"flags\": []\n        },\n        {\n            \"id\": 5,\n            \"name\": \"Mét" +
                    "amorphoses\",\n            \"description\": \"La métamorphose est une discipline ense" +
                    "ignée par les Dieux du Chaos eux-mêmes. Si les elfes noirs sont passés maîtres\\n" +
                    "dans cette spécialité, elle est accessible à toutes les races chaotiques ou assi" +
                    "milées, donc à presque tout le monde, pour peu\\nqu’on ait un minimum de disposit" +
                    "ions pour la magie. Cependant, malgré le côté spectaculaire de la discipline, de" +
                    " nombreux\\nétudiants la délaissent au profit de spécialités plus efficaces au co" +
                    "mbat. Parmi les magies chaotiques, la métamorphose est\\nune des moins dangereuse" +
                    "s pour la santé mentale et consomme souvent peu d\'énergie astrale.\\n\",\n         " +
                    "   \"modifiers\": [],\n            \"specials\": [],\n            \"flags\": []\n        " +
                    "},\n        {\n            \"id\": 7,\n            \"name\": \"Nécromancie\",\n           " +
                    " \"description\": \"Les mages nécromants ou nécromanciens - ou nécros’ dans le lang" +
                    "age pauvre et imbécile des aventuriers de tous bords -\\nsont des êtres sanguinai" +
                    "res, dangereux et irresponsables. Leurs rêves de puissance n’ont d’égal que la f" +
                    "olie qui les anime. Et\\ncomme ils sont aussi mégalomanes qu’inconscients, leurs " +
                    "espoirs de puissance sont souvent déçus. En outre, ils attirent les\\naventuriers" +
                    " comme personne : Il suffit qu’un nécromant s’installe dans un donjon perdu au m" +
                    "ilieu de nulle part pour que,\\nsitôt son installation connue, les bandes d’avent" +
                    "uriers surgissent comme des essaims d’abeilles tueuses. C\'est ainsi que le\\nnécr" +
                    "omant a bien souvent des tas d\'ennemis, et pas d\'amis. En revanche, s\'il trouve " +
                    "des gens assez fous pour l\'accompagner\\nsur les chemins de l\'aventure, le nécrom" +
                    "ant saura tirer son épingle du jeu dès lors qu\'il prend du galon : les sorts de " +
                    "bas\\nniveau sont plutôt tranquilles, mais la puissance augmente rapidement. Il f" +
                    "aut être patient cela dit car nombre d\'entre eux\\nsont des rituels : il faut don" +
                    "c dans un premier temps trouver les ingrédients, et dans un second temps savoir " +
                    "les utiliser.\",\n            \"modifiers\": [],\n            \"specials\": [\n         " +
                    "       {\n                    \"id\": 17,\n                    \"isBonus\": false,\n   " +
                    "                 \"description\": \"il doit choisir cette spécialisation au niveau " +
                    "1 et ne pourra en choisir d’autres par la suite\",\n                    \"flags\": [" +
                    "\n                        {\n                            \"type\": \"ONE_SPECIALITY\"," +
                    "\n                            \"data\": null\n                        }\n            " +
                    "        ]\n                },\n                {\n                    \"id\": 18,\n   " +
                    "                 \"isBonus\": false,\n                    \"description\": \"il doit f" +
                    "aire attention à la présence à ces côtés de créatures mortes : celles-ci peuvent" +
                    " attirer l’attention\",\n                    \"flags\": []\n                },\n      " +
                    "          {\n                    \"id\": 19,\n                    \"isBonus\": false,\n" +
                    "                    \"description\": \"il a souvent besoin de trouver des ingrédien" +
                    "ts et/ou de cadavres\",\n                    \"flags\": []\n                },\n      " +
                    "          {\n                    \"id\": 20,\n                    \"isBonus\": false,\n" +
                    "                    \"description\": \"en voyage à pieds, il avance à la vitesse de" +
                    " ses créatures, soit plutôt lentement\",\n                    \"flags\": []\n        " +
                    "        },\n                {\n                    \"id\": 21,\n                    \"" +
                    "isBonus\": true,\n                    \"description\": \"il n\'a pas forcément besoin " +
                    "d\'être adroit\",\n                    \"flags\": []\n                },\n             " +
                    "   {\n                    \"id\": 22,\n                    \"isBonus\": true,\n        " +
                    "            \"description\": \"il peut éviter de mettre sa vie en danger en faisant" +
                    " travailler les morts à sa place\",\n                    \"flags\": []\n             " +
                    "   },\n                {\n                    \"id\": 23,\n                    \"isBon" +
                    "us\": true,\n                    \"description\": \"il peut régénérer son énergie vit" +
                    "ale et son énergie astrale sans l’aide d’équipiers\",\n                    \"flags\"" +
                    ": []\n                },\n                {\n                    \"id\": 24,\n        " +
                    "            \"isBonus\": true,\n                    \"description\": \"il a accès au g" +
                    "rimoire de magie généraliste à chaque niveau, en plus de son propre grimoire de " +
                    "nécromancien\",\n                    \"flags\": []\n                }\n            ],\n" +
                    "            \"flags\": []\n        },\n        {\n            \"id\": 8,\n            \"n" +
                    "ame\": \"Magie de la Terre\",\n            \"description\": \"La magie de la terre peut" +
                    " sembler ridicule aux yeux de l\'ignorant. Pour certains, elle se rapproche de la" +
                    " béatitude\\nécologique des druides et des elfes sylvains coiffant les poneys sou" +
                    "s la lune argentée. Cependant, cette discipline n\'a rien\\nd\'une partie de rigola" +
                    "de. On y trouve des sortilèges très variés : camouflage, assistance, sorts offen" +
                    "sifs et défensifs,\\ninvocations, malédictions, pièges. En définitive, une offre " +
                    "très complète.\",\n            \"modifiers\": [],\n            \"specials\": [],\n      " +
                    "      \"flags\": []\n        }\n    ]\n}", ((TechTalk.SpecFlow.Table)(null)), "And ");
#line hidden
            this.ScenarioCleanup();
        }
    }
}
#pragma warning restore
#endregion
