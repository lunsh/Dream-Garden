using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Seed
{
    public enum SeedType
    {
        ViolaFern,
        ShearPalm,
        SerpentVine,
        StripedLanceleaf,
        RazorPalm,
        ZeusBeard,
        Cloudsprig,
        Sunspear,
        VioletSugarplum,
        Queenstail,
        GrandSidewinder,
    }

    public SeedType seedType;
    public int amount;
}
