using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

[Serializable]
public class LevelDetails
{
    public bool IsLocked;
    public bool IsCompleted;
    [Newtonsoft.Json.JsonIgnore] // Ignore this during serialization/deserialization
    public Sprite LevelBackground;
}

public class MenuManager : MonoBehaviour
{
    [Header("Panels")]
    [SerializeField] private GameObject _menuPanel;
    [SerializeField] private GameObject _levelSelectPanel;
    [SerializeField] private GameObject _controlsPanel;
    [SerializeField] private GameObject _shopPanel;
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
            _levelPrefabData.UpdateLevelInfo();
        }
    }

    public void UpdateLevelData()
    {
        Debug.Log("UpdateLevelData");
        for (int i = 0; i < _levelData.Count; i++) 
        {
            _levelPlayableGameobjectList[i].gameObject.SetActive(false); //Disables All Game Playable Level Prefab from Hierarchy
            _levelGameObjectList[i].CheckLevelLockState(_levelData[i].IsLocked); //Check if Level is Unlocked and removes lock gameobject from level selection panel game level prefab
        }
    }


    public void OpenGameLevel(int levelNumber)
    {

        Debug.Log("Opened Game Level "+ levelNumber);
        CurrentLevelNumber = levelNumber;
        _levelDataCurrentIndex = CurrentLevelNumber - 1;
        _levelPlayableGameobjectList[_levelDataCurrentIndex].gameObject.SetActive(true);
        //GameManager.Instance.UiManager.ChangeGameBackground(_levelData[_levelDataCurrentIndex].LevelBackground);
        _menuPanel.SetActive(false);
        _animator.Play("LevelSelectClose");
        GameManager.Instance.MobileController.EnableMobileControls();
        GameManager.Instance.BikeController.gameObject.SetActive(true);
        GameManager.Instance.TimerController.TimerStart();
    }

    public void RestartGameLevel()
    {
        //GameManager.Instance.UiManager.SetupGearsonGameUI();
        //GameManager.Instance.UiManager.CloseGameResult();
        //_levelGameObjectList[CurrentLevel - 1].SetActive(true);
        //GameManager.Instance.Player.gameObject.SetActive(true);
        //GameManager.Instance.Player.ResetPlayer();
        //for (int i = 0; i < _levelItemsList[CurrentLevel - 1].Gear.transform.childCount; i++)
        //{
        //    _levelItemsList[CurrentLevel - 1].Gear.transform.GetChild(i).gameObject.SetActive(true);
        //}
        //for (int i = 0; i < _levelItemsList[CurrentLevel - 1].Enemy.transform.childCount; i++)
        //{
        //    _levelItemsList[CurrentLevel - 1].Enemy.transform.GetChild(i).gameObject.SetActive(true);
        //}
        //GameManager.Instance.MobileController.EnableMobileControls();
        Debug.Log("Game Restart");
        GameManager.Instance.BikeController.gameObject.SetActive(true);
        GameManager.Instance.MobileController.EnableMobileControls();
        GameManager.Instance.TimerController.TimerStart();
    }

    public void OnGameLevelComplete()
    {
        Debug.Log("GameLevelComplete");
        _levelData[_levelDataCurrentIndex].IsCompleted = true;
        GameManager.Instance.RewardManager.RewardPlayer();
        if (CurrentLevelNumber < _levelData.Count) _levelData[CurrentLevelNumber].IsLocked = false;
        SaveLevelData();
        GameManager.Instance.SaveManager.SaveWalletData();
        //OpenGameResult Panel Here and redirect to level selection
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
