using System;
using UnityEngine;

/// <summary>
/// Base class for all items in the game.
/// </summary>
[CreateAssetMenu(fileName = "ItemSO", menuName = "Item")]
public class ItemSO : ScriptableObject
{
    [field: SerializeField, ReadOnly]
    public string Id { get; private set; }
    public string Name;
    public string Description;
    public Sprite Icon;

    private void OnValidate()
    {
        // Assign random Id to item
        if (string.IsNullOrEmpty(Id))
        {
            Id = IDGenerator.GenerateRandomID();
        }
    }
}
