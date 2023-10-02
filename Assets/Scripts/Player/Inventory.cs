using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    public static Inventory inventory;
    [SerializeField] public Dictionary<Item, int> PlayerInventory = new Dictionary<Item, int>();
    [SerializeField] private List<GameObject> _slots = new List<GameObject>();



    private void Awake()
    {
        inventory = GetComponent<Inventory>();
    }


    public void AddItemToInventory(Item item)
    {
        
        if (PlayerInventory.ContainsKey(item))
        {
            if (PlayerInventory[item] < item._maxInStack) 
            {

                PlayerInventory[item]++;
            
            }
        }
        else
        {
            PlayerInventory.Add(item, 1);
        }
    }

   

}
