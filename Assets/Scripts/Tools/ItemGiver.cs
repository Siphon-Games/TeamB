using System.Collections.Generic;
using UnityEngine;

class ItemGiver<T>
{
    readonly private List<T> tList = new();

    public List<T> ItemList => tList;

    public ItemGiver(List<T> list)
    {
        tList = list;
    }
    public T GetRandomItem() {
        
        int selectedIndex = Random.Range(0, tList.Count - 1);
        Debug.Log($"totalIndex: {tList.Count - 1} selectedIndex: {selectedIndex} chosenItem: {tList[selectedIndex]}");
        return tList[selectedIndex];
    }
}
