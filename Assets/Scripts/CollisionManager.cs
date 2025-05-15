using DG.Tweening;
using UnityEngine;

public class CollisionManager : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        //if (collision.gameObject.tag == "Enemy")
        //{
        //    if (collision.gameObject.name.Contains("Enemy"))
        //    {
        //        GameManager.Instance.AudioManager.HitSpike();
        //    }
        //    Invoke(nameof(GameOver), 1f);
        //}
        
        if(collision.gameObject.tag == "Finish")
        {
            GameManager.Instance.MenuManager.OnGameLevelComplete();
            GameManager.Instance.AudioManager.LevelComplete();
        }
    }

   void GameOver()
    {
        //Restart Game
    }
}
