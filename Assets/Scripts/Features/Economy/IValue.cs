/// <summary>
/// Defines a method to retrieve the value of an item or entity in terms of a specific currency type.
/// This interface allows any class that implements it to return a currency value based on the provided <see cref="CurrencyTypeSO"/>.
/// </summary>
/// <typeparam name="T">The type of the currency value, typically a numeric type like <see cref="int"/> or <see cref="float"/>.</typeparam>
public interface IValue<T>
{
    T GetValue(CurrencyTypeSO currencyType);
}
