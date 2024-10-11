using System;

/// <summary>
/// Represents a generic currency type in the game, allowing operations such as checking affordability and applying discounts.
/// The generic type <typeparamref name="T"/> represents the value type of the currency, such as <see cref="int"/>.
/// This class supports immutability, performance optimizations, and compatibility with mathematical operations due to its use of <see cref="struct"/> constraints.
/// </summary>
/// <typeparam name="T">The type of the currency value, constrained to value types (<see cref="struct"/>) that implement <see cref="IComparable{T}"/>.</typeparam>
[Serializable]
public class Currency<T>
    where T : struct, IComparable<T>
{
    public CurrencyTypeSO currencyType;
    public T value;

    public Currency(CurrencyTypeSO currencyType, T value)
    {
        this.currencyType = currencyType;
        this.value = value;
    }

    /// <summary>
    /// Determines whether the player can afford a cost by comparing the current currency value to the provided cost.
    /// </summary>
    /// <param name="cost">The cost to compare against the current currency value.</param>
    /// <returns><c>true</c> if the player has enough currency to cover the cost; otherwise, <c>false</c>.</returns>
    public bool CanAfford(T cost)
    {
        return value.CompareTo(cost) >= 0;
    }

    /// <summary>
    /// Returns a new <see cref="Currency{T}"/> instance with the value discounted by a specified percentage.
    /// </summary>
    /// <param name="discountValue">The discount to apply, represented as a fraction (e.g., 0.2 for 20% off).</param>
    /// <returns>A new <see cref="Currency{T}"/> instance with the discounted value.</returns>
    /// <remarks>
    /// The method dynamically casts <typeparamref name="T"/> to support mathematical operations,
    /// which may incur a small performance overhead. This is not an issue unless the method is called frequently.
    /// </remarks>
    public Currency<T> GetDiscounted(float discountValue)
    {
        dynamic currentValue = value;
        dynamic discountedValue = currentValue * (1 - discountValue);
        return new Currency<T>(currencyType, (T)discountedValue);
    }
}
