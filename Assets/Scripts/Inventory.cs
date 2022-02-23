using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory
{
    private List<Seed> seedList;

    public Inventory()
    {
        seedList = new List<Seed>();

        AddItem(new Seed { seedType = Seed.SeedType.ViolaFern, amount = 1 });
        Debug.Log(seedList.Count);
    }

    public void AddItem(Seed seed)
    {
        seedList.Add(seed);
    }
}
