using DG.Tweening;
using System.Collections;
using UnityEngine;

public class RewardManager : MonoBehaviour
{
    [SerializeField] private int _rewardAmount;

    [Header("Particles")]
    [SerializeField] private GameObject _coinPrefab;
    [SerializeField] private Transform _coinParent;
    [SerializeField] private Transform _coinStartPos;
    [SerializeField] private Transform _coinEndPos;
    [SerializeField] private float _moveDuration;
    [SerializeField] private float _coinDelay;
    [SerializeField] private Ease _moveEase;

    public void RewardPlayer()
    {
        GameManager.Instance.Coins += _rewardAmount;
    }

    public void OnClaimRewardClicked()
    {
        for (int i = 0; i < _rewardAmount; i++) 
        {
            var _targetDelay = i*_coinDelay;
            ShowCoinparticles(_targetDelay);
        }
        StartCoroutine(MoveToNextLevel());
    }
    void ShowCoinparticles(float delay)
    {
        var _coinObj = Instantiate(_coinPrefab,_coinParent);
        var _offset = new Vector3(Random.Range(-20, 20), Random.Range(-20, 20), 0);
        var _startPosition = _offset + _coinStartPos.position;
        _coinObj.transform.position = _startPosition;
        _coinObj.transform.DOMove(_coinEndPos.position, _moveDuration).SetEase(_moveEase).SetDelay(delay).OnComplete(() => { Destroy(_coinObj); });
    }

    IEnumerator MoveToNextLevel()
    {
        yield return new WaitForSeconds(2f);
        GameManager.Instance.MenuManager.OpenLevelSelect();
        yield return new WaitForSeconds(1f);
        GameManager.Instance.MenuManager.CloseGameResult();
        GameManager.Instance.BikeController.gameObject.SetActive(false);
    }

    //void CloseGameResult()
    //{
    //    GameManager.Instance.MenuManager.CloseGameResult();
    //}
}
