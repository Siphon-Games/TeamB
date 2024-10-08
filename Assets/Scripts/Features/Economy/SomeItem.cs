using System.Collections.Generic;

public class SomeItem<T> : IValue<T>
    where T : struct
{
    private Dictionary<CurrencyType, T> currencyValues;

    public SomeItem()
    {
        currencyValues = new Dictionary<CurrencyType, T>();
    }

    public void SetValue(CurrencyType currencyType, T value)
    {
        currencyValues[currencyType] = value;
    }

    public T GetValue(CurrencyType currencyType)
    {
        if (currencyValues.TryGetValue(currencyType, out T value))
        {
            return value;
        }

        return default;
    }
}
