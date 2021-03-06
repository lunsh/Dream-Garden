using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Linq;

public class Controller : MonoBehaviour
{
    public Data data;
    public Plant PlantPrefab;

    [SerializeField] private TMP_Text heartsText;
    [SerializeField] private GameObject menus;
    [SerializeField] private GameObject springWindow;
    [SerializeField] private GameObject summerWindow;
    [SerializeField] private GameObject fallWindow;
    [SerializeField] private GameObject winterWindow;
    public Inventory inventory;

    bool isPaused = false;

    public Transform plantsPanel;
    [SerializeField] public GameObject uiSeedSelect;
    [SerializeField] public GameObject uiNoSeedText;
    [SerializeField] public Transform uiInventoryContent;
    [SerializeField] public Transform seedButtonPrefab;
    [SerializeField] public GameObject harvestConfirm;
    [SerializeField] public TMP_Text harvestAmountText;

    [SerializeField] public GameObject notificationPane;
    [SerializeField] public Transform notificationPrefab;
    public Plant CurrentPlant;

    [SerializeField] public GameObject customizeConfirmPane;
    [SerializeField] public Button customizeConfirmButton;
    [SerializeField] public GameObject customizePane;
    [SerializeField] public Transform colorsTransform;

    [SerializeField] public GameObject discoveredAllSeeds;

    private void Start()
    {
        data = new Data();
        // Save data handling
        //PlayerPrefs.DeleteAll();
        if (! PlayerPrefs.HasKey("saveData")) // no save data, run tutorial
        {
            MenuUI menuScripts = menus.GetComponent<MenuUI>();
            menuScripts.TutorialHemis();
            data.plantIDs[0] = 0; 
            Plant tempPlant = Instantiate(PlantPrefab, plantsPanel);
            tempPlant.plantID = 0;
            tempPlant.active = false;
            tempPlant.activeSeed = null;
            tempPlant.stage = -1;
            data.plants.Add(tempPlant);
        } else
        {
            loadSave();
            SetSeasonWindow();
        }
    }

    public void SetSeasonWindow()
    {
        // add handling for spring/summer/fall/winter, with handling for hemispheres
        string utcMonth = System.DateTime.UtcNow.ToString("MMMM");
        int utcDay = int.Parse(System.DateTime.UtcNow.ToString("dd"));
        string currSeason = "spring";
        if (utcMonth == "January" || utcMonth == "February" || (utcMonth == "December" && utcDay >= 21) || (utcMonth == "March" && utcDay < 20))
        {
            currSeason = "winter";
            if (data.hemisphere == "south")
            {
                currSeason = "summer";
            }
        }
        else if (utcMonth == "April" || utcMonth == "May" || (utcMonth == "March" && utcDay >= 20) || (utcMonth == "June" && utcDay < 20))
        {
            currSeason = "spring";
            if (data.hemisphere == "south")
            {
                currSeason = "fall";
            }
        }
        else if (utcMonth == "July" || utcMonth == "August" || (utcMonth == "June" && utcDay >= 20) || (utcMonth == "September" && utcDay < 21))
        {
            currSeason = "summer";
            if (data.hemisphere == "south")
            {
                currSeason = "winter";
            }
        }
        else
        {
            currSeason = "fall";
            if (data.hemisphere == "south")
            {
                currSeason = "spring";
            }
        }
        if (currSeason == "summer")
        {
            springWindow.SetActive(false);
            summerWindow.SetActive(true);
        }
        else if (currSeason == "fall")
        {
            springWindow.SetActive(false);
            fallWindow.SetActive(true);
        }
        else if (currSeason == "winter")
        {
            springWindow.SetActive(false);
            winterWindow.SetActive(true);
        }
    }

    private void Update()
    {
        for (int i = 0; i < data.plants.Count; i++)
        {
            if (data.plants[i] != null && data.plants[i].active == true && data.plants[i].HeartTimer > 0)
            {
                data.plants[i].Timer += Time.deltaTime;
                if (data.plants[i].Timer >= data.plants[i].HeartTimer)
                {
                    data.plants[i].Timer = 0f;
                    data.hearts = Mathf.Min(data.hearts + 1, 9999);

                    Transform prefab = Instantiate(data.plants[i].heartAnimation, data.plants[i].animationHolder);
                    StartCoroutine(destroyHeart(prefab));
                }
            }
        }
        heartsText.text = data.hearts.ToString();

        if ( isPaused )
        {
            //need to save hearts every time
            PlayerPrefs.SetInt("totalhearts", data.hearts);
        }

        if ( Input.GetKeyDown(KeyCode.R))
        {
            PlayerPrefs.DeleteAll();
        }
    }

    public void harvestPlant(Plant harvestPlant, int harvestAmount )
    {
        AddHearts(harvestAmount);
        harvestPlant.plantID = 0;
        harvestPlant.active = false;
        harvestPlant.activeSeed = null;
        harvestPlant.stage = -1;
        var tempColor = harvestPlant.sprout.color;
        tempColor.a = 0f;
        harvestPlant.sprout.color = tempColor;
        harvestPlant.sprout.sprite = null;
        harvestPlant.wiltStage = -1;
    }

    public void AddHearts(int hearts)
    {
        data.hearts = Mathf.Min(data.hearts + hearts, 9999);
        heartsText.text = data.hearts.ToString();
    }

    IEnumerator destroyHeart(Transform heartObj)
    {
        yield return new WaitForSeconds(1.0f);
        // load scene
        GameObject.Destroy(heartObj.gameObject);
    }

    void OnApplicationFocus(bool hasFocus)
    {
        isPaused = !hasFocus;
        if ( isPaused == true )
        {
            saveData();
        }
    }

    void OnApplicationPause(bool pauseStatus)
    {
        isPaused = pauseStatus;
    }

    public void createNotification(string seedName)
    {
        int numNotifications = notificationPane.transform.childCount;
        if ( numNotifications >= 5 )
        {
            Destroy(notificationPane.transform.GetChild(0).gameObject); // destroy the oldest notification
        }
        Transform prefab = Instantiate(notificationPrefab, notificationPane.transform);

        Transform itemText = prefab.transform.GetChild(1);
        itemText.GetComponent<TMPro.TextMeshProUGUI>().text = "You have successfully grown a " + seedName + " and it has been added to your collection!";
    }

    public void showCustomizeUnlockPane(int plantID)
    {
        customizeConfirmPane.SetActive(true);
        if ( data.hearts >= 1000 )
        {
            customizeConfirmButton.interactable = true;
            customizeConfirmButton.onClick.RemoveAllListeners();
            customizeConfirmButton.onClick.AddListener(() => unlockPotCustomization(plantID));
        } else
        {
            customizeConfirmButton.interactable = false;
        }
    }

    public void customizeUnlockCancel()
    {
        customizeConfirmPane.SetActive(false);
    }

    public void unlockPotCustomization(int plantID)
    {
        data.customizeUnlock[plantID] = 1;
        data.hearts -= 1000;
        customizeConfirmPane.SetActive(false);
        showCustomizePane(plantID);
    }

    public void showCustomizePane(int plantID)
    {
        customizePane.SetActive(true);
        string[] colors = new string[13] { "FFC5C5", "FF696C", "FFCD65", "9EF17F", "AAFFDA", "7DFFF7", "7FC0FF", "B897FF", "D76DB6", "D48F5C", "BBD7E3", "758891", "FFFFFF" };
        int i = 0;
        foreach(Transform childColor in colorsTransform)
        {
            if (data.customizeColors[plantID] == colors[i])
            {
                childColor.GetComponent<Button>().Select();
            }
            string tempString = colors[i];
            childColor.GetComponent<Button>().onClick.RemoveAllListeners();
            childColor.GetComponent<Button>().onClick.AddListener(() => setColor(plantID, tempString));
            i++;
        }
    }

    public void setColor(int plantID, string color)
    {
        data.customizeColors[plantID] = color;
    }

    public void closeCustomizePane()
    {
        customizePane.SetActive(false);
    }

    public void showDiscoveredAll()
    {
        discoveredAllSeeds.SetActive(true);
    }

    public void hideDiscoveredAll()
    {
        discoveredAllSeeds.SetActive(false);
    }

    private void saveData()
    {
        BGManager _bgmanager = GameObject.FindWithTag("BGManager").GetComponent<BGManager>();
        PlayerPrefs.SetInt("saveData", 1);
        PlayerPrefs.SetString("hemisphere", data.hemisphere);
        PlayerPrefs.SetInt("playlisttrack", _bgmanager.songPlaying);
        if (data.plantIDs != null)
        {
            PlayerPrefs.SetString("plantIDs", string.Join(",", data.plantIDs));
        }
        // save plants

        for (int i = 0; i < data.plantIDs.Length; i++)
        {
            if (data.plantIDs[i] != -1 && data.plants[i] != null && data.plants[i].activeSeed != null)
            {
                string plantSeedName = data.plants[i].activeSeed.seedType.ToString();
                PlayerPrefs.SetString("plant" + i + "seed", plantSeedName);
                string plantSeedRarity = data.plants[i].activeSeed.rarity;
                PlayerPrefs.SetString("plant" + i + "rarity", plantSeedRarity);
                PlayerPrefs.SetInt("plant" + i + "stage", data.plants[i].stage);
                PlayerPrefs.SetString("plant" + i + "growth", data.plants[i].GrowthTimer.ToString());

                // wilting
                PlayerPrefs.SetString("plant" + i + "currcolor", ColorUtility.ToHtmlStringRGB(data.plants[i].sprout.color));
                PlayerPrefs.SetInt("plant" + i + "wiltStage", data.plants[i].wiltStage);
                PlayerPrefs.SetString("plant" + i + "wiltTimer", data.plants[i].WiltTimer.ToString());
                PlayerPrefs.SetString("Plant" + i + "wiltDuration", data.plants[i].WiltDuration.ToString());
            } else
            {
                PlayerPrefs.DeleteKey("plant" + i + "seed");
                PlayerPrefs.DeleteKey("plant" + i + "rarity");
                PlayerPrefs.DeleteKey("plant" + i + "stage");
                PlayerPrefs.DeleteKey("plant" + i + "growth");
                PlayerPrefs.DeleteKey("plant" + i + "currcolor");
                PlayerPrefs.DeleteKey("plant" + i + "wiltStage");
                PlayerPrefs.DeleteKey("plant" + i + "wiltTimer");
                PlayerPrefs.DeleteKey("plant" + i + "wiltDuration");
            }
        }
        // save inventory
        PlayerPrefs.SetString("inventoryIDs", data.inventory.ToString());

        int[] inventoryCounts = new int[data.inventory.GetCount()];
        for (int i = 0; i < data.inventory.GetCount(); i++ )
        {
            inventoryCounts[i] = data.inventory.GetItem(i).amount;
        }
        PlayerPrefs.SetString("inventoryCounts", string.Join(",", inventoryCounts));
        PlayerPrefs.SetInt("numSeedsBought", data.numSeedsBought);
        PlayerPrefs.SetInt("numPlantPotsBought", data.numPlantPotsBought);

        // save collection
        if (data.seedsDiscovered != null)
        {
            PlayerPrefs.SetString("seedsDiscovered", string.Join(",", data.seedsDiscovered));
        }
        // save pot colors
        PlayerPrefs.SetString("customizeUnlock", string.Join(",", data.customizeUnlock));
        PlayerPrefs.SetString("customizeColors", string.Join(",", data.customizeColors));
        PlayerPrefs.Save();
    }

    private void loadSave()
    {
        data.hemisphere = PlayerPrefs.GetString("hemisphere", "north");
        BGManager _bgmanager = GameObject.FindWithTag("BGManager").GetComponent<BGManager>();
        _bgmanager.songPlaying = PlayerPrefs.GetInt("playlisttrack", 0);
        string tempInventoryIDs = PlayerPrefs.GetString("inventoryIDs", "noIDs");
        if (tempInventoryIDs != "noIDs" && tempInventoryIDs != "")
        {
            string[] tempInventory = tempInventoryIDs.Split(',');
            for (int i = 0; i < tempInventory.Length; i++)
            {
                data.inventory.AddItem(data.findSeed(tempInventory[i]));
            }
        }
        // colors
        string tempCustomizeUnlock = PlayerPrefs.GetString("customizeUnlock", "-1,-1,-1");
        data.customizeUnlock = Array.ConvertAll(tempCustomizeUnlock.Split(','), int.Parse);
        string tempCustomizeColors = PlayerPrefs.GetString("customizeColors", "FFFFFF,FFFFFF,FFFFFF");
        data.customizeColors = tempCustomizeColors.Split(',');
        // load collection

        string collectionIDs = PlayerPrefs.GetString("seedsDiscovered", "noCollection");
        if (collectionIDs != "noCollection")
        {
            string[] tempCollection = collectionIDs.Split(',');
            for ( int i = 0; i < tempCollection.Length; i++ )
            {
                if (tempCollection[i] == "")
                {
                    data.seedsDiscovered[i] = null;
                } else
                {
                    data.seedsDiscovered[i] = tempCollection[i];
                }
            }
        }
        // inventory amounts
        string inventoryValString = PlayerPrefs.GetString("inventoryCounts", "noAmounts");
        if (inventoryValString != "noAmounts" && inventoryValString != "")
        {
            int[] inventoryVals = Array.ConvertAll(inventoryValString.Split(','), int.Parse);
            for ( int i = 0; i < data.inventory.GetCount(); i++ )
            {
                data.inventory.SetAmount(i, inventoryVals[i]);
            }
        }
        data.numSeedsBought = PlayerPrefs.GetInt("numSeedsBought", 0);
        data.numPlantPotsBought = PlayerPrefs.GetInt("numPlantPotsBought", 0);
        // hearts
        data.hearts = PlayerPrefs.GetInt("totalhearts", 0);
        // plants
        string tempIDs = PlayerPrefs.GetString("plantIDs", "noIDs");
        if (tempIDs != "noIDs") { 
            data.plantIDs = Array.ConvertAll(tempIDs.Split(','), int.Parse);
            /**/
            // attempt to get seed data
            for (int i = 0; i < data.plantIDs.Length; i++)
            {
                if (data.plantIDs[i] != -1)
                {
                    Plant tempPlant = Instantiate(PlantPrefab, plantsPanel);
                    tempPlant.plantID = i;

                    string tempSeedData = PlayerPrefs.GetString("plant" + i + "seed", "noSeed");
                    int tempSeedStage = PlayerPrefs.GetInt("plant" + i + "stage", -1);
                    tempPlant.stage = tempSeedStage;
                    tempPlant.active = false;
                    if (tempSeedData != "noSeed")
                    { // the plant pot has a plant in it
                        Seed foundSeed = data.findSeed(tempSeedData);
                        tempPlant.active = true;
                        if ( tempSeedStage == -1 || tempSeedStage == 0 )
                        {
                            tempPlant.sprout.sprite = Resources.Load<Sprite>(foundSeed.textureName + "-baby");
                        } else if ( tempSeedStage == 1 )
                        {
                            tempPlant.sprout.sprite = Resources.Load<Sprite>(foundSeed.textureName + "-teenager");
                        } else
                        {
                            tempPlant.sprout.sprite = Resources.Load<Sprite>(foundSeed.textureName + "-adult");
                        }
                        string tempColorString = "#" + PlayerPrefs.GetString("plant" + i + "currcolor", "nocolor");
                        var tempColor = tempPlant.sprout.color;
                        ColorUtility.TryParseHtmlString(tempColorString, out tempColor);
                        tempColor.a = 1f;
                        tempPlant.sprout.color = tempColor;
                        tempPlant.activeSeed = data.findSeed(tempSeedData);
                        tempPlant.GrowthTimer = float.Parse(PlayerPrefs.GetString("plant" + i + "growth", foundSeed.timeToGrow1.ToString()));
                        tempPlant.WiltDuration = float.Parse(PlayerPrefs.GetString("plant" + i + "wiltDuration", 0f.ToString()));
                        tempPlant.WiltTimer = float.Parse(PlayerPrefs.GetString("plant" + i + "wiltTimer", 0f.ToString()));
                    }
                    data.plants[i] = tempPlant;
                    data.plants[i].stage = tempSeedStage;
                    data.plants[i].wiltStage = PlayerPrefs.GetInt("plant" + i + "wiltStage", -1);
                    data.plants[i].setupPlantFromSave();
                }
            }
        }
    }
}
