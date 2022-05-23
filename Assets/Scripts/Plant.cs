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

    [SerializeField] private GameObject uiInactiveMenu;
    [SerializeField] private GameObject uiActiveMenu;
    [SerializeField] private GameObject uiSeedSelect;
    [SerializeField] private GameObject uiNoSeedText;
    [SerializeField] private Transform uiInventoryContent;
    [SerializeField] private Transform seedButtonPrefab;
    [SerializeField] public Transform animationHolder;
    [SerializeField] public Transform heartAnimation;

    public void Start()
    {
        controllerObj = GameObject.Find("/Scripts/Controller");
        controller = controllerObj.GetComponent<Controller>();
        HeartTimer = 15;
    }

    public void Update()
    {
        if ( stage == 0 )
        {
            growToTeen(activeSeed.timeToGrow1);
        } else if ( stage == 1 )
        {
            growToAdult(activeSeed.timeToGrow2);
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
        //PlayerPrefs.SetString("plant" + plantID + "seed", seedData.seedType.ToString());
        if ( controller.data.plants[plantID] == null ) // is this statement necessary???
        {
            controller.data.plants[plantID] = this;
        }
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
}
