using System.Collections;
using System.Collections.Generic;

public class Data
{
    public int hearts;
    public int[] plantIDs;
    public List<Plant> plants;
    public Inventory inventory;
    public string[] seedsDiscovered;
    public int[] customizeUnlock;
    public string[] customizeColors;

    public string hemisphere;

    public List<Seed> seedsCommon;
    public List<Seed> seedsUncommon;
    public List<Seed> seedsRare;
    public List<Seed> seedsUltraRare;

    public int numSeedsBought = 0;
    public int numPlantPotsBought = 0;
    public int initialSeedCost = 100;
    public int initialPotCost = 500;
    public int seedIncrease = 50;

    public Data()
    {
        hearts = 0;
        plantIDs = new int[] { -1, -1, -1 };
        customizeUnlock = new int[] { 0, 0, 0 };
        customizeColors = new string[] { "FFFFFF", "FFFFFF", "FFFFFF" };
        plants = Methods.CreateList<Plant>(3);
        inventory = new Inventory();
        seedsDiscovered = new string[25];

        // Set up seed databases
        seedsCommon = new List<Seed>()
        {
            new Seed { seedType = Seed.SeedType.ViolaFern,
                       amount = 1,
                       discovered = false,
                       rarity = "Common",
                       preDescription = "Large pink seed",
                       textureName = "violafern",
                       timeToGrow1 = 420f,
                       timeToGrow2 = 900f,
                       id = 0,
                       name = "Viola Fern",
                       description = "A small leafed yet tall bodied fern with a well-defined stem structure. It is at its happiest in warm shadows.",
                       heartsGenerate1 = 20,
                       heartsGenerate2 = 17,
                       heartsGenerate3 = 15,
                       timeToWilt = 600f },
            new Seed { seedType = Seed.SeedType.ShearPalm, 
                       amount = 1,
                       discovered = false,
                       rarity = "Common",
                       preDescription = "Strange brown seed",
                       textureName = "shearpalm",
                       timeToGrow1 = 900f,
                       timeToGrow2 = 600f,
                       id = 1,
                       name = "Shear Palm",
                       description = "With waxy serrated leaves, the shear palm flourishes in many places. Its roots twist, turn, and run deep to find water.",
                       heartsGenerate1 = 15,
                       heartsGenerate2 = 13,
                       heartsGenerate3 = 10,
                       timeToWilt = 780f  },
            new Seed { seedType = Seed.SeedType.SerpentVine,
                       amount = 1,
                       discovered = false,
                       rarity = "Common",
                       preDescription = "Tiny dotted seed",
                       textureName = "serpentvine",
                       timeToGrow1 = 600f,
                       timeToGrow2 = 600f,
                       id = 2,
                       name = "Serpent Vine",
                       description = "White and green dappled patterns snake across blade-like leaves. Despite sharp-looking edges, it is soft to the touch.",
                       heartsGenerate1 = 15,
                       heartsGenerate2 = 12,
                       heartsGenerate3 = 10,
                       timeToWilt = 720f  },
            new Seed { seedType = Seed.SeedType.StripedLanceleaf, 
                       amount = 1,
                       discovered = false,
                       rarity = "Common",
                       preDescription = "Spikey green seed",
                       textureName = "stripedlanceleaf",
                       timeToGrow1 = 420f,
                       timeToGrow2 = 900f,
                       id = 3,
                       name = "Striped Lanceleaf",
                       description = "While modest of build, striped lanceleaf has robust foliage and roots. It is said that its stems have medicinal qualities.",
                       heartsGenerate1 = 20,
                       heartsGenerate2 = 18,
                       heartsGenerate3 = 15,
                       timeToWilt = 900f  },
            new Seed { seedType = Seed.SeedType.RazorPalm, 
                       amount = 1,
                       discovered = false,
                       rarity = "Common",
                       preDescription = "Smelly pungent seed",
                       textureName = "razorpalm",
                       timeToGrow1 = 600f,
                       timeToGrow2 = 900f,
                       id = 4,
                       name = "Razor Palm",
                       description = "The razor-like leaf edges are like the sharpest knife, yet the broadside is smooth and waxy. Handle with great care.",
                       heartsGenerate1 = 17,
                       heartsGenerate2 = 15,
                       heartsGenerate3 = 12,
                       timeToWilt = 840f  },
            new Seed { seedType = Seed.SeedType.ZeusBeard, 
                       amount = 1,
                       discovered = false,
                       rarity = "Common",
                       preDescription = "Ugly purple seed",
                       textureName = "zeusbeard",
                       timeToGrow1 = 420f,
                       timeToGrow2 = 900f,
                       id = 5,
                       name = "Zeus Beard",
                       description = "It has a branches like iron with leaves strong enough to scratch diamonds. Strangely enough, it may be used as tea.",
                       heartsGenerate1 = 20,
                       heartsGenerate2 = 17,
                       heartsGenerate3 = 14,
                       timeToWilt = 600f  },
            new Seed { seedType = Seed.SeedType.Queenstail, 
                       amount = 1,
                       discovered = false,
                       rarity = "Common",
                       preDescription = "Cautious speckle seed",
                       textureName = "queenstail",
                       timeToGrow1 = 600f,
                       timeToGrow2 = 900f,
                       id = 6,
                       name = "Queenstail",
                       description = "Thin and light, its soft-tailed ends and stems enjoy swaying in the wind. The tailed ends are like the most royal velvet.",
                       heartsGenerate1 = 15,
                       heartsGenerate2 = 13,
                       heartsGenerate3 = 11,
                       timeToWilt = 660f  },
            new Seed { seedType = Seed.SeedType.WaxyDiamondback,
                       amount = 1,
                       discovered = false,
                       rarity = "Common",
                       preDescription = "Large round seed",
                       textureName = "waxydiamondback",
                       timeToGrow1 = 900f,
                       timeToGrow2 = 900f,
                       id = 7,
                       name = "Waxy Diamondback",
                       description = "Small clusters of waxy petals curl together in bunches. Their dried leaves make ideal candlewax, like captured sunlight.",
                       heartsGenerate1 = 17,
                       heartsGenerate2 = 15,
                       heartsGenerate3 = 13,
                       timeToWilt = 840f  },
            new Seed { seedType = Seed.SeedType.Stonewart,
                       amount = 1,
                       discovered = false,
                       rarity = "Common",
                       preDescription = "Crisscross damp seed",
                       textureName = "stonewart",
                       timeToGrow1 = 420f,
                       timeToGrow2 = 600f,
                       id = 8,
                       name = "Stonewart",
                       description = "Many have thought it belongs in the ocean, with small leaves wavering like tendrils. Its pebble-like seeds are able to float.",
                       heartsGenerate1 = 20,
                       heartsGenerate2 = 17,
                       heartsGenerate3 = 15,
                       timeToWilt = 1020f  },
            new Seed { seedType = Seed.SeedType.Mirrorwood,
                       amount = 1,
                       discovered = false,
                       rarity = "Common",
                       preDescription = "Sharp deep seed",
                       textureName = "mirrorwood",
                       timeToGrow1 = 600f,
                       timeToGrow2 = 900f,
                       id = 9,
                       name = "Mirrorwood",
                       description = "It has been said the mirrorwood's dark bitter bark allows miraculous healing. Legend holds to avoid mirrors after use.",
                       heartsGenerate1 = 20,
                       heartsGenerate2 = 18,
                       heartsGenerate3 = 16,
                       timeToWilt = 960f  }
        };
        seedsUncommon = new List<Seed>()
        {
            new Seed { seedType = Seed.SeedType.GrandSidewinder,
                       amount = 1,
                       discovered = false,
                       rarity = "Uncommon",
                       preDescription = "Thin coral seed",
                       textureName = "grandsidewinder",
                       timeToGrow1 = 900f,
                       timeToGrow2 = 900f,
                       id = 10,
                       name = "Grand Sidewinder",
                       description = "Contrary to its name, the sidewinder trunk is straight as an arrow. The pollen, however, twists and turns wildly in the wind. ",
                       heartsGenerate1 = 15,
                       heartsGenerate2 = 13,
                       heartsGenerate3 = 10,
                       timeToWilt = 1200f  },
            new Seed { seedType = Seed.SeedType.Webweaver,
                       amount = 1,
                       discovered = false,
                       rarity = "Uncommon",
                       preDescription = "Spooky black seed",
                       textureName = "webweaver",
                       timeToGrow1 = 900f,
                       timeToGrow2 = 1200f,
                       id = 11,
                       name = "Webweaver",
                       description = "Leaves like spindles reach skyward, yearning to catch the sun in its embrace. Its leaves are sticky like a orb-spider's web.",
                       heartsGenerate1 = 10,
                       heartsGenerate2 = 8,
                       heartsGenerate3 = 7,
                       timeToWilt = 1500f  },
            new Seed { seedType = Seed.SeedType.PaleWinterberry,
                       amount = 1,
                       discovered = false,
                       rarity = "Uncommon",
                       preDescription = "Large dotted seed",
                       textureName = "palewinterberry",
                       timeToGrow1 = 1200f,
                       timeToGrow2 = 900f,
                       id = 12,
                       name = "Pale Winterberry",
                       description = "The winterberry is a strange, cold-loving plant that bears fruit year-round. Its berries always seem to carry winter's chill.",
                       heartsGenerate1 = 15,
                       heartsGenerate2 = 12,
                       heartsGenerate3 = 10,
                       timeToWilt = 1320f  },
            new Seed { seedType = Seed.SeedType.Goldenvine,
                       amount = 1,
                       discovered = false,
                       rarity = "Uncommon",
                       preDescription = "Nondescript brown seed",
                       textureName = "goldenvine",
                       timeToGrow1 = 900f,
                       timeToGrow2 = 600f,
                       id = 13,
                       name = "Goldenvine",
                       description = "With flowers like spun-gold, the goldenvine is often pressed and dried in memory. Echoes of a half-remembered summer.",
                       heartsGenerate1 = 13,
                       heartsGenerate2 = 11,
                       heartsGenerate3 = 9,
                       timeToWilt = 1800f  },
            new Seed { seedType = Seed.SeedType.AshenThyme,
                       amount = 1,
                       discovered = false,
                       rarity = "Uncommon",
                       preDescription = "Pink shiny seed",
                       textureName = "ashenthyme",
                       timeToGrow1 = 900f,
                       timeToGrow2 = 900f,
                       id = 14,
                       name = "Ashen Thyme",
                       description = "Small and potent, ashen thyme carries with it the weight of time. It has small flowers that bloom when least expected.",
                       heartsGenerate1 = 13,
                       heartsGenerate2 = 11,
                       heartsGenerate3 = 9,
                       timeToWilt = 1500f  },
            new Seed { seedType = Seed.SeedType.MilkySilkwater,
                       amount = 1,
                       discovered = false,
                       rarity = "Uncommon",
                       preDescription = "Soft forked seed",
                       textureName = "milkysilkwater",
                       timeToGrow1 = 1200f,
                       timeToGrow2 = 900f,
                       id = 15,
                       name = "Milky Silkwater",
                       description = "Grape-like flower clusters are the silkwater's hallmark feature. It usually grows close to the ocean and prefers white sand.",
                       heartsGenerate1 = 10,
                       heartsGenerate2 = 8,
                       heartsGenerate3 = 7,
                       timeToWilt = 1140f  }
        };
        seedsRare = new List<Seed>()
        {
            new Seed { seedType = Seed.SeedType.Cloudsprig,
                       amount = 1,
                       discovered = false,
                       rarity = "Rare",
                       preDescription = "Feathery light seed",
                       textureName = "cloudsprig",
                       timeToGrow1 = 900f,
                       timeToGrow2 = 1200f,
                       id = 16,
                       name = "Cloudsprig",
                       description = "Small multi-faceted sprigs adorn each leaf. After maturing, the sprigs sprout small wings and may drift continents away.",
                       heartsGenerate1 = 7,
                       heartsGenerate2 = 6,
                       heartsGenerate3 = 4,
                       timeToWilt = 1800f  },
            new Seed { seedType = Seed.SeedType.Sunspear,
                       amount = 1,
                       discovered = false,
                       rarity = "Rare",
                       preDescription = "Cheerful bright seed",
                       textureName = "sunspear",
                       timeToGrow1 = 1200f,
                       timeToGrow2 = 1200f,
                       id = 17,
                       name = "Sunspear",
                       description = "Bright as a noonday sun, the sunspear has long stems with large flower bells. Its flowers give light even in darkest night.",
                       heartsGenerate1 = 5,
                       heartsGenerate2 = 4,
                       heartsGenerate3 = 3,
                       timeToWilt = 2100f  },
            new Seed { seedType = Seed.SeedType.SpindlesofHeaven,
                       amount = 1,
                       discovered = false,
                       rarity = "Rare",
                       preDescription = "Deceitful knowing seed",
                       textureName = "spindlesofheaven",
                       timeToGrow1 = 1200f,
                       timeToGrow2 = 900f,
                       id = 18,
                       name = "Spindles of Heaven",
                       description = "With leaves like naked flames, these spindles superheat the surrounding air at night. During the day, they are stone cold.",
                       heartsGenerate1 = 7,
                       heartsGenerate2 = 5,
                       heartsGenerate3 = 3,
                       timeToWilt = 2100f  },
            new Seed { seedType = Seed.SeedType.JillintheBush,
                       amount = 1,
                       discovered = false,
                       rarity = "Rare",
                       preDescription = "Tiny orange seed",
                       textureName = "jillinthebush",
                       timeToGrow1 = 1500f,
                       timeToGrow2 = 600f,
                       id = 19,
                       name = "Jill in the Bush",
                       description = "Its flowers are like champagne flutes, softly featured but undeniable in truth. The nectar is irresistible to nearly all bees.",
                       heartsGenerate1 = 5,
                       heartsGenerate2 = 3,
                       heartsGenerate3 = 2,
                       timeToWilt = 2400f  },
            new Seed { seedType = Seed.SeedType.BloodClover,
                       amount = 1,
                       discovered = false,
                       rarity = "Rare",
                       preDescription = "Weird striped seed",
                       textureName = "bloodclover",
                       timeToGrow1 = 1200f,
                       timeToGrow2 = 1020f,
                       id = 20,
                       name = "Blood Clover",
                       description = "The bloodclover has triangular leaves with deep sunset-colored flowers. It is said to bloom only on summer's longest day.",
                       heartsGenerate1 = 5,
                       heartsGenerate2 = 4,
                       heartsGenerate3 = 3,
                       timeToWilt = 2700f  },
            new Seed { seedType = Seed.SeedType.TitianCitrus,
                       amount = 1,
                       discovered = false,
                       rarity = "Rare",
                       preDescription = "Juicy round seed",
                       textureName = "titiancitrus",
                       timeToGrow1 = 900f,
                       timeToGrow2 = 900f,
                       id = 21,
                       name = "Titian Citrus",
                       description = "Many flowering plants offer one kind of fruit but the titian is unique. It can bear any fruit of the seed planted with it.",
                       heartsGenerate1 = 6,
                       heartsGenerate2 = 4,
                       heartsGenerate3 = 3,
                       timeToWilt = 2400f  }
        };
        seedsUltraRare = new List<Seed>()
        {
            new Seed { seedType = Seed.SeedType.VioletSugarplum,
                       amount = 1,
                       discovered = false,
                       rarity = "Ultrarare",
                       preDescription = "Bold striped seed",
                       textureName = "violetsugarplum",
                       timeToGrow1 = 1500f,
                       timeToGrow2 = 2100f,
                       id = 22,
                       name = "Violet Sugarplum",
                       description = "Strange and beautiful, the sugarplum bears the sweetest fruit. It may only be found once a century before disappearing.",
                       heartsGenerate1 = 3,
                       heartsGenerate2 = 2,
                       heartsGenerate3 = 1,
                       timeToWilt = 3600f  },
            new Seed { seedType = Seed.SeedType.SpinyHellebore,
                       amount = 1,
                       discovered = false,
                       rarity = "Ultrarare",
                       preDescription = "Violent drop seed",
                       textureName = "spinyhellebore",
                       timeToGrow1 = 1800f,
                       timeToGrow2 = 1200f,
                       id = 23,
                       name = "Spiny Hellebore",
                       description = "Dangerous and otherworldly, this small plant is among the rarest of spindled plants. Its flowers smell faintly of brimstone.",
                       heartsGenerate1 = 4,
                       heartsGenerate2 = 3,
                       heartsGenerate3 = 2,
                       timeToWilt = 4800f  },
            new Seed { seedType = Seed.SeedType.ChillySpindleweed,
                       amount = 1,
                       discovered = false,
                       rarity = "Ultrarare",
                       preDescription = "Stripey purple seed",
                       textureName = "chillyspindleweed",
                       timeToGrow1 = 1500f,
                       timeToGrow2 = 1500f,
                       id = 24,
                       name = "Chilly Spindleweed",
                       description = "Glacial and vivid, its leaves and berries shine brightly among dark branches. It carries the essence of winter, cold everlasting.",
                       heartsGenerate1 = 3,
                       heartsGenerate2 = 2,
                       heartsGenerate3 = 1,
                       timeToWilt = 3300f  }
        };
    }

    public Seed findSeed(string seedName)
    {
        for (int j = 0; j < seedsCommon.Count; j++)
        {
            if (seedsCommon[j].seedType.ToString() == seedName)
            {
                return seedsCommon[j];
            }
        }
        for (int j = 0; j < seedsUncommon.Count; j++)
        {
            if (seedsUncommon[j].seedType.ToString() == seedName)
            {
                return seedsUncommon[j];
            }
        }
        for (int j = 0; j < seedsRare.Count; j++)
        {
            if (seedsRare[j].seedType.ToString() == seedName)
            {
                return seedsRare[j];
            }
        }
        for (int j = 0; j < seedsUltraRare.Count; j++)
        {
            if (seedsUltraRare[j].seedType.ToString() == seedName)
            {
                return seedsUltraRare[j];
            }
        }
        return null;
    }
}
