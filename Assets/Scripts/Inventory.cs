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
        // first, find the seed
        int seedFound = -1;
        for (int i = 0; i < seedList.Count; i++)
        {
            if (seedList[i].seedType == seed.seedType)
            {
                seedFound = i;
            }
        }
        if ( seedFound != -1 ) // found the seed
        {
            seedList[seedFound].amount++;
        } else
        {
            seedList.Add(seed);
        }
    }

    public Seed GetItem(int slot)
    {
        return seedList[slot];
    }

    public Seed SetAmount(int slot, int value)
    {
        seedList[slot].amount = value;
        return seedList[slot];
    }

    public void RemoveItem(Seed seed)
    {
        for (int i = 0; i < seedList.Count; i++)
        {
            if (seedList[i].seedType == seed.seedType)
            {
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

    public override string ToString()
    {
        string[] buildArray = new string[seedList.Count];
        for (int i = 0; i < seedList.Count; i++)
        {
            buildArray[i] = seedList[i].seedType.ToString();
        }
        return string.Join(",", buildArray);
    }
}
