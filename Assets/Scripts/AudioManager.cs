using UnityEngine;

public class AudioManager : MonoBehaviour
{
    [SerializeField] private AudioSource _sfxAudioSource; 
    [SerializeField] private AudioSource _bikeAudioSource; 
    [SerializeField] private AudioClip _coinsCollect;
    [SerializeField] private AudioClip _coinsDeduct;
    [SerializeField] private AudioClip _buttonClick;
    [SerializeField] private AudioClip _levelComplete;

    public void SetUpBikeAudioSource(AudioSource _source)
    {
        _bikeAudioSource = _source;
    }
   public void Accelerate()
   {
        _bikeAudioSource.enabled=true;
   } 
    public void Idle()
    {
        if(_bikeAudioSource!=null)
        _bikeAudioSource.enabled = false;
    }

    public void CoinsCollect()
    {
        _sfxAudioSource.clip = _coinsCollect;
        _sfxAudioSource.Play();
    }
    public void CoinsDeduct()
    {
        _sfxAudioSource.clip = _coinsDeduct;
        _sfxAudioSource.Play();
    }
    public void ButtonClick()
    {
        _sfxAudioSource.clip = _buttonClick;
        _sfxAudioSource.Play();
        
        //test
    } 
    
    public void LevelComplete()
    {
        _sfxAudioSource.clip = _levelComplete;
        _sfxAudioSource.Play();

        //test2
    }
}
