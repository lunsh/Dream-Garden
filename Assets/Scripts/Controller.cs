using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Controller : MonoBehaviour
{
    public Data data;
    public Plant PlantPrefab;
    [SerializeField] private TMP_Text heartsText;
    [SerializeField] private UIInventory uiInventory;
    public Inventory inventory;

    public Transform plantsPanel;

    private void Start()
    {
        data = new Data();
        inventory = new Inventory();
        uiInventory.SetInventory(inventory);
        // Save data handling
        //PlayerPrefs.DeleteAll();
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
                if (data.plants[i].Timer >= data.plants[i].DelayAmount)
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
}
