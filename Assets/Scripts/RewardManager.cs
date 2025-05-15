using UnityEngine;

public class RewardManager : MonoBehaviour
{
    [SerializeField] private int _rewardAmount;
    
    public void RewardPlayer()
    {
        GameManager.Instance.Coins += _rewardAmount;
    }
}
