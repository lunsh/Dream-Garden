using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuUI : MonoBehaviour
{
    private GameObject controllerObj;
    private Controller controller;

    [SerializeField] private GameObject noSeedText;
    [SerializeField] private Transform inventoryContent;
    [SerializeField] private Transform itemPrefab;

    public void Start()
    {
        controllerObj = GameObject.Find("/Scripts/Controller");
        controller = controllerObj.GetComponent<Controller>();
    }

    public void ToggleInventory()
    {
        Inventory menuInventory = controller.data.inventory;

        if (menuInventory.GetCount() > 0)
        {
            // hide the "no seeds yet" text
            noSeedText.SetActive(false);
            for ( int i = 0; i < menuInventory.GetCount(); i++ )
            {
                Instantiate(itemPrefab, inventoryContent);
            }
        }
    }
}
