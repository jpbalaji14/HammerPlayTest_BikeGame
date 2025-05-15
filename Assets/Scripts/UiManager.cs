using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UiManager : MonoBehaviour
{

    [SerializeField] private TextMeshProUGUI _coinsText;
    [SerializeField] private TextMeshProUGUI _timerText;
    [SerializeField] private Image _gameBgImage;
    [SerializeField] private Button _restartButton;
    public void UpdateCoinsText()
    {
        _coinsText.text = GameManager.Instance.Coins.ToString();
    } 
    public void UpdateTimerText( int _seconds, int milliseconds)
    {
        _timerText.text = $"{_seconds:00}:{milliseconds:00}";
    }
    public void ChangeGameBackground(Sprite sprite)
    {
        _gameBgImage.sprite = sprite;
    }
}
