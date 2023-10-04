using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditorInternal.Profiling.Memory.Experimental;
using UnityEngine;
using UnityEngine.Events;
using static UnityEditor.Progress;

public class Inventory : MonoBehaviour
{
    public static Inventory inventory;
        
    [SerializeField] private List<ItemInventaryView> _playerInventoryForCanvas = new List<ItemInventaryView>(); //список вьюшек
    [SerializeField] private ItemInventaryView _inventorySlotTemplate;

    
    [SerializeField] public List<ItemInventary> ItemsInventaryList = new List<ItemInventary>(); //объекты с данными о предметах
    [SerializeField] private GameObject _container;


    public event UnityAction< int> CountChanged; //номер позиции в списке и количество

    private void Awake()
    {
        inventory = GetComponent<Inventory>();
    }


    public void AddItemToInventory(Item item)
    {

        ItemInventary newItem = new ItemInventary(item.Id, item.Name, item.Icon, item.CountItem, item.MaxInStack);

       

        
        Debug.Log($"ItemsCount{ItemsInventaryList.Count}");
        




        if (ItemsInventaryList.Count == 0)
        {
            ItemsInventaryList.Add(newItem);

            ItemInventaryView newItemView = Instantiate(_inventorySlotTemplate, _container.transform);
            newItemView.Render(newItem);
            _playerInventoryForCanvas.Add(newItemView);

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
                        _playerInventoryForCanvas.Add(newItemView);

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
                        _playerInventoryForCanvas.Add(newItemView);
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
                _playerInventoryForCanvas.Add(newItemView);
                Debug.Log("AddItem5");
            }





        }




    }

    public void DeleteItemInventory(GameObject item)
    {
        Destroy(item);
    }






































    //public void AddItemToInventory(Item item)
    //{
        
    //    if (_playerInventory.Count == 0)
    //    {

    //        ItemInventory newItem = Instantiate(_inventorySlotTemplate, _container.transform);
    //        newItem.Render(item);
    //        _playerInventory.Add(newItem);

    //        return;
    //    }

    //    Debug.Log("---------------------2");

    //    int countRemainingSpaceInSlot;
    //    for (int i = 0; i < _playerInventory.Count; i++)
    //    {
    //        Debug.Log("AddItem2");
    //        if (_playerInventory[i].Id == item.Id)
    //        {
    //            countRemainingSpaceInSlot = _playerInventory[i].MaxInStack - _playerInventory[i].CountItem; //сколько места осталось в слоте этого типа предмета
                
    //            if (item.CountItem <= countRemainingSpaceInSlot) //если места больше чем количество поднятого предмета и он уже есть в инвентаре
    //            {
    //                Debug.Log("AddItem3");
    //                //int num = int.Parse(_playerInventory[i].CountItemTMP.text);
    //                _playerInventory[i].CountItem += item.CountItem;
    //                //_playerInventory[i].CountItemTMP.text = num.ToString();
    //                //_playerInventory[i].CountItem += item.CountItem;
    //            }

    //            if(_playerInventory[i].CountItem == _playerInventory[i].MaxInStack)   //если существующий слот полный
    //            {
    //                ItemInventory newItem = Instantiate(_inventorySlotTemplate, _container.transform);
    //                newItem.Render(item);
    //                _playerInventory.Add(newItem); //добавляем если такого предмета еще нет в инвентаре
    //                Debug.Log("AddItem5");
    //            }


    //            else                                                             //если место есть но не хватает
    //            {
    //                //_playerInventory[i].CountItem += countRemainingSpaceInSlot; 
    //                //int num = int.Parse(_playerInventory[i].CountItemTMP.text);
    //                _playerInventory[i].CountItem += countRemainingSpaceInSlot;
    //                //_playerInventory[i].CountItemTMP.text = num.ToString(); //заполняем существующий слот


    //                item.CountItem -= countRemainingSpaceInSlot; //уменьшаем количество предметов на величину сколько уже добавили
    //                ItemInventory newItem = Instantiate(_inventorySlotTemplate, _container.transform);
    //                newItem.Render(item);
                    
    //                _playerInventory.Add(newItem);
    //                Debug.Log("AddItem4");
    //            }
    //        }
    //        else
    //        {
    //            ItemInventory newItem = Instantiate(_inventorySlotTemplate, _container.transform);
    //            newItem.Render(item);
    //            _playerInventory.Add(newItem); //добавляем если такого предмета еще нет в инвентаре
    //            Debug.Log("AddItem5");
    //        }
    //    }

    //    //Debug.Log($"_playerInventory-{_playerInventory} _playerInventory1-{_playerInventory[0]} _playerInventory1-{_playerInventory[1]}");

    //}
    
    public void RenderItemInventory(GameObject item)
    {
    }
    public void DeleteItemInventory()
    {

    }
}


