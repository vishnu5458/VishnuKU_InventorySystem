using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    public int inventoryID;
    public int inventoryCount;
    public TextMeshProUGUI itemCountText;

    public void AddInventory()
    {
        inventoryCount++;
        itemCountText.text = inventoryCount.ToString();
    }

    public void RemoveInventory()
    {
        if (inventoryCount > 0)
        {
            inventoryCount--;
            itemCountText.text = inventoryCount.ToString();
            GameManager.Instance.ReplaceItem(inventoryID);
        }
    }
}
