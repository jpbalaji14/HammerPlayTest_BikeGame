using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UiManager : MonoBehaviour
{

    [SerializeField] private TextMeshProUGUI _coinsText;
    [SerializeField] private TextMeshProUGUI _timerText;
    [SerializeField] private Button _restartButton;
    [SerializeField] private GameObject _purchaseIconGameobject;
    [SerializeField] private Camera _mainCamera;

    [Header("ResultCoinParticles")]
    public Transform _resultCoinParent;
    public Transform _resultCoinStartPos;
    public Transform _resultCoinEndPos;
    
    [Header("PurchaseCoinParticles")]
    public Transform _purchaseCoinParent;
    public Transform _purchaseCoinStartPos;
    public Transform _purchaseCoinEndPos;
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
}
