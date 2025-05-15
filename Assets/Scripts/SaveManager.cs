using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using UnityEngine;

public class SaveManager:MonoBehaviour
{
    [SerializeField] string _stringLevelData;
    [SerializeField] string _stringShopData;
    #region LevelData
    public void SaveLevelData(List<LevelDetails> _levelData) 
    {
        List<LevelDetails> levelDataList = new List<LevelDetails>();
        for (int i = 0; i < _levelData.Count; i++)
        {
            LevelDetails data = new LevelDetails();
            data.IsLocked = _levelData[i].IsLocked;
            data.IsCompleted = _levelData[i].IsCompleted;
            if (!string.IsNullOrEmpty(_levelData[i].BestTime))
                data.BestTime = _levelData[i].BestTime;
            levelDataList.Add(data);
        }
        string saveData = JsonConvert.SerializeObject(levelDataList);
        PlayerPrefs.SetString("LevelData", saveData);
        _stringLevelData = saveData;
    }
    public void GetLevelData(List<LevelDetails> _levelData)
    {
        _stringLevelData = PlayerPrefs.GetString("LevelData");
        if (!string.IsNullOrEmpty(_stringLevelData))
        {
             _levelData.Clear();
            List<LevelDetails> levelDataList = JsonConvert.DeserializeObject<List<LevelDetails>>(_stringLevelData);
            _levelData.AddRange(levelDataList);
            for (int i = 0; i < _levelData.Count; i++)
            {
                _levelData[i].LevelBackground = Resources.Load<Sprite>("LevelBg/Level_" + (i + 1));
            }
        }
        GameManager.Instance.MenuManager.SetUpLevelGameObjects();
    }
#endregion

    #region WalletData
    public void SaveWalletData() 
    {
       PlayerPrefs.SetInt("WalletData", GameManager.Instance.Coins);
    }
    public void GetWalletData()
    {
        if (PlayerPrefs.HasKey("WalletData"))
        {
            GameManager.Instance.Coins = PlayerPrefs.GetInt("WalletData");
        }
        GameManager.Instance.UiManager.UpdateCoinsText();
    }

    #endregion

    #region ShopData
    public void SaveShopData(List<ShopPurchaseData> _shopData)
    {
        List<ShopPurchaseData> shopDataList = new List<ShopPurchaseData>();
        for (int i = 0; i < _shopData.Count; i++)
        {
            ShopPurchaseData data = new ShopPurchaseData();
            data.IsUnlocked = _shopData[i].IsUnlocked;
            data.UnlockAmount = _shopData[i].UnlockAmount;
            data.IsEquipped = _shopData[i].IsEquipped;
            shopDataList.Add(data);
        }
        string saveData = JsonConvert.SerializeObject(shopDataList);
        PlayerPrefs.SetString("ShopData", saveData);
        _stringShopData = saveData;
    }
    public void GetShopData(List<ShopPurchaseData> _shopData)
    {
        _stringShopData = PlayerPrefs.GetString("ShopData");
        if (!string.IsNullOrEmpty(_stringShopData))
        {
            _shopData.Clear();
            List<ShopPurchaseData> shopDataList = JsonConvert.DeserializeObject<List<ShopPurchaseData>>(_stringShopData);
            _shopData.AddRange(shopDataList);
            for (int i = 0; i < _shopData.Count; i++)
            {
                _shopData[i].VehicleSprite = Resources.Load<Sprite>("Vehicles/Vehicle_" + (i + 1));
            }
        }
        GameManager.Instance.ShopManager.SetupShop();
    }
    #endregion
}
