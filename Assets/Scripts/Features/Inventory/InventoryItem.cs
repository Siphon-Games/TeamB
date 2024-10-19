using System;
using UnityEngine;

[Serializable]
public class InventoryItem
{
    [SerializeField]
    public ItemSO item;

    public string Id => item.Id;
    public Sprite Icon => item.Icon;

    // [field: SerializeField]
    // public int Id { get; private set; }
    // public string Type => GetType().Name;

    // public InventoryItem(int id)
    // {
    //     Id = id;
    // }
}
