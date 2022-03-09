using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory
{
    private List<Seed> seedList;

    public Inventory()
    {
        seedList = new List<Seed>();

        //AddItem(new Seed { seedType = Seed.SeedType.ViolaFern, amount = 1 });
        //Debug.Log(seedList.Count);
    }

    public int GetCount()
    {
        return seedList.Count;
    }

    public void AddItem(Seed seed)
    {
        seedList.Add(seed);
    }

    public Seed GetItem(int slot)
    {
        return seedList[slot];
    }

    public void RemoveItem(Seed seed)
    {
        for (int i = 0; i < seedList.Count; i++)
        {
            if (seedList[i].seedType == seed.seedType)
            {
                Debug.Log("found it");
                if ( seedList[i].amount > 1 ) // you have more than 1 so just decrease the amount
                {
                    seedList[i].amount--;
                } else
                {
                    seedList.RemoveAt(i);
                }
            }
        }
    }
}
