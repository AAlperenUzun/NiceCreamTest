using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;

public class ItemsData : MonoBehaviour
{
    private List<ItemsData.ItemData> _items;
    private Dictionary<string, ItemsData.ItemData> _itemsDict;
    
    public IReadOnlyDictionary<string, ItemsData.ItemData> Items
    {
         get => (IReadOnlyDictionary<string, ItemsData.ItemData>) null;
    }
    
    [Serializable]
    public class ItemData
    {
        public string Id;
        public ItemData()
        {
        }
    }
}
