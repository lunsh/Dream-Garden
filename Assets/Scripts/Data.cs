using System.Collections;
using System.Collections.Generic;

public class Data
{
    public int hearts;
    public int[] plantIDs;
    public List<Plant> plants;
    public Inventory inventory;

    public List<Seed> seedsCommon;
    public List<Seed> seedsUncommon;
    public List<Seed> seedsRare;
    public List<Seed> seedsUltraRare;

    public int numSeedsBought = 0;
    public int numPlantPotsBought = 0;
    public int initialSeedCost = 100;
    public int initialPotCost = 1000;
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
                       textureName = "violafern" },
            new Seed { seedType = Seed.SeedType.ShearPalm, 
                       amount = 1,
                       discovered = false,
                       rarity = "Common",
                       preDescription = "Strange brown seed",
                       textureName = "shearpalm" },
            new Seed { seedType = Seed.SeedType.SerpentVine,
                       amount = 1,
                       discovered = false,
                       rarity = "Common",
                       preDescription = "Tiny dotted seed",
                       textureName = "serpentvine" },
            new Seed { seedType = Seed.SeedType.StripedLanceleaf, 
                       amount = 1,
                       discovered = false,
                       rarity = "Common",
                       preDescription = "Spikey green seed",
                       textureName = "stripedlanceleaf" },
            new Seed { seedType = Seed.SeedType.RazorPalm, 
                       amount = 1,
                       discovered = false,
                       rarity = "Common",
                       preDescription = "Smelly pungent seed",
                       textureName = "razorpalm" },
            new Seed { seedType = Seed.SeedType.ZeusBeard, 
                       amount = 1,
                       discovered = false,
                       rarity = "Common",
                       preDescription = "Ugly purple seed",
                       textureName = "zeusbeard" },
            new Seed { seedType = Seed.SeedType.Queenstail, 
                       amount = 1,
                       discovered = false,
                       rarity = "Common",
                       preDescription = "Cautious speckle seed",
                       textureName = "queenstail" },
            new Seed { seedType = Seed.SeedType.WaxyDiamondback,
                       amount = 1,
                       discovered = false,
                       rarity = "Common",
                       preDescription = "Large round seed",
                       textureName = "waxydiamondback" },
            new Seed { seedType = Seed.SeedType.Stonewart,
                       amount = 1,
                       discovered = false,
                       rarity = "Common",
                       preDescription = "Crisscross damp seed",
                       textureName = "stonewart" },
            new Seed { seedType = Seed.SeedType.Mirrorwood,
                       amount = 1,
                       discovered = false,
                       rarity = "Common",
                       preDescription = "Sharp deep seed",
                       textureName = "mirrorwood" }
        };
        seedsUncommon = new List<Seed>()
        {
            new Seed { seedType = Seed.SeedType.GrandSidewinder,
                       amount = 1,
                       discovered = false,
                       rarity = "Uncommon",
                       preDescription = "Thin coral seed",
                       textureName = "grandsidewinder" },
            new Seed { seedType = Seed.SeedType.Webweaver,
                       amount = 1,
                       discovered = false,
                       rarity = "Uncommon",
                       preDescription = "Spooky black seed",
                       textureName = "webweaver" },
            new Seed { seedType = Seed.SeedType.PaleWinterberry,
                       amount = 1,
                       discovered = false,
                       rarity = "Uncommon",
                       preDescription = "Large dotted seed",
                       textureName = "palewinterberry" },
            new Seed { seedType = Seed.SeedType.Goldenvine,
                       amount = 1,
                       discovered = false,
                       rarity = "Uncommon",
                       preDescription = "Nondescript brown seed",
                       textureName = "goldenvine" },
            new Seed { seedType = Seed.SeedType.AshenThyme,
                       amount = 1,
                       discovered = false,
                       rarity = "Uncommon",
                       preDescription = "Pink shiny seed",
                       textureName = "ashenthyme" },
            new Seed { seedType = Seed.SeedType.MilkySilkwater,
                       amount = 1,
                       discovered = false,
                       rarity = "Uncommon",
                       preDescription = "Soft forked seed",
                       textureName = "milkysilkwater" }
        };
        seedsRare = new List<Seed>()
        {
            new Seed { seedType = Seed.SeedType.Cloudsprig,
                       amount = 1,
                       discovered = false,
                       rarity = "Rare",
                       preDescription = "Feathery light seed",
                       textureName = "cloudsprig" },
            new Seed { seedType = Seed.SeedType.Sunspear,
                       amount = 1,
                       discovered = false,
                       rarity = "Rare",
                       preDescription = "Cheeful bright seed",
                       textureName = "sunspear" },
            new Seed { seedType = Seed.SeedType.SpindlesofHeaven,
                       amount = 1,
                       discovered = false,
                       rarity = "Rare",
                       preDescription = "Deceitful knowing seed",
                       textureName = "spindlesofheaven" },
            new Seed { seedType = Seed.SeedType.JillintheBush,
                       amount = 1,
                       discovered = false,
                       rarity = "Rare",
                       preDescription = "Tiny orange seed",
                       textureName = "jillinthebush" },
            new Seed { seedType = Seed.SeedType.BloodClover,
                       amount = 1,
                       discovered = false,
                       rarity = "Rare",
                       preDescription = "Weird striped seed",
                       textureName = "bloodclover" },
            new Seed { seedType = Seed.SeedType.TitianCitrus,
                       amount = 1,
                       discovered = false,
                       rarity = "Rare",
                       preDescription = "Juicy round seed",
                       textureName = "titiancitrus" }
        };
        seedsUltraRare = new List<Seed>()
        {
            new Seed { seedType = Seed.SeedType.VioletSugarplum,
                       amount = 1,
                       discovered = false,
                       rarity = "Ultrarare",
                       preDescription = "Bold striped seed",
                       textureName = "violetsugarplum" },
            new Seed { seedType = Seed.SeedType.SpinyHellebore,
                       amount = 1,
                       discovered = false,
                       rarity = "Ultrarare",
                       preDescription = "Violent drop seed",
                       textureName = "spinyhellebore" },
            new Seed { seedType = Seed.SeedType.ChillySpindleweed,
                       amount = 1,
                       discovered = false,
                       rarity = "Ultrarare",
                       preDescription = "Stripey purple seed",
                       textureName = "chillyspindleweed" }
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
