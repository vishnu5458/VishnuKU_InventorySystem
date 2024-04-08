using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UniqueInventory : MonoBehaviour
{
    public int inventoryID;
    public Image image;
    public Sprite[] gunStrites;


    public void AddInventory(int index)
    {
        inventoryID = index;
        image.sprite = gunStrites[index];
        GameManager.Instance.firstPersonButton.SetActive(true);
    }

    public void RemoveInventory()
    {
        image.sprite = null;
        GameManager.Instance.RemoveWeapon(inventoryID);
    }
}
