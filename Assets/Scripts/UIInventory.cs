using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIInventory : MonoBehaviour
{
    private Inventory inventory;
    private Transform itemSlotContainer;

    private void Awake()
    {
        itemSlotContainer = transform.Find("itemSlotContainer");
    }

    public void SetInventory(Inventory inventory)
    {
        this.inventory = inventory;
    }
}
