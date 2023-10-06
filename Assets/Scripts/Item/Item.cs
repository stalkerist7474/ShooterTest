using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using static UnityEditor.Progress;

public class Item : MonoBehaviour
{
    [SerializeField] public int Id;
    [SerializeField] public string Name;
    [SerializeField] public Sprite Icon;
    [SerializeField] public int CountItem;
    [SerializeField] public int MaxInStack;

    public Item(int id, string name, Sprite icon, int countItem, int maxInStack)
    {
        this.Id = id;
        this.Name = name;
        this.Icon = icon;
        this.CountItem = countItem;
        this.MaxInStack = maxInStack;
    }


}
