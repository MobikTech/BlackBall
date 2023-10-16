using System;
using UnityEngine;
using UnityEngine.UI;
using BlackBall.Services.PerGameServices;
using BlackBall.Services.SaveLoad;
using TMPro;

namespace BlackBall
{
    public class PurchaseButton : MonoBehaviour, IPersistentData, IScrollable
    {
        public  SnapScrolling snapScrolling;

        public bool[]? purchasedItems;
        
        public Button purchaseButton;
        public TextMeshProUGUI buttonText;
        
        public BallSpawner ballSpawn;
        public GameObject[] customPrefabs;
        
        public int[] objectPrices;
        private int loadMooney;
        
        private int selectedBallID = -1;
        int IScrollable.selectedBallID => selectedBallID;
        GameObject[] IScrollable.ballPrefabs => customPrefabs ?? snapScrolling.ballPrefabs;
        int IScrollable.ballCount => customPrefabs?.Length ?? snapScrolling.ballCount;

        private void Start()
        {
            purchaseButton.onClick.AddListener(HandlePurchaseOrSelect);
        }

        private void Update()
        {
            UpdateButtonText();
        }

        private void UpdateButtonText()
        {
            if (purchasedItems == null || snapScrolling == null) return;
            
            buttonText.text = purchasedItems[snapScrolling.selectedBallID] 
                ? (snapScrolling.selectedBallID == selectedBallID ? "Selected" : "Select") 
                : "Buy";
        }

        private void HandlePurchaseOrSelect()
        {
            GameObject prefabToSpawn = GetPrefabToSpawn();
            
            if (purchasedItems[snapScrolling.selectedBallID])
            {
                SelectBall(prefabToSpawn);
            }
            else
            {
                PurchaseBall();
            }

            SaveCurrentState();
        }

        private GameObject GetPrefabToSpawn()
        {
            if (customPrefabs != null && customPrefabs.Length > snapScrolling.selectedBallID && customPrefabs[snapScrolling.selectedBallID] != null)
            {
                return customPrefabs[snapScrolling.selectedBallID];
            }
            return snapScrolling.ballPrefabs[snapScrolling.selectedBallID];
        }

        private void SelectBall(GameObject prefabToSpawn)
        {
            if (snapScrolling.selectedBallID == selectedBallID) return;
            
            selectedBallID = snapScrolling.selectedBallID;
        }

        private void PurchaseBall()
        {
            if (loadMooney < objectPrices[snapScrolling.selectedBallID]) 
            {
                UnityEngine.Debug.Log("not money");
                return;
            }

            loadMooney -= objectPrices[snapScrolling.selectedBallID];
            purchasedItems[snapScrolling.selectedBallID] = true;
        }

        private void SaveCurrentState()
        {
            
            ServiceLocator.ServiceLocatorInstance.SaveLoader.Save(null, this);
        }

        public void LoadData(Data data)
        {
            loadMooney = data.CurrentMoney;
            purchasedItems = data.PurchasedBalls ?? new bool[snapScrolling.ballCount];
            if (data.SelectedBallID >= 0 && data.SelectedBallID < snapScrolling.ballCount)
            {
                selectedBallID = data.SelectedBallID;
            }
        }

        public void SaveData(ref Data data)
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