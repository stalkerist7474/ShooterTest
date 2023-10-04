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
        
    [SerializeField] private List<ItemInventaryView> _playerInventoryForCanvas = new List<ItemInventaryView>(); //������ ������
    [SerializeField] private ItemInventaryView _inventorySlotTemplate;

    
    [SerializeField] public List<ItemInventary> ItemsInventaryList = new List<ItemInventary>(); //������� � ������� � ���������
    [SerializeField] private GameObject _container;


    public event UnityAction< int> CountChanged; //����� ������� � ������ � ����������

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
                        _playerInventoryForCanvas.Add(newItemView);

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
            
                ItemsInventaryList.Add(newItem); //��������� ���� ������ �������� ��� ��� � ���������
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
    //            countRemainingSpaceInSlot = _playerInventory[i].MaxInStack - _playerInventory[i].CountItem; //������� ����� �������� � ����� ����� ���� ��������
                
    //            if (item.CountItem <= countRemainingSpaceInSlot) //���� ����� ������ ��� ���������� ��������� �������� � �� ��� ���� � ���������
    //            {
    //                Debug.Log("AddItem3");
    //                //int num = int.Parse(_playerInventory[i].CountItemTMP.text);
    //                _playerInventory[i].CountItem += item.CountItem;
    //                //_playerInventory[i].CountItemTMP.text = num.ToString();
    //                //_playerInventory[i].CountItem += item.CountItem;
    //            }

    //            if(_playerInventory[i].CountItem == _playerInventory[i].MaxInStack)   //���� ������������ ���� ������
    //            {
    //                ItemInventory newItem = Instantiate(_inventorySlotTemplate, _container.transform);
    //                newItem.Render(item);
    //                _playerInventory.Add(newItem); //��������� ���� ������ �������� ��� ��� � ���������
    //                Debug.Log("AddItem5");
    //            }


    //            else                                                             //���� ����� ���� �� �� �������
    //            {
    //                //_playerInventory[i].CountItem += countRemainingSpaceInSlot; 
    //                //int num = int.Parse(_playerInventory[i].CountItemTMP.text);
    //                _playerInventory[i].CountItem += countRemainingSpaceInSlot;
    //                //_playerInventory[i].CountItemTMP.text = num.ToString(); //��������� ������������ ����


    //                item.CountItem -= countRemainingSpaceInSlot; //��������� ���������� ��������� �� �������� ������� ��� ��������
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
    //            _playerInventory.Add(newItem); //��������� ���� ������ �������� ��� ��� � ���������
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


