/// <summary>
/// Defines a method to retrieve the value of an item or entity in terms of a specific currency type.
/// This interface allows any class that implements it to return a currency value based on the provided <see cref="CurrencyTypeSO"/>.
/// </summary>
public interface IValue
{
    /// <summary>
    /// Retrieves the value of the item in a specific currency.
    /// </summary>
    /// <param name="currencyType">The type of currency (e.g., gold, steel).</param>
    /// <returns>
    /// The item's value in the specified currency. If a discount is applied, returns the discounted value.
    /// </returns>
    int GetValue(CurrencyTypeSO currencyType);
}
