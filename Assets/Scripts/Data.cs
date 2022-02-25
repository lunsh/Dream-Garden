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
            new Seed { seedType = Seed.SeedType.ViolaFern, amount = 1 }
        };
        seedsUncommon = new List<Seed>();
        seedsRare = new List<Seed>();
        seedsUltraRare = new List<Seed>();
    }
}
