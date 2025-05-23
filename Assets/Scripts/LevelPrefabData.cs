using TMPro;
using UnityEngine;

public class LevelPrefabData : MonoBehaviour
{
    public int LevelNumber;
    [SerializeField] private GameObject _lockGameobject;
    [SerializeField] private TextMeshProUGUI _levelNameText;
    [SerializeField] private TextMeshProUGUI _timeValueText;
    public void CheckLevelLockState(bool _isLevelLocked)
    {
        if (_isLevelLocked) { _lockGameobject.SetActive(true); }
        else {
            _lockGameobject.SetActive(false);
        }
        //keep lock false by default
    }
    public void UpdateLevelInfo(string timeValue)
    {
        _levelNameText.text="Level "+LevelNumber;
        _timeValueText.text= string.IsNullOrEmpty(timeValue) ? "_ _ : _ _" : timeValue;
    }
    public void OnLevelClicked()
    {
        GameManager.Instance.AudioManager.ButtonClick();
        GameManager.Instance.MenuManager.OpenGameLevel(LevelNumber);
    }
}
