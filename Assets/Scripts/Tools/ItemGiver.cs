using System.Collections.Generic;
using UnityEngine;

class ItemGiver<T>
{
    public List<T> ItemList { get; private set; } = new();

    public ItemGiver(List<T> list)
    {
        ItemList = list;
    }
    public T GetRandomItem() {
        
        int selectedIndex = Random.Range(0, ItemList.Count - 1);
        Debug.Log($"totalIndex: {ItemList.Count - 1} selectedIndex: {selectedIndex} chosenItem: {ItemList[selectedIndex]}");
        return ItemList[selectedIndex];
    }
}
