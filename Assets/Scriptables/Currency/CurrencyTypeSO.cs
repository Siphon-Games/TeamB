using UnityEngine;

[CreateAssetMenu(fileName = "Currency", menuName = "CurrencyType")]
public class CurrencyTypeSO : ScriptableObject
{
    public string currency;
    public string description; // for additional metadata

    // Can also place a sprite icon here too!
}
