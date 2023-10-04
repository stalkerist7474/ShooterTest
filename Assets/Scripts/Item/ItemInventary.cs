using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemInventary
{
    public int Id;
    public string Name;
    public Sprite Icon;
    public int CountItem;
    public int MaxInStack;

    public ItemInventary(int id, string name, Sprite icon, int countItem, int maxInStack) 
    { 
        this.Id = id;
        this.Name = name;
        this.Icon = icon;
        this.CountItem = countItem;
        this.MaxInStack = maxInStack;
    }
}
