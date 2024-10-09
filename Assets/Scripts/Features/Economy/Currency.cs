using System;

/// <summary>
/// Generic class for currency.
/// Struct helps with immutability, performance, and compatibility with math operations.
/// </summary>
/// <typeparam name="T"></typeparam>
[Serializable]
public class Currency<T>
    where T : struct, IComparable<T>
{
    public CurrencyTypeSO currencyType;
    public T value;

    public Currency(CurrencyTypeSO currencyType, T value)
    {
        // Fetch the currencyType from CurrencyManager dynamically
        this.currencyType = currencyType;
        this.value = value;
    }

    public bool CanAfford(T cost)
    {
        return value.CompareTo(cost) >= 0;
    }

    public Currency<T> GetDiscounted(float discountValue)
    {
        // Dyanmic can incur a small performance overhead, but assuming
        // this is not a frequent operation, it "should" be fine
        dynamic currentValue = value;
        dynamic discountedValue = currentValue * (1 - discountValue);
        return new Currency<T>(currencyType, (T)discountedValue);
    }
}
