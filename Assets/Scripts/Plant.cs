using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Plant : MonoBehaviour
{
    private GameObject controllerObj;
    private Controller controller;

    public int plantID;
    public bool active;
    public Image sprout;
    public int HeartTimer;
    public float Timer;
    private int count;
    public float GrowthTimer;
    public Seed activeSeed;
    public int stage;
    /* wilting */
    public float WiltTimer; // overall timer for wilting, used to also unwilt
    private float BeginWiltTime;
    private float FullyWiltTime;
    public float WiltDuration; // duration between beginning of wilt and end of wilt
    public int wiltStage;
    public Color wiltColor;
    public Color normalColor;
    private bool wiltShaking;
    private float tempAlpha;
    private float unWiltTime;

    [SerializeField] private GameObject uiInactiveMenu;
    [SerializeField] private GameObject uiActiveMenu;
    [SerializeField] private GameObject uiSeedSelect;
    [SerializeField] private GameObject uiNoSeedText;
    [SerializeField] private Transform uiInventoryContent;
    [SerializeField] private Transform seedButtonPrefab;
    [SerializeField] public Transform animationHolder;
    [SerializeField] public Transform heartAnimation;
    [SerializeField] private CanvasGroup wiltIcon;
    [SerializeField] private Button tendButton;
    [SerializeField] private Button harvestButton;
    [SerializeField] private GameObject harvestConfirm;
    [SerializeField] private GameObject sparkle;

    public Animator wiggleAnimation;

    public void Awake()
    {
        HeartTimer = 2; //todo: fix according to actual heartTimer from seed
        wiltStage = -1;
    }

    public void Start()
    {
        controllerObj = GameObject.Find("/Scripts/Controller");
        controller = controllerObj.GetComponent<Controller>();
        wiltShaking = true;
        tempAlpha = 0;

        // set wilt color
        float H, S, V;
        normalColor = Color.white;
        wiltColor = normalColor;
        wiltColor.r = wiltColor.r * 1.5f;
        wiltColor.g = wiltColor.g * 1.0f;
        wiltColor.b = 0f;
        Color.RGBToHSV(wiltColor, out H, out S, out V);
        S = 1f;
        V = 1f;
        wiltColor = Color.HSVToRGB(H, S, V);

        // temporary wilttime set
        float WiltTime = 100f;
        BeginWiltTime = WiltTime * 0.75f;
        FullyWiltTime = WiltTime;
    }

    public void Update()
    {
        if ( wiltStage != -1 && (wiltStage == 0 || wiltStage == 1) )
        {
            if (stage == 0)
            {
                growToTeen(activeSeed.timeToGrow1);
            }
            else if (stage == 1)
            {
                growToAdult(activeSeed.timeToGrow2);
            }
            wilt();
        } 
        if ( wiltShaking == true && wiltStage == 2)
        {
            StartCoroutine(wiltIconShake(Random.Range(1f, 5f)));
        }
        if ( wiltStage == 4 )
        {
            unwilt();
        }
        if (wiltStage >= 1 && wiltStage <= 3)
        {
            tendButton.interactable = true;
        }
    }

    public void setupPlantFromSave()
    {
        if ( wiltStage == 1 )
        {
            HeartTimer = HeartTimer * 2;
            normalColor = sprout.color; // temporarily set the "normal" color to the current color
            tempAlpha = WiltDuration / (FullyWiltTime - WiltTimer);
            wiltIcon.alpha = tempAlpha;
            harvestButton.interactable = false;
        } else if ( wiltStage == 2 )
        {
            wiltIcon.alpha = 1;
            HeartTimer = 0;
            harvestButton.interactable = false;
        }
    }

    public void plantButton()
    {
        if ( ! active )
        {
            if ( uiInactiveMenu.activeSelf )
            {
                uiInactiveMenu.SetActive(false);
            } else
            {
                uiInactiveMenu.SetActive(true);
            }
        } else
        {
            if (uiActiveMenu.activeSelf)
            {
                uiActiveMenu.SetActive(false);
            }
            else
            {
                uiActiveMenu.SetActive(true);
            }
        }
    }

    public void sowSeed()
    {
        uiSeedSelect.SetActive(true);
        uiInactiveMenu.SetActive(false);

        Inventory menuInventory = controller.data.inventory;
        if (menuInventory.GetCount() > 0)
        {
            // hide the "no seeds yet" text
            uiNoSeedText.SetActive(false);

            foreach (Transform child in uiInventoryContent.transform)
            {
                GameObject.Destroy(child.gameObject);
            }
            for (int i = 0; i < menuInventory.GetCount(); i++)
            {
                Seed seedData = controller.data.inventory.GetItem(i);
                Transform prefab = Instantiate(seedButtonPrefab, uiInventoryContent);

                Button prefabButton = prefab.GetComponent<Button>();
                prefabButton.onClick.AddListener(() => selectSeed(seedData));

                Transform itemImage = prefab.transform.GetChild(0).GetChild(0);
                Transform itemText = prefab.transform.GetChild(0).GetChild(1);

                itemText.GetComponent<TMPro.TextMeshProUGUI>().text = seedData.preDescription;
                itemImage.GetComponent<Image>().sprite = Resources.Load<Sprite>(seedData.textureName + "-seed");
            }
        }
    }
    
    public void cancelSeed()
    {
        uiSeedSelect.SetActive(false);
    }

    public void selectSeed(Seed seedData)
    {
        uiSeedSelect.SetActive(false);
        // remove seed from inventory
        controller.data.inventory.RemoveItem(seedData);
        active = true;
        //PlayerPrefs.SetInt("plant" + plantID + "active", 1);
        sprout.sprite = Resources.Load<Sprite>(seedData.textureName + "-baby");
        var tempColor = sprout.color;
        tempColor.a = 1f;
        sprout.color = tempColor;
        activeSeed = seedData;
        stage = 0; // baby
        wiltStage = 0;
        //PlayerPrefs.SetString("plant" + plantID + "seed", seedData.seedType.ToString());
        if ( controller.data.plants[plantID] == null ) // is this statement necessary???
        {
            controller.data.plants[plantID] = this;
        }
    }

    public void tendPlant()
    {
        if (wiltStage >= 1 && wiltStage <= 3)
        {
            if (wiltStage == 2)
            {
                WiltTimer = 60f;
                unWiltTime = 60f;
            } else
            {
                unWiltTime = WiltDuration;
                WiltTimer = WiltDuration;
            }
            wiltStage = 4;
            HeartTimer = 2; //todo: fix according to actual heartTimer from seed
            wiltIcon.alpha = 0;
            tendButton.interactable = false;
            uiActiveMenu.SetActive(false);
            sparkle.SetActive(true);
            StartCoroutine(stopSparkle());
        }
    }

    IEnumerator stopSparkle()
    {
        yield return new WaitForSeconds(0.6f);
        sparkle.SetActive(false);
    }

    private void growToTeen(float countTime = 3f) // pass in the amount of time til it grows
    {
        GrowthTimer += Time.deltaTime;
        if ( GrowthTimer >= countTime )
        {
            count++;
            GrowthTimer = 0f;
            sprout.sprite = Resources.Load<Sprite>(activeSeed.textureName + "-teenager");
            stage = 1;
        }
    }

    private void growToAdult(float countTime = 3f)
    {
        GrowthTimer += Time.deltaTime;
        if (GrowthTimer >= countTime)
        {
            count++;
            sprout.sprite = Resources.Load<Sprite>(activeSeed.textureName + "-adult");
            stage = 2;
        }
    }

    private void wilt()
    {
        WiltTimer += Time.deltaTime;
        if ( WiltTimer >= BeginWiltTime && WiltTimer < FullyWiltTime) // begin to yellow
        {
            sprout.color = Color.Lerp(normalColor, wiltColor, WiltDuration / (FullyWiltTime - WiltTimer));
            wiltIcon.alpha = Mathf.Lerp(tempAlpha, 1, WiltDuration / (FullyWiltTime - WiltTimer));
            WiltDuration += Time.deltaTime;
            if ( wiltStage == 0 )
            {
                HeartTimer = HeartTimer * 2;
            }
            wiltStage = 1;
            harvestButton.interactable = false;
        } else if ( WiltTimer >= FullyWiltTime ) // fully wilted
        {
            // correct for saving
            tempAlpha = 0;
            normalColor = Color.white;
            wiltStage = 2;
            HeartTimer = 0;
        }
    }

    private void unwilt()
    {
        print(WiltTimer);
        if (WiltTimer > 0f)
        {
            sprout.color = Color.Lerp(wiltColor, normalColor, (unWiltTime - WiltTimer) / unWiltTime); // slowly unwilt
            WiltTimer -= Time.deltaTime;
        } else
        {
            harvestButton.interactable = true;
            sprout.color = normalColor;
            WiltTimer = 0f;
            WiltDuration = 0f;
            wiltStage = 0;
            HeartTimer = 2; //todo fix according to actual hearttimer from seed
        }
    }

    IEnumerator wiltIconShake(float randomDuration)
    {
        wiltShaking = false;
        wiggleAnimation.SetTrigger("Wiggle");
        yield return new WaitForSeconds(randomDuration);
        // shake the icon
        wiggleAnimation.SetTrigger("NoWiggle");
        wiltShaking = true;
    }
}
