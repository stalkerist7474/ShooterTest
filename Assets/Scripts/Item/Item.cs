using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Item : MonoBehaviour
{
    [SerializeField] public int Id;
    [SerializeField] public string Name;
    [SerializeField] public Sprite Icon;
    [SerializeField] public int CountItem;
    [SerializeField] public int MaxInStack;

    private void Start()
    {
        
    }
}
