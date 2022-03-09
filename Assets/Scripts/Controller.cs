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

    public Transform plantsPanel;

    private void Start()
    {
        data = new Data();
        // Save data handling
        PlayerPrefs.DeleteAll();
        if (! PlayerPrefs.HasKey("saveData")) // no save data, run tutorial
        {
            Tutorial();
            PlayerPrefs.SetInt("saveData", 1);
        } else
        {
            // inventory save data: todo: add other seed types (rare, uncommon, etc)
            string tempInventoryIDs = PlayerPrefs.GetString("inventoryIDs", "noIDs");
            if (tempInventoryIDs != "noIDs")
            {
                int[] tempInventory = Array.ConvertAll(tempInventoryIDs.Split(','), int.Parse);
                for ( int i = 0; i < tempInventory.Length; i++ )
                {
                    data.inventory.AddItem(data.seedsCommon[tempInventory[i]]);
                }
            }
        }
        data.hearts = PlayerPrefs.GetInt("totalhearts", 0);
        string tempIDs = PlayerPrefs.GetString("plantIDs", "noIDs");
        if ( tempIDs == "noIDs" )
        {
            // create a new plant
            data.plantIDs[0] = 0;
            PlayerPrefs.SetString("plantIDs", string.Join(",", data.plantIDs));
        }
        else
        {
            data.plantIDs = Array.ConvertAll(tempIDs.Split(','), int.Parse);
        }

        for ( int i = 0; i < data.plantIDs.Length; i++ )
        {
            if ( data.plantIDs[i] != -1 ) { 
                Plant tempPlant = Instantiate(PlantPrefab, plantsPanel);
                tempPlant.plantID = i;
                data.plants.Add(tempPlant);
            }
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
        PlayerPrefs.SetInt("totalhearts", data.hearts);

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

        PlayerPrefs.SetString("inventoryIDs", seed1Num + "," + seed2Num);
        PlayerPrefs.SetString("inventoryCounts", "1, 1");
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
}
