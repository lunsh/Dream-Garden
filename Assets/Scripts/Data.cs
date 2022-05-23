using System.Collections;
using System.Collections.Generic;

public class Data
{
    public int hearts;
    public int[] plantIDs;
    public List<Plant> plants;
    public Inventory inventory;

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
        plants = Methods.CreateList<Plant>(3);
        inventory = new Inventory();

        // Set up seed databases
        seedsCommon = new List<Seed>()
        {
            new Seed { seedType = Seed.SeedType.ViolaFern,
                       amount = 1,
                       discovered = false,
                       rarity = "Common",
                       preDescription = "Large pink seed",
                       textureName = "violafern",
                       timeToGrow1 = 1200f,
                       timeToGrow2 = 1800f},
            new Seed { seedType = Seed.SeedType.ShearPalm, 
                       amount = 1,
                       discovered = false,
                       rarity = "Common",
                       preDescription = "Strange brown seed",
                       textureName = "shearpalm",
                       timeToGrow1 = 1800f,
                       timeToGrow2 = 5400f },
            new Seed { seedType = Seed.SeedType.SerpentVine,
                       amount = 1,
                       discovered = false,
                       rarity = "Common",
                       preDescription = "Tiny dotted seed",
                       textureName = "serpentvine",
                       timeToGrow1 = 600f,
                       timeToGrow2 = 3600f },
            new Seed { seedType = Seed.SeedType.StripedLanceleaf, 
                       amount = 1,
                       discovered = false,
                       rarity = "Common",
                       preDescription = "Spikey green seed",
                       textureName = "stripedlanceleaf",
                       timeToGrow1 = 900f,
                       timeToGrow2 = 1800f },
            new Seed { seedType = Seed.SeedType.RazorPalm, 
                       amount = 1,
                       discovered = false,
                       rarity = "Common",
                       preDescription = "Smelly pungent seed",
                       textureName = "razorpalm",
                       timeToGrow1 = 2400f,
                       timeToGrow2 = 5400f },
            new Seed { seedType = Seed.SeedType.ZeusBeard, 
                       amount = 1,
                       discovered = false,
                       rarity = "Common",
                       preDescription = "Ugly purple seed",
                       textureName = "zeusbeard",
                       timeToGrow1 = 1200f,
                       timeToGrow2 = 1800f },
            new Seed { seedType = Seed.SeedType.Queenstail, 
                       amount = 1,
                       discovered = false,
                       rarity = "Common",
                       preDescription = "Cautious speckle seed",
                       textureName = "queenstail",
                       timeToGrow1 = 1200f,
                       timeToGrow2 = 2400f },
            new Seed { seedType = Seed.SeedType.WaxyDiamondback,
                       amount = 1,
                       discovered = false,
                       rarity = "Common",
                       preDescription = "Large round seed",
                       textureName = "waxydiamondback",
                       timeToGrow1 = 1800f,
                       timeToGrow2 = 1800f },
            new Seed { seedType = Seed.SeedType.Stonewart,
                       amount = 1,
                       discovered = false,
                       rarity = "Common",
                       preDescription = "Crisscross damp seed",
                       textureName = "stonewart",
                       timeToGrow1 = 900f,
                       timeToGrow2 = 2400f },
            new Seed { seedType = Seed.SeedType.Mirrorwood,
                       amount = 1,
                       discovered = false,
                       rarity = "Common",
                       preDescription = "Sharp deep seed",
                       textureName = "mirrorwood",
                       timeToGrow1 = 1200f,
                       timeToGrow2 = 2700f }
        };
        seedsUncommon = new List<Seed>()
        {
            new Seed { seedType = Seed.SeedType.GrandSidewinder,
                       amount = 1,
                       discovered = false,
                       rarity = "Uncommon",
                       preDescription = "Thin coral seed",
                       textureName = "grandsidewinder",
                       timeToGrow1 = 1200f,
                       timeToGrow2 = 2700f },
            new Seed { seedType = Seed.SeedType.Webweaver,
                       amount = 1,
                       discovered = false,
                       rarity = "Uncommon",
                       preDescription = "Spooky black seed",
                       textureName = "webweaver",
                       timeToGrow1 = 1200f,
                       timeToGrow2 = 2700f },
            new Seed { seedType = Seed.SeedType.PaleWinterberry,
                       amount = 1,
                       discovered = false,
                       rarity = "Uncommon",
                       preDescription = "Large dotted seed",
                       textureName = "palewinterberry",
                       timeToGrow1 = 1200f,
                       timeToGrow2 = 2700f },
            new Seed { seedType = Seed.SeedType.Goldenvine,
                       amount = 1,
                       discovered = false,
                       rarity = "Uncommon",
                       preDescription = "Nondescript brown seed",
                       textureName = "goldenvine",
                       timeToGrow1 = 1200f,
                       timeToGrow2 = 2700f },
            new Seed { seedType = Seed.SeedType.AshenThyme,
                       amount = 1,
                       discovered = false,
                       rarity = "Uncommon",
                       preDescription = "Pink shiny seed",
                       textureName = "ashenthyme",
                       timeToGrow1 = 1200f,
                       timeToGrow2 = 2700f },
            new Seed { seedType = Seed.SeedType.MilkySilkwater,
                       amount = 1,
                       discovered = false,
                       rarity = "Uncommon",
                       preDescription = "Soft forked seed",
                       textureName = "milkysilkwater",
                       timeToGrow1 = 1200f,
                       timeToGrow2 = 2700f }
        };
        seedsRare = new List<Seed>()
        {
            new Seed { seedType = Seed.SeedType.Cloudsprig,
                       amount = 1,
                       discovered = false,
                       rarity = "Rare",
                       preDescription = "Feathery light seed",
                       textureName = "cloudsprig",
                       timeToGrow1 = 1200f,
                       timeToGrow2 = 2700f },
            new Seed { seedType = Seed.SeedType.Sunspear,
                       amount = 1,
                       discovered = false,
                       rarity = "Rare",
                       preDescription = "Cheeful bright seed",
                       textureName = "sunspear",
                       timeToGrow1 = 1200f,
                       timeToGrow2 = 2700f },
            new Seed { seedType = Seed.SeedType.SpindlesofHeaven,
                       amount = 1,
                       discovered = false,
                       rarity = "Rare",
                       preDescription = "Deceitful knowing seed",
                       textureName = "spindlesofheaven",
                       timeToGrow1 = 1200f,
                       timeToGrow2 = 2700f },
            new Seed { seedType = Seed.SeedType.JillintheBush,
                       amount = 1,
                       discovered = false,
                       rarity = "Rare",
                       preDescription = "Tiny orange seed",
                       textureName = "jillinthebush",
                       timeToGrow1 = 1200f,
                       timeToGrow2 = 2700f },
            new Seed { seedType = Seed.SeedType.BloodClover,
                       amount = 1,
                       discovered = false,
                       rarity = "Rare",
                       preDescription = "Weird striped seed",
                       textureName = "bloodclover",
                       timeToGrow1 = 1200f,
                       timeToGrow2 = 2700f },
            new Seed { seedType = Seed.SeedType.TitianCitrus,
                       amount = 1,
                       discovered = false,
                       rarity = "Rare",
                       preDescription = "Juicy round seed",
                       textureName = "titiancitrus",
                       timeToGrow1 = 1200f,
                       timeToGrow2 = 2700f }
        };
        seedsUltraRare = new List<Seed>()
        {
            new Seed { seedType = Seed.SeedType.VioletSugarplum,
                       amount = 1,
                       discovered = false,
                       rarity = "Ultrarare",
                       preDescription = "Bold striped seed",
                       textureName = "violetsugarplum",
                       timeToGrow1 = 1200f,
                       timeToGrow2 = 2700f },
            new Seed { seedType = Seed.SeedType.SpinyHellebore,
                       amount = 1,
                       discovered = false,
                       rarity = "Ultrarare",
                       preDescription = "Violent drop seed",
                       textureName = "spinyhellebore",
                       timeToGrow1 = 1200f,
                       timeToGrow2 = 2700f },
            new Seed { seedType = Seed.SeedType.ChillySpindleweed,
                       amount = 1,
                       discovered = false,
                       rarity = "Ultrarare",
                       preDescription = "Stripey purple seed",
                       textureName = "chillyspindleweed",
                       timeToGrow1 = 1200f,
                       timeToGrow2 = 2700f }
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
