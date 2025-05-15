using TMPro;
using Unity.Cinemachine;
using UnityEngine;
using UnityEngine.UI;

public class UiManager : MonoBehaviour
{

    [SerializeField] private TextMeshProUGUI _coinsText;
    [SerializeField] private TextMeshProUGUI _timerText;
    [SerializeField] private Button _restartButton;
    [SerializeField] private GameObject _purchaseIconGameobject;
    [SerializeField] private Camera _mainCamera;
    [SerializeField] private CinemachineCamera _followCam;
    
    [Header("ResultTimeDisplay")]
    [SerializeField] private TextMeshProUGUI _resultTimerText;
    [SerializeField] private TextMeshProUGUI _resultBestTimerText;
    [SerializeField] private GameObject _BestTimeGameobject;

    [Header("ResultCoinParticles")]
    public Transform ResultCoinParent;
    public Transform ResultCoinStartPos;
    public Transform ResultCoinEndPos;
    
    [Header("PurchaseCoinParticles")]
    public Transform PurchaseCoinParent;
    public Transform PurchaseCoinStartPos;
    public Transform PurchaseCoinEndPos;
    
    [Header("Bike1")]
    [SerializeField] private Sprite _bikeBody1;
    [SerializeField] private Sprite _bikeHead1;
   
    [Header("Bike2")]
    [SerializeField] private Sprite _bikeBody2;
    [SerializeField] private Sprite _bikeHead2;
    
    [Header("Bike3")]
    [SerializeField] private Sprite _bikeBody3;
    [SerializeField] private Sprite _bikeHead3;
    public void UpdateCoinsText()
    {
        _coinsText.text = GameManager.Instance.Coins.ToString();
    } 
    public void UpdateTimerText( int _seconds, int _milliseconds)
    {
        _timerText.text = $"{_seconds:00}:{_milliseconds:00}";
    }
    public string GetTimerValueAsString()
    {
        return _timerText.text;
    }
    public void ChangeGameBackground(string _colorCode)
    {
        Debug.Log("Change Color");
        Color newColor;
        if (ColorUtility.TryParseHtmlString("#" + _colorCode, out newColor))
        {
            _mainCamera.backgroundColor = newColor;
        }
    }
    public void CheckPurchaseState(bool _isUnlocked, int _unlockAmount)
    {
        _purchaseIconGameobject.SetActive(!_isUnlocked && _unlockAmount > 0);
    }

    public void SetupCamBikeTracking(Transform _bikeTransform)
    {
        _followCam.Follow = _bikeTransform;
    }

    public void ShowResultTime(string _time, bool _isActive)
    {
        _resultTimerText.text = _time + " sec";
        _BestTimeGameobject.SetActive(_isActive);
        _resultBestTimerText.text = _time + " sec";
    }

    public void SetupBikeSprite(SpriteRenderer _bikeBody, SpriteRenderer _bikeHead)
    {
        switch (GameManager.Instance.ShopManager.GetCurrentListEquippedIndex())
        {
            case 0:
                _bikeBody.sprite = _bikeBody1;
                _bikeHead.sprite = _bikeHead1;
                break; 
            case 1:
                _bikeBody.sprite = _bikeBody2;
                _bikeHead.sprite = _bikeHead2;
                break;
            case 2:
                _bikeBody.sprite = _bikeBody3;
                _bikeHead.sprite = _bikeHead3;
                break;
        }
    }

}
