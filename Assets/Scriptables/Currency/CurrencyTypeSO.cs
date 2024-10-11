using UnityEngine;

/// <summary>
/// ScriptableObject representing a currency type in the game.
/// This class defines the core attributes for a currency type, such as its name, description, and additional metadata.
/// It can be used to create reusable currency type assets that can be referenced throughout the game.
/// </summary>
/// <remarks>
/// This class uses Unity's <see cref="CreateAssetMenuAttribute"/> to allow easy creation of new currency types from the Unity Editor.
/// These currency types can be used across various systems, such as player inventory, shops, and transactions.
/// </remarks>
[CreateAssetMenu(fileName = "Currency", menuName = "CurrencyType")]
public class CurrencyTypeSO : ScriptableObject
{
    public string currency;
    public string description; // for additional metadata

    // Can also place a sprite icon here too!
}
