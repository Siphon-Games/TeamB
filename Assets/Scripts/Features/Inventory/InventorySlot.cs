using System;
using UnityEngine;

[Serializable]
public class InventorySlot : MonoBehaviour
{
    public int Quantity { get; private set; }
    public InventoryItem Item { get; private set; } = null;

    public InventorySlot(InventorySlot slot)
    {
        Quantity = slot.Quantity;
        Item = slot.Item;
    }

    public virtual void UpdateQuantity(int newAmount)
    {
        Quantity += newAmount;

        if (Quantity == 0)
        {
            RemoveItem();
        }
    }

    public virtual void RemoveItem()
    {
        Quantity = 0;
        Item = null;
    }

    public virtual void AddItem(InventoryItem item, int quantity)
    {
        Item = item;
        Quantity = quantity;
    }
}
