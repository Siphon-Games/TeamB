using System;
using UnityEngine;

[Serializable]
public class InventoryItem
{
    [field: SerializeField]
    public int Id { get; private set; }
    public string Type => GetType().Name;

    public InventoryItem(int id)
    {
        Id = id;
    }
}
