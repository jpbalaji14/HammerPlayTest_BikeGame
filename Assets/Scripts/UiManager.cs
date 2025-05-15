using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UiManager : MonoBehaviour
{

    [SerializeField] private TextMeshProUGUI _coinsText;
    [SerializeField] private TextMeshProUGUI _timerText;
    [SerializeField] private Image _gameBgImage;
    public void UpdateCoinsText()
    {
        _coinsText.text = GameManager.Instance.Coins.ToString();
    } 
    public void UpdateTimerText(int _minutes, int _seconds)
    {
        _timerText.text = $"{_minutes:00}:{_seconds:00}";
    }
    public void ChangeGameBackground(Sprite sprite)
    {
        _gameBgImage.sprite = sprite;
    }

}
