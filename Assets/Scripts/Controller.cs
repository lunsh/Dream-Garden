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
    [SerializeField] private GameObject uiInventory;
    [SerializeField] private GameObject uiIntro;
    [SerializeField] private GameObject uiSeedStarters;
    public Inventory inventory;

    bool isPaused = false;

    public Transform plantsPanel;

    private void Start()
    {
        data = new Data();
        // Save data handling
        //PlayerPrefs.DeleteAll();
        if (! PlayerPrefs.HasKey("saveData")) // no save data, run tutorial
        {
            Tutorial();
            data.plantIDs[0] = 0; 
            Plant tempPlant = Instantiate(PlantPrefab, plantsPanel);
            tempPlant.plantID = 0;
            tempPlant.active = false;
            tempPlant.activeSeed = null;
            data.plants.Add(tempPlant);
        } else
        {
            loadSave();
        }
    }

    private void Update()
    {
        for (int i = 0; i < data.plants.Count; i++)
        {
            if (data.plants[i] != null && data.plants[i].active == true)
            {
                data.plants[i].Timer += Time.deltaTime;
                if (data.plants[i].Timer >= data.plants[i].HeartTimer)
                {
                    data.plants[i].Timer = 0f;
                    data.hearts += 1;
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

    private void Tutorial()
    {
        uiIntro.SetActive(true);
        int seed1Num = UnityEngine.Random.Range(1, data.seedsCommon.Count) - 1;
        int seed2Num = UnityEngine.Random.Range(1, data.seedsCommon.Count) - 1;
        if (seed1Num == seed2Num)
        {
            seed2Num = seed1Num + 1;
            if (seed2Num == data.seedsCommon.Count)
            {
                seed2Num = 0;
            }
        }
        //print(seed1Num);
        //print(seed2Num);
        //data.inventory.AddItem(data.seedsCommon.Where(Seed => Seed.seedType == Seed.SeedType.ViolaFern).SingleOrDefault());
        data.inventory.AddItem(data.seedsCommon[seed1Num]);
        data.inventory.AddItem(data.seedsCommon[seed2Num]);

        int count = 0;
        int seedNum = seed1Num;
        foreach (Transform seedStarterTemplate in uiSeedStarters.transform)
        {
            Transform itemImage = seedStarterTemplate.transform.GetChild(0);
            Transform itemText = seedStarterTemplate.transform.GetChild(1);
            itemText.GetComponent<TMPro.TextMeshProUGUI>().text = data.seedsCommon[seedNum].preDescription;
            itemImage.GetComponent<Image>().sprite = Resources.Load<Sprite>(data.seedsCommon[seedNum].textureName + "-seed");
            count++;
            seedNum = seed2Num;
        }
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

    private void saveData()
    {
        print("saving");
        PlayerPrefs.SetInt("saveData", 1);
        if (data.plantIDs != null)
        {
            PlayerPrefs.SetString("plantIDs", string.Join(",", data.plantIDs));
        }
        // save plants

        print(data.plants[0]);
        for (int i = 0; i < data.plantIDs.Length; i++)
        {
            if (data.plantIDs[i] != -1 && data.plants[i] != null)
            {
                print(data.plants[i].activeSeed);
                string plantSeedName = data.plants[i].activeSeed.seedType.ToString();
                PlayerPrefs.SetString("plant" + i + "seed", plantSeedName);
                string plantSeedRarity = data.plants[i].activeSeed.rarity;
                PlayerPrefs.SetString("plant" + i + "rarity", plantSeedRarity);
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
    }

    private void loadSave()
    {
        string tempInventoryIDs = PlayerPrefs.GetString("inventoryIDs", "noIDs");
        if (tempInventoryIDs != "noIDs")
        {
            string[] tempInventory = tempInventoryIDs.Split(',');
            for (int i = 0; i < tempInventory.Length; i++)
            {
                data.inventory.AddItem(data.findSeed(tempInventory[i]));
            }
        }
        // inventory amounts
        string inventoryValString = PlayerPrefs.GetString("inventoryAmounts", "noAmounts");
        if (inventoryValString != "noAmounts")
        {
            int[] inventoryVals = Array.ConvertAll(inventoryValString.Split(','), int.Parse);
            for ( int i = 0; i < data.inventory.GetCount(); i++ )
            {
                data.inventory.SetAmount(i, inventoryVals[i]);
            }
        }
        data.hearts = PlayerPrefs.GetInt("totalhearts", 0);
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
                    if (tempSeedData != "noSeed")
                    { // the plant pot has a plant in it
                        Seed foundSeed = data.findSeed(tempSeedData);
                        tempPlant.active = true;
                        tempPlant.sprout.sprite = Resources.Load<Sprite>(foundSeed.textureName + "-baby");
                        var tempColor = tempPlant.sprout.color;
                        tempColor.a = 1f;
                        tempPlant.sprout.color = tempColor;
                        tempPlant.activeSeed = data.findSeed(tempSeedData);
                        tempPlant.stage = 0; // baby
                    }
                    data.plants[i] = tempPlant;
                }
            }
        }
    }
}
