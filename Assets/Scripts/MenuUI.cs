using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class MenuUI : MonoBehaviour
{
    private GameObject controllerObj;
    private Controller controller;

    [SerializeField] private GameObject noSeedText;
    [SerializeField] private Transform inventoryContent;
    [SerializeField] private Transform itemPrefab;
    [SerializeField] private GameObject inventoryUI;
    [SerializeField] private GameObject uiIntro;

    /* shop */
    [SerializeField] private GameObject shopUI;
    [SerializeField] private GameObject notEnoughSeed;
    [SerializeField] private GameObject notEnoughPot;
    [SerializeField] private TMP_Text seedPrice;
    [SerializeField] private TMP_Text potPrice;
    [SerializeField] private GameObject seedBuySuccess;

    public void Start()
    {
        controllerObj = GameObject.Find("/Scripts/Controller");
        controller = controllerObj.GetComponent<Controller>();
    }

    public void Update()
    {
        if (controller.data.hearts >= controller.data.initialSeedCost)
        {
            notEnoughSeed.SetActive(false);
        }
        else
        {
            notEnoughSeed.SetActive(true);
        }
        if (controller.data.hearts >= controller.data.initialPotCost)
        {
            notEnoughPot.SetActive(false);
        }
        else
        {
            notEnoughPot.SetActive(true);
        }
    }

    public void ToggleInventory()
    {
        Inventory menuInventory = controller.data.inventory;

        if (inventoryUI.activeSelf)
        { // set to inactive and hide
            inventoryUI.SetActive(false);
        } else
        { // set to active and show
            inventoryUI.SetActive(true);
            foreach (Transform child in inventoryContent.transform)
            {
                GameObject.Destroy(child.gameObject);
            }
            if (menuInventory.GetCount() > 0)
            {
                // hide the "no seeds yet" text
                noSeedText.SetActive(false);
                for (int i = 0; i < menuInventory.GetCount(); i++)
                {
                    Seed seedData = controller.data.inventory.GetItem(i);
                    Transform prefab = Instantiate(itemPrefab, inventoryContent);

                    Transform itemImage = prefab.transform.GetChild(0);
                    Transform itemText = prefab.transform.GetChild(1);

                    itemText.GetComponent<TMPro.TextMeshProUGUI>().text = seedData.preDescription;
                    itemImage.GetComponent<Image>().sprite = Resources.Load<Sprite>(seedData.textureName + "-seed");
                }
            }
        }
    }

    public void ToggleShop()
    {
        if ( shopUI.activeSelf )
        {
            shopUI.SetActive(false);
        } else
        {
            shopUI.SetActive(true);
            seedPrice.text = (controller.data.initialSeedCost + (controller.data.seedIncrease * controller.data.numSeedsBought)).ToString();
            potPrice.text = (controller.data.initialPotCost + controller.data.initialPotCost * controller.data.numPlantPotsBought).ToString();
        }
    }

    public void CloseShop()
    {
        shopUI.SetActive(false);
    }

    public void BuySeed()
    {
        // double check they can afford it
        if (controller.data.hearts >= controller.data.initialSeedCost)
        {
            print("buying seeds");
            shopUI.SetActive(false);
            seedBuySuccess.SetActive(true);

            // pick a random seed

        }
    }

    public void DoneSeed()
    {
        seedBuySuccess.SetActive(false);
    }

    public void CloseTutorial()
    {
        uiIntro.SetActive(false);
    }
}
