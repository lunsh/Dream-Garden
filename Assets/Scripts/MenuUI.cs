using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class MenuUI : MonoBehaviour
{
    private GameObject controllerObj;
    private Controller controller;
    public Plant PlantPrefab;
    public Transform plantsPanel;

    [SerializeField] private GameObject uiHemisphereIntro;
    [SerializeField] private TMP_Text hemisphereText;
    [SerializeField] private GameObject uiIntro;
    [SerializeField] private GameObject uiSeedStarters;

    [SerializeField] private GameObject noSeedText;
    [SerializeField] private Transform inventoryContent;
    [SerializeField] private Transform itemPrefab;
    [SerializeField] private GameObject inventoryUI;

    /* shop */
    [SerializeField] private GameObject shopUI;
    [SerializeField] private GameObject notEnoughSeed;
    [SerializeField] private GameObject notEnoughPot;
    [SerializeField] private TMP_Text seedPrice;
    [SerializeField] private TMP_Text potPrice;
    [SerializeField] private GameObject seedBuySuccess;
    [SerializeField] private Transform seedPurchasePrefab;
    [SerializeField] private GameObject plantPotConfirm;
    [SerializeField] private GameObject plantPotSuccess;
    [SerializeField] private TMP_Text potConfirmText;

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
            SFXManager.sfxInstance.Audio.PlayOneShot(SFXManager.sfxInstance.Click);
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

                    Transform itemNumContainer = prefab.transform.GetChild(2);
                    if ( seedData.amount > 1 )
                    {
                        itemNumContainer.gameObject.SetActive(true);
                        Transform itemNum = itemNumContainer.transform.GetChild(1);
                        itemNum.GetComponent<TMPro.TextMeshProUGUI>().text = seedData.amount.ToString();
                    }

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
            SFXManager.sfxInstance.Audio.PlayOneShot(SFXManager.sfxInstance.Click);
            shopUI.SetActive(true);
            seedPrice.text = (controller.data.initialSeedCost + (controller.data.seedIncrease * controller.data.numSeedsBought)).ToString();
            potPrice.text = (controller.data.initialPotCost + 8500 * controller.data.numPlantPotsBought).ToString();
        }
    }

    public void CloseShop()
    {
        SFXManager.sfxInstance.Audio.PlayOneShot(SFXManager.sfxInstance.Close);
        shopUI.SetActive(false);
    }

    public void CloseInventory()
    {
        SFXManager.sfxInstance.Audio.PlayOneShot(SFXManager.sfxInstance.Close);
        inventoryUI.SetActive(false);
    }

    public void BuyPot()
    {
        if ( controller.data.hearts >= (controller.data.initialPotCost + 8500 * controller.data.numPlantPotsBought))
        {
            shopUI.SetActive(false);
            plantPotConfirm.SetActive(true);
            potConfirmText.text = (controller.data.initialPotCost + 8500 * controller.data.numPlantPotsBought).ToString();
        }
    }

    public void BuyPotCancel()
    {
        plantPotConfirm.SetActive(false);
    }

    public void BuyPotConfirm()
    {
        plantPotConfirm.SetActive(false);
        plantPotSuccess.SetActive(true);
        controller.data.hearts -= (controller.data.initialPotCost + 8500 * controller.data.numPlantPotsBought);
        controller.data.numPlantPotsBought++;
        controller.data.plantIDs[controller.data.numPlantPotsBought] = 1;
        Plant tempPlant = Instantiate(PlantPrefab, plantsPanel);
        tempPlant.plantID = 0;
        tempPlant.active = false;
        tempPlant.activeSeed = null;
        tempPlant.stage = -1;
        controller.data.plants[controller.data.numPlantPotsBought] = tempPlant;
    }

    public void BuyPotClose()
    {
        plantPotSuccess.SetActive(false);
    }

    public void BuySeed()
    {
        // double check they can afford it
        if (controller.data.hearts >= (controller.data.initialSeedCost + (controller.data.seedIncrease * controller.data.numSeedsBought)))
        {
            shopUI.SetActive(false);
            seedBuySuccess.SetActive(true);

            // pick a random seed
            int randomSeedType = Random.Range(0, 100);
            print(randomSeedType);
            Seed randomSeed;

            if (randomSeedType <= 45 ) // common seed
            {
                int randomSeedNum = Random.Range(0, controller.data.seedsCommon.Count);
                randomSeed = controller.data.seedsCommon[randomSeedNum];
                print(randomSeed.seedType);
            } else if (randomSeedType <= 75 ) // uncommon seed
            {
                int randomSeedNum = Random.Range(0, controller.data.seedsUncommon.Count);
                randomSeed = controller.data.seedsUncommon[randomSeedNum];
                print(randomSeed.seedType);
            } else if (randomSeedType <= 95 ) // rare seed
            {
                int randomSeedNum = Random.Range(0, controller.data.seedsRare.Count);
                randomSeed = controller.data.seedsRare[randomSeedNum];
                print(randomSeed.seedType);
            } else // ultrarare seed
            {
                int randomSeedNum = Random.Range(0, controller.data.seedsUltraRare.Count);
                randomSeed = controller.data.seedsUltraRare[randomSeedNum];
                print(randomSeed.seedType);
            }
            controller.data.inventory.AddItem(randomSeed);
            controller.data.hearts -= (controller.data.initialSeedCost + (controller.data.seedIncrease * controller.data.numSeedsBought));
            controller.data.numSeedsBought++;

            Transform itemImage = seedPurchasePrefab.transform.GetChild(0);
            Transform itemText = seedPurchasePrefab.transform.GetChild(1);

            itemText.GetComponent<TMPro.TextMeshProUGUI>().text = randomSeed.preDescription;
            itemImage.GetComponent<Image>().sprite = Resources.Load<Sprite>(randomSeed.textureName + "-seed");
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


    public void TutorialHemis()
    {
        uiHemisphereIntro.SetActive(true);
    }

    public void SouthHemis()
    {
        controller.data.hemisphere = "south";
        Tutorial();
    }
    public void NorthHemis()
    {
        controller.data.hemisphere = "north";
        Tutorial();
    }

    private void Tutorial()
    {
        controller.SetSeasonWindow();
        uiHemisphereIntro.SetActive(false);
        uiIntro.SetActive(true);
        if ( controller.data.hemisphere == "north" )
        {
            hemisphereText.text = "So you're in the northern hemisphere, great!";
        }
        int seed1Num = UnityEngine.Random.Range(1, controller.data.seedsCommon.Count) - 1;
        int seed2Num = UnityEngine.Random.Range(1, controller.data.seedsCommon.Count) - 1;
        if (seed1Num == seed2Num)
        {
            seed2Num = seed1Num + 1;
            if (seed2Num == controller.data.seedsCommon.Count)
            {
                seed2Num = 0;
            }
        }
        //print(seed1Num);
        //print(seed2Num);
        //data.inventory.AddItem(data.seedsCommon.Where(Seed => Seed.seedType == Seed.SeedType.ViolaFern).SingleOrDefault());
        controller.data.inventory.AddItem(controller.data.seedsCommon[seed1Num]);
        controller.data.inventory.AddItem(controller.data.seedsCommon[seed2Num]);

        int count = 0;
        int seedNum = seed1Num;
        foreach (Transform seedStarterTemplate in uiSeedStarters.transform)
        {
            Transform itemImage = seedStarterTemplate.transform.GetChild(0);
            Transform itemText = seedStarterTemplate.transform.GetChild(1);
            itemText.GetComponent<TMPro.TextMeshProUGUI>().text = controller.data.seedsCommon[seedNum].preDescription;
            itemImage.GetComponent<Image>().sprite = Resources.Load<Sprite>(controller.data.seedsCommon[seedNum].textureName + "-seed");
            count++;
            seedNum = seed2Num;
        }
    }
}
