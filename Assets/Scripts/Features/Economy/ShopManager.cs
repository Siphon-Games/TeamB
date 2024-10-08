using TMPro;
using UnityEngine;

public class ShopManager : MonoBehaviour
{
    [SerializeField]
    private int playerGold;

    [SerializeField]
    private Currency<int> playerSteel;

    [SerializeField]
    private TextMeshProUGUI messageText;

    [SerializeField]
    private TextMeshProUGUI playerGoldText;

    private SomeItem<int> apple = new SomeItem<int>();

    private void Start()
    {
        playerGoldText.text = playerGold.ToString() + "g";

        apple.SetValue(CurrencyType.Gold, 10);
    }

    public void Buy()
    {
        int appleCost = apple.GetValue(CurrencyType.Gold);

        if (playerGold >= appleCost)
        {
            playerGold -= appleCost;
            messageText.text = "You bought an apple!";
        }
        else
        {
            messageText.text = "You don't have enough gold!";
        }

        UpdateUI();
    }

    private void UpdateUI()
    {
        playerGoldText.text = playerGold.ToString();
    }
}
