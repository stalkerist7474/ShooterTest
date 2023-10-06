using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.Events;


public class Inventory : MonoBehaviour
{
    public static Inventory inventory;
        
    [SerializeField] public static List<ItemInventaryView> PlayerInventoryForCanvas = new List<ItemInventaryView>(); //список вьюшек
    [SerializeField] private ItemInventaryView _inventorySlotTemplate;

    
    [SerializeField] public List<ItemInventary> ItemsInventaryList = new List<ItemInventary>(); //объекты с данными о предметах
    [SerializeField] private GameObject _container;

    [SerializeField] public List<Item> ItemBaseData = new List<Item>();


    public event UnityAction< int> CountChanged; //номер позиции в списке и количество

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
                    countRemainingSpaceInSlot = ItemsInventaryList[i].MaxInStack - ItemsInventaryList[i].CountItem; //сколько места осталось в слоте этого типа предмета

                    if (newItem.CountItem <= countRemainingSpaceInSlot) //если места больше чем количество поднятого предмета и он уже есть в инвентаре
                    {
                    
                        ItemsInventaryList[i].CountItem += newItem.CountItem;
                        CountChanged?.Invoke(newItem.CountItem);
                        //обновить цифры
                        return;
                    }

                    if (ItemsInventaryList[i].CountItem == ItemsInventaryList[i].MaxInStack)   //если существующий слот, полный
                    {
                    
                        ItemsInventaryList.Add(newItem); //добавляем если такого предмета еще нет в инвентаре
                        ItemInventaryView newItemView = Instantiate(_inventorySlotTemplate, _container.transform);
                        newItemView.Render(newItem);
                        PlayerInventoryForCanvas.Add(newItemView);

                        return;
                    }


                    else                                                             //если место есть но не хватает
                    {

                        ItemsInventaryList[i].CountItem += countRemainingSpaceInSlot;
                        CountChanged?.Invoke(countRemainingSpaceInSlot);

                        newItem.CountItem -= countRemainingSpaceInSlot; //уменьшаем количество предметов на величину сколько уже добавили

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
            
                ItemsInventaryList.Add(newItem); //добавляем если такого предмета еще нет в инвентаре
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


