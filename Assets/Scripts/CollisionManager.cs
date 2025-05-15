using DG.Tweening;
using UnityEngine;

public class CollisionManager : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Finish")
        {
            GameManager.Instance.MenuManager.OnGameLevelComplete();
            GameManager.Instance.AudioManager.LevelComplete();
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (this.gameObject.tag == "PlayerBody" && collision.gameObject.tag == "Ground")
        {
            GameManager.Instance.BikeController.CrashBike();
            Invoke(nameof(RestartGame),2f);
        } 
        
        if (this.gameObject.tag == "PlayerBody" && collision.gameObject.tag == "Obstacle")
        {
            GameManager.Instance.BikeController.CrashBike();
            Invoke(nameof(RestartGame),2f);
        }

    }
    void RestartGame()
    {
        GameManager.Instance.MenuManager.RestartGameLevel();
    }
}
