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
                       preDescription = "Small pink seed",
                       textureName = "violafern" },
            new Seed { seedType = Seed.SeedType.ShearPalm, 
                       amount = 1,
                       discovered = false,
                       preDescription = "Strange brown seed",
                       textureName = "shearpalm" },
            new Seed { seedType = Seed.SeedType.SerpentVine,
                       amount = 1,
                       discovered = false,
                       preDescription = "Tiny dotted seed",
                       textureName = "serpentvine" },
            new Seed { seedType = Seed.SeedType.StripedLanceleaf, 
                       amount = 1,
                       discovered = false,
                       preDescription = "Spikey green seed",
                       textureName = "stripedlanceleaf" },
            new Seed { seedType = Seed.SeedType.RazorPalm, 
                       amount = 1,
                       discovered = false,
                       preDescription = "Smelly pungent seed",
                       textureName = "razorpalm" },
            new Seed { seedType = Seed.SeedType.ZeusBeard, 
                       amount = 1,
                       discovered = false,
                       preDescription = "Ugly purple seed",
                       textureName = "zeusbeard" },
            new Seed { seedType = Seed.SeedType.Queenstail, 
                       amount = 1,
                       discovered = false,
                       preDescription = "Feathery light seed",
                       textureName = "queenstail" }
        };
        seedsUncommon = new List<Seed>();
        seedsRare = new List<Seed>();
        seedsUltraRare = new List<Seed>();
    }
}
