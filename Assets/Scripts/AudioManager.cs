using UnityEngine;

public class AudioManager : MonoBehaviour
{
    [SerializeField] private AudioSource _sfxAudioSource; 
    [SerializeField] private AudioSource _bikeAudioSource; 
    [SerializeField] private AudioClip _coinsCollect;
    [SerializeField] private AudioClip _buttonClick;
    [SerializeField] private AudioClip _jump;
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
    public void ButtonClick()
    {
        _sfxAudioSource.clip = _buttonClick;
        _sfxAudioSource.Play();
    } 
    public void Jump()
    {
        _sfxAudioSource.clip = _jump;
        _sfxAudioSource.Play();
    }
    public void LevelComplete()
    {
        _sfxAudioSource.clip = _levelComplete;
        _sfxAudioSource.Play();
    }
}
