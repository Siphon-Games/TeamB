using System;
using UnityEngine;

[Serializable]
public class InventoryItem
{
    public ItemSO item;
    public int Quantity;

    public string Id => item.Id;
    public Sprite Icon => item.Icon;
}
