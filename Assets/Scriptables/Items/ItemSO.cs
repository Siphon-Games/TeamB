using UnityEngine;

/// <summary>
/// Base class for all items in the game.
/// </summary>
[CreateAssetMenu(fileName = "ItemSO", menuName = "Item")]
public class ItemSO : ScriptableObject
{
    public int Id;
    public string Name;
    public string Description;
    public Sprite Icon;
}
