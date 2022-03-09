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
    [SerializeField] private GameObject inventoryUI;
    [SerializeField] private GameObject uiIntro;

    public void Start()
    {
        controllerObj = GameObject.Find("/Scripts/Controller");
        controller = controllerObj.GetComponent<Controller>();
    }

    public void ToggleInventory()
    {
        Inventory menuInventory = controller.data.inventory;

        if (inventoryUI.activeSelf)
        { // set to inactive and hide
            inventoryUI.SetActive(false);
            foreach (Transform child in inventoryContent.transform)
            {
                GameObject.Destroy(child.gameObject);
            }
        } else
        { // set to active and show
            inventoryUI.SetActive(true);
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
                }
            }
        }
    }

    public void CloseTutorial()
    {
        uiIntro.SetActive(false);
    }
}
