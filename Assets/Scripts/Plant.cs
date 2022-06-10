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

    private int harvestAmount;

    [SerializeField] private GameObject uiInactiveMenu;
    [SerializeField] private GameObject uiActiveMenu;
    [SerializeField] private Transform seedButtonPrefab;
    [SerializeField] public Transform animationHolder;
    [SerializeField] public Transform heartAnimation;
    [SerializeField] private CanvasGroup wiltIcon;
    [SerializeField] private Button tendButton;
    [SerializeField] private Button potButton;
    [SerializeField] private Button harvestButton;
    [SerializeField] private GameObject sparkle;
    [SerializeField] private GameObject harvestAnimation;
    [SerializeField] private GameObject customizeLockedButton;
    [SerializeField] private GameObject customizeUnlockedButton;

    public Animator wiggleAnimation;

    public void Awake()
    {
        HeartTimer = 0;
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

        if (wiltStage != -1)
        {
            BeginWiltTime = activeSeed.timeToWilt * 0.75f;
            FullyWiltTime = activeSeed.timeToWilt;
        }

        setHeartTimer();
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

        if (controller.data.customizeUnlock[plantID] == 1 )
        {
            Image potImage = potButton.gameObject.GetComponent<Image>();
            Color tempColor = potImage.color;
            ColorUtility.TryParseHtmlString("#" + controller.data.customizeColors[plantID], out tempColor);
            tempColor.a = 1f;
            potImage.color = tempColor;
            var colors = potButton.colors;
            colors.normalColor = tempColor;
            potButton.colors = colors;
        }
    }

    public void setHeartTimer()
    {
        if ( stage == 0 )
        {
            HeartTimer = activeSeed.heartsGenerate1;
        } else if ( stage == 1 )
        {
            HeartTimer = activeSeed.heartsGenerate2;
        } else if ( stage == 2 )
        {
            HeartTimer = activeSeed.heartsGenerate3;
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

            BeginWiltTime = activeSeed.timeToWilt * 0.75f;
            FullyWiltTime = activeSeed.timeToWilt;
        } else if ( wiltStage == 2 )
        {
            wiltIcon.alpha = 1;
            HeartTimer = 0;
            harvestButton.interactable = false;

            BeginWiltTime = activeSeed.timeToWilt * 0.75f;
            FullyWiltTime = activeSeed.timeToWilt;
        }
    }

    public void plantButton()
    {
        if ( ! active )
        {
            if ( uiInactiveMenu.activeSelf )
            {
                uiInactiveMenu.SetActive(false);
                SFXManager.sfxInstance.Audio.PlayOneShot(SFXManager.sfxInstance.Close);
            } else
            {
                uiInactiveMenu.SetActive(true);
                SFXManager.sfxInstance.Audio.PlayOneShot(SFXManager.sfxInstance.Click);
            }
        } else
        {
            if (uiActiveMenu.activeSelf)
            {
                uiActiveMenu.SetActive(false);
                SFXManager.sfxInstance.Audio.PlayOneShot(SFXManager.sfxInstance.Close);
            }
            else
            {
                if (controller.data.customizeUnlock[plantID] == 1)
                {
                    customizeLockedButton.SetActive(false);
                    customizeUnlockedButton.SetActive(true);
                }
                uiActiveMenu.SetActive(true);
                SFXManager.sfxInstance.Audio.PlayOneShot(SFXManager.sfxInstance.Click);
            }
        }
    }

    public void sowSeed()
    {
        controller.uiSeedSelect.SetActive(true);
        uiInactiveMenu.SetActive(false);
        SFXManager.sfxInstance.Audio.PlayOneShot(SFXManager.sfxInstance.Click);

        controller.CurrentPlant = this;

        Inventory menuInventory = controller.data.inventory;
        foreach (Transform child in controller.uiInventoryContent.transform)
        {
            GameObject.Destroy(child.gameObject);
        }
        if (menuInventory.GetCount() > 0)
        {
            // hide the "no seeds yet" text
            controller.uiNoSeedText.SetActive(false);
            for (int i = 0; i < menuInventory.GetCount(); i++)
            {
                Seed seedData = controller.data.inventory.GetItem(i);
                Transform prefab = Instantiate(seedButtonPrefab, controller.uiInventoryContent);

                Button prefabButton = prefab.GetComponent<Button>();
                prefabButton.onClick.AddListener(() => selectSeed(seedData));

                Transform itemImage = prefab.transform.GetChild(0).GetChild(0);
                Transform itemText = prefab.transform.GetChild(0).GetChild(1);

                Transform itemNumContainer = prefab.transform.GetChild(0).GetChild(2);
                if (seedData.amount > 1)
                {
                    itemNumContainer.gameObject.SetActive(true);
                    Transform itemNum = itemNumContainer.transform.GetChild(1);
                    itemNum.GetComponent<TMPro.TextMeshProUGUI>().text = seedData.amount.ToString();
                } else
                {
                    itemNumContainer.gameObject.SetActive(false);
                }

                if (controller.data.seedsDiscovered[seedData.id] != null)
                {
                    itemText.GetComponent<TMPro.TextMeshProUGUI>().text = seedData.name;
                }
                else
                {
                    itemText.GetComponent<TMPro.TextMeshProUGUI>().text = seedData.preDescription;
                }

                itemImage.GetComponent<Image>().sprite = Resources.Load<Sprite>(seedData.textureName + "-seed");
            }
        } else
        {
            controller.uiNoSeedText.SetActive(true);
        }
    }

    public void selectSeed(Seed seedData)
    {
        controller.uiSeedSelect.SetActive(false);
        SFXManager.sfxInstance.Audio.PlayOneShot(SFXManager.sfxInstance.Click);
        // remove seed from inventory
        controller.data.inventory.RemoveItem(seedData);
        active = true;
        sprout.sprite = Resources.Load<Sprite>(seedData.textureName + "-baby");
        var tempColor = sprout.color;
        tempColor.a = 1f;
        sprout.color = tempColor;
        activeSeed = seedData;
        stage = 0; // baby
        wiltStage = 0;
        GrowthTimer = 0f;
        BeginWiltTime = activeSeed.timeToWilt * 0.75f;
        FullyWiltTime = activeSeed.timeToWilt;
        setHeartTimer();
        if ( controller.data.plants[plantID] == null ) // is this statement necessary???
        {
            controller.data.plants[plantID] = this;
        }
    }

    public void harvestPlantButton()
    {
        uiActiveMenu.SetActive(false);
        controller.harvestConfirm.SetActive(true);
        SFXManager.sfxInstance.Audio.PlayOneShot(SFXManager.sfxInstance.Click);
        if (activeSeed.rarity == "Common")
        {
            if ( stage == 0 ) { harvestAmount = 100; }
            if ( stage == 1 ) { harvestAmount = 200; }
            if ( stage == 2 ) { harvestAmount = 400; }
        } else if ( activeSeed.rarity == "Uncommon" )
        {
            if (stage == 0) { harvestAmount = 200; }
            if (stage == 1) { harvestAmount = 400; }
            if (stage == 2) { harvestAmount = 800; }
        } else if (activeSeed.rarity == "Rare")
        {
            if (stage == 0) { harvestAmount = 300; }
            if (stage == 1) { harvestAmount = 600; }
            if (stage == 2) { harvestAmount = 1000; }
        } else if (activeSeed.rarity == "Ultrarare")
        {
            if (stage == 0) { harvestAmount = 500; }
            if (stage == 1) { harvestAmount = 1000; }
            if (stage == 2) { harvestAmount = 2000; }
        }
        controller.harvestAmountText.text = harvestAmount.ToString();
        controller.CurrentPlant = this;
    }

    public void harvestPlant()
    {
        harvestAnimation.SetActive(true);
        SFXManager.sfxInstance.Audio.PlayOneShot(SFXManager.sfxInstance.Click);
        controller.harvestPlant(this, harvestAmount);
        StartCoroutine(stopHarvestAnimation());
    }

    IEnumerator stopHarvestAnimation()
    {
        yield return new WaitForSeconds(0.48f);
        WiltTimer = 0f;
        WiltDuration = 0f;
        harvestAnimation.SetActive(false);
    }

    public void tendPlant()
    {
        if (wiltStage >= 1 && wiltStage <= 3)
        {
            if (wiltStage == 2)
            {
                WiltTimer = activeSeed.timeToWilt;
                unWiltTime = activeSeed.timeToWilt;
            } else
            {
                unWiltTime = Mathf.Min(WiltDuration, activeSeed.timeToWilt);
                WiltTimer = Mathf.Min(WiltDuration, activeSeed.timeToWilt);
                wiltColor = sprout.color;
            }
            wiltStage = 4;
            setHeartTimer(); //todo: fix according to actual heartTimer from seed
            wiltIcon.alpha = 0;
            SFXManager.sfxInstance.Audio.PlayOneShot(SFXManager.sfxInstance.Click);
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
            setHeartTimer();
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
            setHeartTimer();
            bool prevDiscovered = true;
            if (controller.data.seedsDiscovered[activeSeed.id] == null )
            {
                prevDiscovered = false;
            }
            controller.data.seedsDiscovered[activeSeed.id] = activeSeed.seedType.ToString();
            if ( ! prevDiscovered )
            {
                controller.createNotification(activeSeed.name);
                int numDiscovered = 0;
                for (int i = 0; i < controller.data.seedsDiscovered.Length; i++)
                {
                    if (controller.data.seedsDiscovered[i] != null)
                    {
                        numDiscovered++;
                    }
                }
                if (numDiscovered == controller.data.seedsDiscovered.Length )
                {
                    controller.showDiscoveredAll();
                }
            }
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
            tempAlpha = 0;
            normalColor = Color.white;
            wiltStage = 2;
            HeartTimer = 0;
        }
    }

    private void unwilt()
    {
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
            setHeartTimer();
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

    public void toggleUnlockCustomize()
    {
        uiActiveMenu.SetActive(false);
        controller.showCustomizeUnlockPane(plantID);
    }

    public void toggleCustomize()
    {
        uiActiveMenu.SetActive(false);
        controller.showCustomizePane(plantID);
    }
}
