using UnityEngine;
using TMPro;  // Import TextMeshPro namespace

public class PlayerController : MonoBehaviour
{
    public TextMeshProUGUI coinText;  // Assign this in the Inspector
    private int totalCoins;

    // Call this method when a coin is collected
    public void CollectCoin(int coinValue)
    {
        totalCoins += coinValue;
        UpdateCoinText();
    }

    // Update the coin text display
    private void UpdateCoinText()
    {
        coinText.text = "Coins: " + totalCoins.ToString();
    }
}

