using UnityEngine;

public class HelmetItem : InventoryItem
{
    [field: SerializeField]
    public int Resistence { get; private set; }

    [field: SerializeField]
    public int Durability { get; private set; }

    public HelmetItem(int id, int resistence, int durability)
        : base(id)
    {
        Resistence = resistence;
        Durability = durability;
    }
}
