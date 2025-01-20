using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "item",menuName = "Data/Item")]

public class Item : ScriptableObject
{
    [SerializeField]private string itemName;
    public string ItemName { get { return itemName; } }
    
    [SerializeField]private string desc;
    public string Desc { get { return desc; } } 

    [SerializeField]private Sprite icon;
    public Sprite Icon { get { return icon; } }

}
