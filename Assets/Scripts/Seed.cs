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
        Webweaver,
        WaxyDiamondback,
        SpindlesofHeaven,
        JillintheBush,
        BloodClover,
        PaleWinterberry,
        SpinyHellebore,
        Goldenvine,
        TitianCitrus,
        ChillySpindleweed,
        AshenThyme,
        MilkySilkwater,
        Stonewart,
        Mirrorwood
    }

    public SeedType seedType;
    public int amount;
    public bool discovered;
    public string rarity;
    public string preDescription;
    public string textureName;
    public float timeToGrow1;
    public float timeToGrow2;
    public int heartsGenerate1;
    public int heartsGenerate2;
    public int heartsGenerate3;
    public int id;
    public string name;
    public float timeToWilt;
    public string description;
}
