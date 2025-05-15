using UnityEngine;

public class AudioManager : MonoBehaviour
{
    [SerializeField] private AudioSource _sfxAudioSource; 
    [SerializeField] private AudioClip _accelerateSound;
    [SerializeField] private AudioClip _brakeSound;
    [SerializeField] private AudioClip _coinsCollect;
    [SerializeField] private AudioClip _buttonClick;
    [SerializeField] private AudioClip _jump;
    [SerializeField] private AudioClip _levelComplete;

   public void Accelerate()
    {
        _sfxAudioSource.clip = _accelerateSound;
        _sfxAudioSource.Play();
    } 
    public void Brake()
    {
        _sfxAudioSource.clip = _brakeSound;
        _sfxAudioSource.Play();
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
