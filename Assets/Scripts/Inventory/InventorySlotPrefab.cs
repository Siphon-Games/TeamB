using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class InventorySlotPrefab : InventorySlot
{
    [SerializeField]
    Image ItemImage;

    [SerializeField]
    TextMeshProUGUI ItemQuantityField;

    [SerializeField]
    Image ItemQuantityBackground;

    public override void AddItem(InventoryItem item, int quantity)
    {
        base.AddItem(item, quantity);
        if (quantity > 1)
        {
            ItemQuantityBackground.gameObject.SetActive(true);
            ItemQuantityField.text = Quantity.ToString();
        }
        ItemImage.gameObject.SetActive(true);
        ItemImage.color = GetColorById(item.Id);
    }

    Color GetColorById(int id)
    {
        return id switch
        {
            1 => Color.blue,
            2 => Color.red,
            3 => Color.yellow,
            4 => Color.gray,
            5 => Color.green,
            6 => Color.magenta,
            7 => Color.cyan,
            _ => Color.black,
        };
    }

    public override void RemoveItem()
    {
        base.RemoveItem();
        ItemImage.gameObject.SetActive(false);
        ItemQuantityBackground.gameObject.SetActive(false);
    }

    public override void UpdateQuantity(int newAmount)
    {
        base.UpdateQuantity(newAmount);
        ItemQuantityBackground.gameObject.SetActive(Quantity > 1);
        ItemQuantityField.text = Quantity.ToString();
    }
}
