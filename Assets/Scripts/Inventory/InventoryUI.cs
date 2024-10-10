using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class InventoryUI
    : Inventory,
        IPointerClickHandler,
        IPointerMoveHandler,
        IBeginDragHandler,
        IEndDragHandler,
        IDragHandler
{
    [SerializeField]
    InventorySlotPrefab _slotPrefab;

    [SerializeField]
    RectTransform _parentRectTransform;

    InventorySlot selectedSlot;
    InventorySlot originSlot;

    Image draggingItem;

    [SerializeField]
    List<InventoryItem> items;

    [SerializeField]
    Button SaveButton,
        LoadButton;

    bool isDragging;

    enum ItemMoveType
    {
        DragAndDrop,
        ClickHold,
    }

    [SerializeField]
    List<ItemMoveType> itemMoveTypes;

    public override void InitializeInventory()
    {
        LoadButton.onClick.AddListener(LoadInventory);
        SaveButton.onClick.AddListener(Save);

        for (int i = 0; i < InventorySize; i++)
        {
            var spawnedSlot = Instantiate(_slotPrefab);
            spawnedSlot.GetComponent<RectTransform>().SetParent(_parentRectTransform, false);
            Slots.Add(spawnedSlot);
        }

        List<(InventoryItem item, int quantity)> itemsWithQuantities = new();

        items.ForEach(item =>
        {
            itemsWithQuantities.Add(new(item, Random.Range(1, 5)));
        });

        itemsWithQuantities.Add(new(new HelmetItem(8, 11, 255), Random.Range(1, 5)));

        AddItems(itemsWithQuantities);
    }

    public void LoadInventory()
    {
        var savedInventory = Load();
        Slots.ForEach(slot => slot.RemoveItem());

        savedInventory.ForEach(savedSlot =>
        {
            Slots[savedSlot.Index].AddItem(savedSlot.Item, savedSlot.Quantity);
        });
    }

    public void OnPointerMove(PointerEventData eventData)
    {
        if (draggingItem != null && !isDragging)
        {
            var screenPoint = Input.mousePosition;
            screenPoint.z = 100;
            draggingItem.transform.position = Camera.main.ScreenToWorldPoint(screenPoint);
        }
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (!itemMoveTypes.Contains(ItemMoveType.ClickHold))
        {
            return;
        }

        bool isRightClick = eventData.button == PointerEventData.InputButton.Right;

        if (!isDragging)
        {
            if (selectedSlot == null)
            {
                PickUpItem(eventData);
            }
            else
            {
                TryMoveItem(eventData, isRightClick);
            }
        }
    }

    public void PickUpItem(PointerEventData eventData)
    {
        var targetSlot =
            eventData.pointerCurrentRaycast.gameObject.GetComponentInParent<InventorySlot>();
        if (targetSlot != null && targetSlot.Item != null && targetSlot.Item.Id > 0)
        {
            if (draggingItem != null)
            {
                Destroy(draggingItem.gameObject);
            }

            selectedSlot = Instantiate(_slotPrefab);
            selectedSlot.AddItem(targetSlot.Item, targetSlot.Quantity);
            draggingItem = selectedSlot.GetComponent<Image>();
            draggingItem.transform.SetParent(
                _parentRectTransform.GetComponentInParent<Canvas>().transform,
                false
            );
            draggingItem.gameObject.AddComponent<CanvasGroup>().blocksRaycasts = false;
            draggingItem.enabled = false;
            var screenPoint = Input.mousePosition;
            screenPoint.z = 100;
            draggingItem.transform.position = Camera.main.ScreenToWorldPoint(screenPoint);

            targetSlot.RemoveItem();
            originSlot = targetSlot;
            return;
        }
    }

    public void MoveDraggedItem(PointerEventData eventData)
    {
        var pointerObject = eventData.pointerCurrentRaycast.gameObject;

        if (pointerObject == null)
        {
            if (originSlot != null)
            {
                ReturnToOrigin();
            }
            return;
        }

        var targetSlot = pointerObject.GetComponentInParent<InventorySlot>();
        if (targetSlot != null)
        {
            if (selectedSlot != null)
            {
                var targetItem = targetSlot.Item;
                var originItem = selectedSlot.Item;

                // if target has no item we dump
                if (targetItem == null)
                {
                    targetSlot.AddItem(originItem, selectedSlot.Quantity);
                    Destroy(selectedSlot.gameObject);
                    selectedSlot = null;
                    return;
                }

                // if both are the same we update amount
                if (targetItem == originItem)
                {
                    targetSlot.UpdateQuantity(selectedSlot.Quantity);
                    Destroy(selectedSlot.gameObject);
                    selectedSlot = null;
                    return;
                }

                // else we switch
                targetSlot.AddItem(originItem, selectedSlot.Quantity);
                originSlot.AddItem(targetItem, targetSlot.Quantity);
            }
        }
    }

    void ReturnToOrigin()
    {
        originSlot.AddItem(selectedSlot.Item, selectedSlot.Quantity);
        Destroy(selectedSlot.gameObject);
        selectedSlot = null;
    }

    public void TryMoveItem(PointerEventData eventData, bool isRightClick = false)
    {
        var pointerObject = eventData.pointerCurrentRaycast.gameObject;
        if (pointerObject == null)
        {
            if (originSlot != null)
            {
                ReturnToOrigin();
            }
            return;
        }

        var targetSlot = pointerObject.GetComponentInParent<InventorySlot>();
        if (targetSlot != null)
        {
            if (selectedSlot != null)
            {
                if (IsAllowedToMove(selectedSlot, targetSlot))
                {
                    int moveAmount = isRightClick ? 1 : selectedSlot.Quantity;

                    MoveItem(selectedSlot, targetSlot, moveAmount);
                    if (!isRightClick || selectedSlot.Quantity == 0)
                    {
                        Destroy(selectedSlot.gameObject);
                        selectedSlot = null;
                    }
                    return;
                }
                if (!isRightClick && originSlot != null)
                {
                    originSlot.AddItem(selectedSlot.Item, selectedSlot.Quantity);
                    Destroy(selectedSlot.gameObject);
                    selectedSlot = null;
                }
            }
        }
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        if (!itemMoveTypes.Contains(ItemMoveType.DragAndDrop))
        {
            return;
        }

        if (selectedSlot == null)
        {
            PickUpItem(eventData);
            isDragging = true;
        }
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        bool isRightClick = eventData.button == PointerEventData.InputButton.Right;
        TryMoveItem(eventData, isRightClick);
        isDragging = false;
    }

    public void OnDrag(PointerEventData eventData)
    {
        if (draggingItem != null)
        {
            var screenPoint = Input.mousePosition;
            screenPoint.z = 100;
            draggingItem.transform.position = Camera.main.ScreenToWorldPoint(screenPoint);
        }
    }

    public override void MoveItem(
        InventorySlot originSlot,
        InventorySlot targetSlot,
        int moveAmount
    )
    {
        bool isTargetItemSameAsOrigin =
            targetSlot.Item != null && targetSlot.Item.Id == originSlot.Item.Id;

        if (isTargetItemSameAsOrigin)
        {
            targetSlot.UpdateQuantity(moveAmount);
        }
        else
        {
            targetSlot.AddItem(originSlot.Item, moveAmount);
        }

        originSlot.UpdateQuantity(-moveAmount);
    }
}
