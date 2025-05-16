using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

[Serializable]
public class ShopPurchaseData
{
    public int ItemIndex;
    public bool IsUnlocked;
    public int UnlockAmount;
    public bool IsEquipped;
    [Newtonsoft.Json.JsonIgnore] // Ignore this during serialization/deserialization
    public Sprite VehicleSprite;
}
public class ShopManager : MonoBehaviour
{
    [SerializeField] private int _currentItemIndex;
    [SerializeField] private int _currentEquippedItemIndex;
    [SerializeField] private bool _isTransitioning=false;
    [SerializeField] private Image _itemImage;
    [SerializeField] private RectTransform _itemTransform;
    [SerializeField] private Button _previousButton;
    [SerializeField] private Button _nextButton;
    [SerializeField] private GameObject _lockGameobject;
    [SerializeField] private List<ShopPurchaseData> _shopItemsDataList;
    
    [SerializeField] private Button _purchaseButton;
    [SerializeField] private TextMeshProUGUI _purchaseAmountText;
    
    [SerializeField] private Button _equipButton;
    [SerializeField] private TextMeshProUGUI _equipText;
   
    [SerializeField] private GameObject _noMoneyPopup;

    public void SetupShop()
    {
        _itemImage.sprite = _shopItemsDataList[_currentItemIndex].VehicleSprite;
        UpdateButtonsAndText();
    }
    public void NextClick()
    {
        ChangeItem(1);
    }
    public void PreviousClick()
    {
        ChangeItem(-1);
    }
    void ChangeItem(int _direction)
    {
        if (_isTransitioning) return;
       
        int _newIndex = _currentItemIndex + _direction;
        if (_newIndex < 0 || _newIndex >= _shopItemsDataList.Count) return;

        _isTransitioning = true;
        SetupButtonsAndText(false);

        float _exitPos = _direction > 0 ? 950 : -950;
        float _enterPos = -_exitPos;

        _itemTransform.DOAnchorPosX(_exitPos, 0.2f).OnComplete(() =>
        {
            _itemTransform.anchoredPosition = new Vector2(_enterPos, 0);
            _currentItemIndex = _newIndex;
            _itemImage.sprite = _shopItemsDataList[_currentItemIndex].VehicleSprite;
 
            _itemTransform.DOAnchorPosX(0, 0.4f).OnComplete(() =>
            {
                _lockGameobject.SetActive(!_shopItemsDataList[_currentItemIndex].IsUnlocked);
                UpdateButtonsAndText();
                _isTransitioning = false;
            });
        });
    }

    void UpdateButtonsAndText()
    {
        _previousButton.interactable = _currentItemIndex > 0;
        _nextButton.interactable = _currentItemIndex < _shopItemsDataList.Count - 1;

        _purchaseButton.interactable = !_shopItemsDataList[_currentItemIndex].IsUnlocked && _shopItemsDataList[_currentItemIndex].UnlockAmount > 0 ? true : false;
        _purchaseAmountText.text = !_shopItemsDataList[_currentItemIndex].IsUnlocked &&  _shopItemsDataList[_currentItemIndex].UnlockAmount > 0 ? _shopItemsDataList[_currentItemIndex].UnlockAmount.ToString() : "Unlocked";
        GameManager.Instance.UiManager.CheckPurchaseState(_shopItemsDataList[_currentItemIndex].IsUnlocked, _shopItemsDataList[_currentItemIndex].UnlockAmount);

        _equipButton.interactable =  _shopItemsDataList[_currentItemIndex].IsUnlocked && !_shopItemsDataList[_currentItemIndex].IsEquipped ? true : false;
        _equipText.text = _shopItemsDataList[_currentItemIndex].IsEquipped ? "Equipped" : "Equip";
    }

    void SetupButtonsAndText(bool _state)
    {
        _previousButton.interactable = _state;
        _nextButton.interactable = _state;
        _purchaseButton.interactable = _state;
        _equipButton.interactable = _state;
        _lockGameobject.SetActive(_state);
    }

    public void OnPurchaseItem()
    {
        if (_shopItemsDataList[_currentItemIndex].UnlockAmount <= GameManager.Instance.Coins)
        {
           
            GameManager.Instance.RewardManager.OnShopPurchaseClicked();
            GameManager.Instance.AudioManager.CoinsDeduct();
            _lockGameobject.SetActive(false);
            _shopItemsDataList[_currentItemIndex].IsUnlocked = true;
            SortShopList();
            GameManager.Instance.Coins -= _shopItemsDataList[_currentItemIndex].UnlockAmount;
            GameManager.Instance.UiManager.UpdateCoinsText();
            GameManager.Instance.SaveManager.SaveWalletData();
            UpdateButtonsAndText();
            SaveShopData();
        }
        else
        {
            StartCoroutine(ShowNoMoneyPopup());
        }
    }
    public void EquipItem()
    {
        for (int i = 0; i < _shopItemsDataList.Count; i++)
        {
            _shopItemsDataList[i].IsEquipped = false;
            if (i == _currentItemIndex)
            {
                _shopItemsDataList[i].IsEquipped = true;
            }
        }
       _currentEquippedItemIndex = _currentItemIndex;
        UpdateButtonsAndText();
        SaveShopData();
    }
    IEnumerator ShowNoMoneyPopup()
    {
        _noMoneyPopup.SetActive(true);
        yield return new WaitForSeconds(1.5f);
        _noMoneyPopup.SetActive(false);
    }

    void SortShopList()
    {
        
        ShopPurchaseData unlockedItem = _shopItemsDataList[_currentItemIndex];
        _shopItemsDataList = _shopItemsDataList.OrderByDescending(item => item.IsUnlocked).ToList();
        _currentItemIndex = _shopItemsDataList.IndexOf(unlockedItem);
    }

    #region DataSave and Retrieve
    [ContextMenu("SavePRefs")]
    public void SaveShopData()
    {
        GameManager.Instance.SaveManager.SaveShopData(_shopItemsDataList);
    }

    [ContextMenu("GetPRefs")]
    public void GetShopData()
    {
        GameManager.Instance.SaveManager.GetShopData(_shopItemsDataList);
    }

    public int GetCurrentEquippedIndex()
    {
        return _currentEquippedItemIndex;
    } 
    public int GetCurrentListEquippedIndex()
    {
        return _shopItemsDataList[_currentEquippedItemIndex].ItemIndex;
    } 

    public void SetCurrentEquippedIndex(int _value)
    {
        _currentEquippedItemIndex = _value;
        _currentItemIndex = _currentEquippedItemIndex;
    }
    #endregion
}
