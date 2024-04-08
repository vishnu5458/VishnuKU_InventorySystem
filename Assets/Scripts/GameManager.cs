using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance = null;

    [SerializeField] GameObject[] numberedPrefabs;
    [SerializeField] GameObject[] uniquePrefabs;
    [SerializeField] Inventory[] inventories;
    [SerializeField] UniqueInventory uniqueInventory;
    [SerializeField] GameObject enemyPrefab;
    public GameObject firstPersonButton;
    public GameObject thirdPersonButton;
    public GameObject fireButton;
    [SerializeField] GameObject gameOverPanel;

    public int bulletCount;
    public int weaponType;
    int collectableCount;

    private void Awake()
    {
        Instance = this;
    }
    // Start is called before the first frame update
    void Start()
    {

        for (int i = 0; i<20; i++)
        {
            float rndx = Random.Range(-40, 40);
            float rndy = Random.Range(-40, 40);
            int rnd = Random.Range(0, 4);
            Instantiate(numberedPrefabs[rnd], new Vector3(rndx, 0, rndy), Quaternion.identity);
        }
        for (int i = 0; i < uniquePrefabs.Length; i++)
        {
            float rndx = Random.Range(-40, 40);
            float rndy = Random.Range(-40, 40);
            Instantiate(uniquePrefabs[i], new Vector3(rndx, 0, rndy), Quaternion.identity);
        }
    }

    public void AddToInventory(Item _item)
    {
        if (_item.IsUnique)
        {
            uniqueInventory.AddInventory(_item.itemID);
            weaponType = _item.itemID;

            float rndx = Random.Range(-40, 40);
            float rndy = Random.Range(-40, 40);
            Instantiate(enemyPrefab, new Vector3(rndx, 0, rndy), Quaternion.identity);
        }
        else
        {
            foreach (Inventory inventory in inventories)
            {
                if (inventory.inventoryID == _item.itemID)
                {
                    if (_item.IsConsumable)
                        bulletCount++;
                    inventory.AddInventory();
                    return;
                }
            }
        }
        collectableCount++;
        if (collectableCount >= 10)
        {
            OnGameOver();
        }
    }

    public void ReplaceItem(int _id)
    {
        float rndx = Random.Range(-40, 40);
        float rndy = Random.Range(-40, 40);
        Instantiate(numberedPrefabs[_id], new Vector3(rndx, 0, rndy), Quaternion.identity);
        InputController.onDrop?.Invoke();
    }

    public void RemoveWeapon(int _id)
    {
        float rndx = Random.Range(-40, 40);
        float rndy = Random.Range(-40, 40);
        Instantiate(uniquePrefabs[_id], new Vector3(rndx, 0, rndy), Quaternion.identity);
        InputController.onDrop?.Invoke();
    }

    public void OnGameOver()
    {
        gameOverPanel.SetActive(true);
    }

}
