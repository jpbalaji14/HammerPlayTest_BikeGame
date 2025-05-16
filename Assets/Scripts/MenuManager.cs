using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using UnityEngine;
using System.Globalization;

[Serializable]
public class LevelDetails
{
    public bool IsLocked;
    public bool IsCompleted;
    public string BestTime;
    public string LevelBackgroundColorCode;
    //[Newtonsoft.Json.JsonIgnore] // Ignore this during serialization/deserialization
}

public class MenuManager : MonoBehaviour
{
    [Header("Panels")]
    [SerializeField] private GameObject _menuPanel;
    [SerializeField] private GameObject _levelSelectPanel;
    [SerializeField] private GameObject _controlsPanel;
    [SerializeField] private GameObject _shopPanel;
    [SerializeField] private GameObject _gameResultPanel;
    [SerializeField] private Animator _animator;

    [Header("LevelData")]
    public int CurrentLevelNumber;
    [SerializeField] private int _levelDataCurrentIndex;
    [SerializeField] private List<LevelDetails> _levelData; 
    [SerializeField] private List<GameObject> _levelPlayableGameobjectList; 
    [SerializeField] private GameObject _levelPrefab; 
    [SerializeField] private Transform _levelGameObjectSpawnTransform;
    [SerializeField] private List<LevelPrefabData> _levelGameObjectList;

    private void Start()
    {
    }

    #region MenuPanelsOpenCloseFunctions
    public void OpenLevelSelect()
    {
        UpdateLevelData();
        _levelSelectPanel.SetActive(true);
        _animator.Play("LevelSelectOpen");
    }
    public void CloseLevelSelect()
    {
        _menuPanel.SetActive(true);
        _animator.Play("LevelSelectClose");
    }

    public void OpenControls()
    {
        _controlsPanel.SetActive(true);
        _animator.Play("ControlsOpen");
    }
    public void CloseControls()
    {
        _menuPanel.SetActive(true);
        _animator.Play("ControlsClose");
    } 
    
    public void OpenShop()
    {
        _controlsPanel.SetActive(true);
        _animator.Play("ShopOpen");
    }
    public void CloseShop()
    {
        _menuPanel.SetActive(true);
        _animator.Play("ShopClose");
    }

    public void OpenMenu()
    {
        _menuPanel.SetActive(true);
        _animator.Play("MenuOpen");
    }
    public void OpenGameResult()
    {
        _gameResultPanel.SetActive(true);
    }
    public void CloseGameResult()
    {
        _gameResultPanel.SetActive(false);
    }
    public void QuitGame()
    {
        Application.Quit();
    }

    #endregion

    public void SetUpLevelGameObjects()
    {
        Debug.Log("SetUpLevelGameObjects");
        for (int i = 0; i < _levelData.Count; i++)
        {
            GameObject _levelGameobject = Instantiate(_levelPrefab, _levelGameObjectSpawnTransform);
            _levelGameobject.name = "GameLevelMenuPrefab"+(i+1);
            LevelPrefabData _levelPrefabData = _levelGameobject.GetComponent<LevelPrefabData>();
            _levelPrefabData.LevelNumber = i + 1;
            _levelGameObjectList.Add(_levelPrefabData);
            _levelPrefabData.CheckLevelLockState(_levelData[i].IsLocked);
            _levelPrefabData.UpdateLevelInfo(_levelData[i].BestTime);
        }
    }

    public void UpdateLevelData()
    {
        Debug.Log("UpdateLevelData");
        for (int i = 0; i < _levelData.Count; i++)
        {
            _levelPlayableGameobjectList[i].gameObject.SetActive(false); //Disables All Game Playable Level Prefab from Hierarchy
           // Debug.Log("<<<<" + i +" is "+ _levelData[i].IsLocked);
            _levelGameObjectList[i].CheckLevelLockState(_levelData[i].IsLocked); //Check if Level is Unlocked and removes lock gameobject from level selection panel game level prefab
            _levelGameObjectList[i].UpdateLevelInfo(_levelData[i].BestTime); //Check if Level is Unlocked and removes lock gameobject from level selection panel game level prefab
        }
    }


    public void OpenGameLevel(int levelNumber)
    {
        Debug.Log("Opened Game Level "+ levelNumber);
        CurrentLevelNumber = levelNumber;
        _levelDataCurrentIndex = CurrentLevelNumber - 1;
        _levelPlayableGameobjectList[_levelDataCurrentIndex].gameObject.SetActive(true);
        GameManager.Instance.TimerController.ResetTimer();
        GameManager.Instance.UiManager.ChangeGameBackground(_levelData[_levelDataCurrentIndex].LevelBackgroundColorCode);
        _menuPanel.SetActive(false);
        _animator.Play("LevelSelectClose");
        GameManager.Instance.SpawnBike();
    }

    public void RestartGameLevel()
    {
        Debug.Log("Game Restart");
        GameManager.Instance.BikeController.DestroyBike();
        GameManager.Instance.TimerController.StopTimer();
        GameManager.Instance.TimerController.ResetTimer();
        GameManager.Instance.SpawnBike();
    }

    public void OnGameLevelComplete()
    {
        Debug.Log("GameLevelComplete");
        GameManager.Instance.TimerController.StopTimer();
       _levelData[_levelDataCurrentIndex].IsCompleted = true;
        GameManager.Instance.RewardManager.RewardPlayer();
        if (CurrentLevelNumber <= _levelData.Count)
        {
            Debug.Log("<");
            if (CurrentLevelNumber < _levelData.Count)
            {
                Debug.Log("<<<");
                _levelData[CurrentLevelNumber].IsLocked = false;
            }
            CheckAndUpdateQuickerGameFinishTime();
        }
        SaveLevelData();
        GameManager.Instance.SaveManager.SaveWalletData();
        OpenGameResult();
        GameManager.Instance.BikeController.DestroyBike();

    }

    public void OnMenuClickFromGame()
    {
       Invoke(nameof(ResetTimerAndBike),0.8f);
    }
    void ResetTimerAndBike()
    {
        GameManager.Instance.TimerController.StopTimer();
        GameManager.Instance.BikeController.DestroyBike();
    }
    void CheckAndUpdateQuickerGameFinishTime()
    {
        if (!string.IsNullOrEmpty(_levelData[_levelDataCurrentIndex].BestTime))
        {
            int _totalMillisA = GameManager.Instance.TimerController.ConvertToTotalMilliseconds(_levelData[_levelDataCurrentIndex].BestTime);
            int _totalMillisB = GameManager.Instance.TimerController.ConvertToTotalMilliseconds(GameManager.Instance.UiManager.GetTimerValueAsString());
            if (_totalMillisB < _totalMillisA)
            {
                Debug.Log("fast");
                _levelData[_levelDataCurrentIndex].BestTime = GameManager.Instance.UiManager.GetTimerValueAsString();
                GameManager.Instance.UiManager.ShowResultTime(_levelData[_levelDataCurrentIndex].BestTime, true);
            }
            else
            {
                Debug.Log("Not so fast");
                GameManager.Instance.UiManager.ShowResultTime(GameManager.Instance.UiManager.GetTimerValueAsString(), false);
            }
        }
        else
        {
            _levelData[_levelDataCurrentIndex].BestTime = GameManager.Instance.UiManager.GetTimerValueAsString();

            GameManager.Instance.UiManager.ShowResultTime(_levelData[_levelDataCurrentIndex].BestTime, true);
        }
    }
    [ContextMenu("SavePRefs")]
    public void SaveLevelData()
    {
       GameManager.Instance.SaveManager.SaveLevelData(_levelData);
    }
  
    [ContextMenu("GetPRefs")]
    public void GetLevelData()
    {
        Debug.Log("GetLevelData");
        GameManager.Instance.SaveManager.GetLevelData(_levelData);
        
    }
}
