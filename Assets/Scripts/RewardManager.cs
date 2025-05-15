using DG.Tweening;
using System.Collections;
using UnityEngine;

public class RewardManager : MonoBehaviour
{
    [SerializeField] private int _rewardAmount;

    [Header("CoinReward Particles")]
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
        SetUpParticles(GameManager.Instance.UiManager.ResultCoinParent, GameManager.Instance.UiManager.ResultCoinStartPos,GameManager.Instance.UiManager.ResultCoinEndPos);
        for (int i = 0; i < _rewardAmount; i++) 
        {
            var _targetDelay = i*_coinDelay;
            ShowCoinparticles(_targetDelay);
        }
        
        StartCoroutine(MoveToNextLevel());
    }
    public void OnShopPurchaseClicked()
    {
        SetUpParticles(GameManager.Instance.UiManager.PurchaseCoinParent, GameManager.Instance.UiManager.PurchaseCoinStartPos,GameManager.Instance.UiManager.PurchaseCoinEndPos);
        for (int i = 0; i < _rewardAmount; i++) 
        {
            var _targetDelay = i*_coinDelay;
            ShowCoinparticles(_targetDelay);
        }
    }

    void SetUpParticles(Transform _parent, Transform _start, Transform _end)
    {
        _coinParent = _parent;
        _coinStartPos = _start;
        _coinEndPos = _end;
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
        GameManager.Instance.UiManager.UpdateCoinsText();
        GameManager.Instance.MenuManager.OpenLevelSelect();
        yield return new WaitForSeconds(1f);
        GameManager.Instance.MenuManager.CloseGameResult();
    }

   
}
