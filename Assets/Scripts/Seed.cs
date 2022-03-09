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
    public bool discovered;
    public string preDescription;
    public string textureName;
    /*public float timeToAccumulateHeart;
    public float timeToWilt1;
    public float timeToWilt2;
    public float timeToGrow1;
    public float timeToGrow2;
    public string description;
    */
}
