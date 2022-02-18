using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Plant : MonoBehaviour
{
    public int plantID;
    public bool active;
    public Image sprout;
    public int DelayAmount = 1; // 1 second for timer
    public float Timer;

    public void Start()
    {
        active = PlayerPrefs.GetInt("plant" + plantID + "active", 0) == 0 ? false : true;
    }

    public void sowSeed()
    {
        // no seed yet, plant the seed
        if (!active)
        {
            active = true;
            PlayerPrefs.SetInt("plant" + plantID + "active", 1);
            print("sowing seed");
            sprout.sprite = Resources.Load<Sprite>("violafern-baby");
            var tempColor = sprout.color;
            tempColor.a = 1f;
            sprout.color = tempColor;
        }
    }
}
