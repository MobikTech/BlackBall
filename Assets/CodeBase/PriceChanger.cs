using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace BlackBall
{
  public class PriceChanger : MonoBehaviour
  {
    public SnapScrolling snapScrolling;

    public float[] prices;
    
    public PurchaseButton checkPurchased;

    public TextMeshProUGUI priceText;

    public string coinSpriteName = "Star_Coin";

    private void Update()
    {
      if (prices.Length == snapScrolling.ballCount && snapScrolling.selectedBallID < prices.Length)
      {
        if (checkPurchased.purchasedItems != null && snapScrolling.selectedBallID < checkPurchased.purchasedItems.Length)
        {
          if (checkPurchased.purchasedItems[snapScrolling.selectedBallID])
            priceText.text = "";
          else
            priceText.text = "Price: " + prices[snapScrolling.selectedBallID].ToString() + " " + coinSpriteName;
        }
      }
    }
  }
}