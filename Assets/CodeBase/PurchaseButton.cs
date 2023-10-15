using System;
using UnityEngine;
using UnityEngine.UI;
using BlackBall.Services.PerGameServices;
using BlackBall.Services.SaveLoad;
using TMPro;


namespace BlackBall
{
    public class PurchaseButton : MonoBehaviour, IPersistentData
    {
        public SnapScrolling snapScrolling;

        public bool[]? purchasedItems;
        
        public Button purchaseButton;
        public TextMeshProUGUI buttonText;
        
        public SpriteRenderer ballSpriteRenderer;
        public Sprite[] ballSprites;
        
        public int[] objectPrices;
        private int loadMooney;
        
        private int selectedBallID = -1;

        private void Start()
        {
            purchaseButton.onClick.AddListener(HandlePurchaseOrSelect);
        }

        private void Update()
        {
            if (purchasedItems != null && snapScrolling != null)
            {
                if (purchasedItems[snapScrolling.selectedBallID])
                {
                    if (snapScrolling.selectedBallID == selectedBallID)
                    {
                        buttonText.text = "Selected";
                    } else
                    {
                        buttonText.text = "Select";
                    }
                } else
                {
                    buttonText.text = "Buy";
                }
            }
        }

        private void HandlePurchaseOrSelect()
        {
            if (purchasedItems[snapScrolling.selectedBallID])
            {
                if (snapScrolling.selectedBallID == selectedBallID)
                    return;
                ChangeSprite(snapScrolling.selectedBallID);
                selectedBallID = snapScrolling.selectedBallID;
            } else
            {
                if (loadMooney >= objectPrices[snapScrolling.selectedBallID])
                {
                    loadMooney -= objectPrices[snapScrolling.selectedBallID];
                    purchasedItems[snapScrolling.selectedBallID] = true;
                    ServiceLocator.ServiceLocatorInstance.SaveLoader.Save(null,this);
                }  else
                {
                    UnityEngine.Debug.Log("not money");
                }
            }
            selectedBallID = snapScrolling.selectedBallID;
            ServiceLocator.ServiceLocatorInstance.SaveLoader.Save(null,this);
        }
        
        private void ChangeSprite(int id)
        {
            if (ballSpriteRenderer && ballSprites.Length > id)
            {
                ballSpriteRenderer.sprite = ballSprites[id];
            } else
            {
                UnityEngine.Debug.LogWarning("Cannot change sprite. Check if references are set.");
            }
        }
        
        public void LoadData (Data data)
        { 
            loadMooney = data.CurrentMoney;
            if (data.PurchasedBalls != null && data.PurchasedBalls.Length > 0)
            {
                purchasedItems = data.PurchasedBalls;
            } else
            {
                purchasedItems = new bool[snapScrolling.ballCount];
            }
            if (data.SelectedBallID >= 0 && data.SelectedBallID < snapScrolling.ballCount)
            {
                selectedBallID = data.SelectedBallID;
                ChangeSprite(selectedBallID);
            }
        }
        public void SaveData (ref Data data)
        {
            data.CurrentMoney = loadMooney;
            data.PurchasedBalls = purchasedItems;
            data.SelectedBallID = selectedBallID;
        }

        private void OnEnable()
        {
            ServiceLocator.ServiceLocatorInstance.SaveLoader.Load(null, this);
        }
    }
}