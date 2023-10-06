using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.Events;


public class Inventory : MonoBehaviour
{
    public static Inventory inventory;
        
    [SerializeField] public static List<ItemInventaryView> PlayerInventoryForCanvas = new List<ItemInventaryView>(); //������ ������
    [SerializeField] private ItemInventaryView _inventorySlotTemplate;

    
    [SerializeField] public List<ItemInventary> ItemsInventaryList = new List<ItemInventary>(); //������� � ������� � ���������
    [SerializeField] private GameObject _container;

    [SerializeField] public List<Item> ItemBaseData = new List<Item>();


    public event UnityAction< int> CountChanged; //����� ������� � ������ � ����������

    private void Awake()
    {
        inventory = GetComponent<Inventory>();
    }


    public void AddItemToInventory(Item item)
    {

        ItemInventary newItem = new ItemInventary(item.Id, item.Name, item.Icon, item.CountItem, item.MaxInStack);

        if (ItemsInventaryList.Count == 0)
        {
            ItemsInventaryList.Add(newItem);

            ItemInventaryView newItemView = Instantiate(_inventorySlotTemplate, _container.transform);
            newItemView.Render(newItem);
            PlayerInventoryForCanvas.Add(newItemView);

            return;
        }

        if (ItemsInventaryList != null)
        {
            bool seached= false;
            int countRemainingSpaceInSlot = 0;

            for (int i = 0; i < ItemsInventaryList.Count; i++)
            {
            
                if (ItemsInventaryList[i].Id == newItem.Id)
                {
                    seached = true;
                    countRemainingSpaceInSlot = ItemsInventaryList[i].MaxInStack - ItemsInventaryList[i].CountItem; //������� ����� �������� � ����� ����� ���� ��������

                    if (newItem.CountItem <= countRemainingSpaceInSlot) //���� ����� ������ ��� ���������� ��������� �������� � �� ��� ���� � ���������
                    {
                    
                        ItemsInventaryList[i].CountItem += newItem.CountItem;
                        CountChanged?.Invoke(newItem.CountItem);
                        //�������� �����
                        return;
                    }

                    if (ItemsInventaryList[i].CountItem == ItemsInventaryList[i].MaxInStack)   //���� ������������ ����, ������
                    {
                    
                        ItemsInventaryList.Add(newItem); //��������� ���� ������ �������� ��� ��� � ���������
                        ItemInventaryView newItemView = Instantiate(_inventorySlotTemplate, _container.transform);
                        newItemView.Render(newItem);
                        PlayerInventoryForCanvas.Add(newItemView);

                        return;
                    }


                    else                                                             //���� ����� ���� �� �� �������
                    {

                        ItemsInventaryList[i].CountItem += countRemainingSpaceInSlot;
                        CountChanged?.Invoke(countRemainingSpaceInSlot);

                        newItem.CountItem -= countRemainingSpaceInSlot; //��������� ���������� ��������� �� �������� ������� ��� ��������

                        ItemsInventaryList.Add(newItem);
                        ItemInventaryView newItemView = Instantiate(_inventorySlotTemplate, _container.transform);
                        newItemView.Render(newItem);
                        PlayerInventoryForCanvas.Add(newItemView);
                        return;

                    }
                }
                else
                {

                }
            }

            if(seached == false) 
            { 
            
                ItemsInventaryList.Add(newItem); //��������� ���� ������ �������� ��� ��� � ���������
                ItemInventaryView newItemView = Instantiate(_inventorySlotTemplate, _container.transform);
                    
                newItemView.Render(newItem);
                PlayerInventoryForCanvas.Add(newItemView);
                
            }





        }




    }



    public void LoadData(Save.InventorySaveData data)
    {


        List<Item> items = FindAnyObjectByType<Inventory>().ItemBaseData;
        Inventory inventory = FindAnyObjectByType<Inventory>();


        for (int i = 0; i < items.Count; i++)
        {
            if (items[i].Id == data.Id)
            {

                Item item = new Item(items[i].Id, items[i].Name, items[i].Icon, items[i].CountItem, items[i].MaxInStack);

                inventory.AddItemToInventory(item);
            }
        }



    }
}


