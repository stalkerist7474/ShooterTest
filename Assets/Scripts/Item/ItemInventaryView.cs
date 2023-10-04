using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using static UnityEditor.Experimental.GraphView.GraphView;
using static UnityEditor.Progress;

public class ItemInventaryView : MonoBehaviour
{
    [SerializeField] public TMP_Text Name;
    [SerializeField] public Image IconView;
    [SerializeField] public TMP_Text CountItemTMP;

    public int Id;
    public int MaxInStack;    
    public int CountItem;

    public bool ShowCount = true;

    public ItemInventary itemInventary; //связь вью и списка с данными

    private void OnEnable()
    {
        
        Inventory.inventory.CountChanged += OnCountChanged;
    }

    private void OnDisable()
    {
        Inventory.inventory.CountChanged -= OnCountChanged;
    }




    private void Start()
    {
        ItemInventaryView inventory = GetComponent<ItemInventaryView>();
        
    }


    public void Render(ItemInventary item)
    {
    
        itemInventary = item;
        Id = item.Id;
        Name.text = item.Name.ToString();
        CountItemTMP.text = item.CountItem.ToString();
        if (item.CountItem == 1)
            ShowCount = false;
        MaxInStack = item.MaxInStack;
        IconView.sprite = item.Icon;

        CheckShowCount();

    }

    private void OnCountChanged(int count) //для скрытия и открытия числа в стаке
    {
        int num = int.Parse(CountItemTMP.text) + count;
        ShowCount = true;

        CountItemTMP.text = num.ToString();

        CheckShowCount();
    }

    private void CheckShowCount()
    {
        if (ShowCount == false)
        {
            CountItemTMP.alpha = 0f;
        }
        if (ShowCount == true)
        {
            CountItemTMP.alpha = 1f;
        }
    }
    public void DeleteItemInventory(ItemInventaryView itemView)
    {
        Inventory.inventory.ItemsInventaryList.Remove(itemView.itemInventary);
        Destroy(itemView.gameObject);
        
    }
}
