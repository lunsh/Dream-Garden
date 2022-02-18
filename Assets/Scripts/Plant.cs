using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Plant : MonoBehaviour
{
    public bool active;
    public Image sprout;
    public int DelayAmount = 1; // 1 second for timer
    protected float Timer;

    public void Start()
    {
        active = false;
    }

    public void sowSeed()
    {
        // no seed yet, plant the seed
        if (! active)
        {
            active = true;
            print("sowing seed");
            sprout.sprite = Resources.Load<Sprite>("violafern-baby");
            var tempColor = sprout.color;
            tempColor.a = 1f;
            sprout.color = tempColor;
        }
    }

    private void Update()
    {
        Timer += Time.deltaTime;

        if ( Timer >= DelayAmount)
        {
            Timer = 0f;
        }
    }
}
