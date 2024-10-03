using System.Collections.Generic;
using System.Linq;
using UnityEngine;

class ChanceItem : IChanceScore
{
    public string itemName;
    public int weightAmount;

    public ChanceItem(string _itemName, int _weightAmount)
    {
        itemName = _itemName;
        weightAmount = _weightAmount;
    }
    public int GetWeight(){ return weightAmount; }
}

interface IChanceScore {
    public int GetWeight();
}

class WeightedItemGiver<T> : ItemGiver<T> where T : IChanceScore
{
    public WeightedItemGiver(List<T> list) : base(list){}

    public T GetWeightedRandomItem() {
        int totalWeight = ItemList.Sum(item=>item.GetWeight());
        int chosenWeight = Random.Range(0, totalWeight);
        int baseWeight = chosenWeight;
        T foundItem = ItemList.Find(item =>
        {
            chosenWeight -= item.GetWeight();
            return chosenWeight <= 0;
        });

        Debug.Log($"totalWeight: {totalWeight} chosenWeight: {baseWeight} foundItem: {(foundItem as ChanceItem).itemName}");
        return foundItem;
    }
}