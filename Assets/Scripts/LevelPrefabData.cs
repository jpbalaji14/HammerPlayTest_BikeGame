using TMPro;
using UnityEngine;

public class LevelPrefabData : MonoBehaviour
{
    public int LevelNumber;
    [SerializeField] private GameObject _lockGameobject;
    [SerializeField] private TextMeshProUGUI _levelNameText;
    public void CheckLevelLockState(bool _isLevelLocked)
    {
        if (_isLevelLocked) _lockGameobject.SetActive(true);
        //keep lock false by default
    }
    public void UpdateLevelInfo()
    {
        _levelNameText.text="Level "+LevelNumber;
    }
    public void OnLevelClicked()
    {
        GameManager.Instance.MenuManager.OpenGameLevel(LevelNumber);
    }
}
