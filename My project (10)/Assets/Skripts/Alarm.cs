using System.Collections;
using UnityEngine;

public class Alarm : MonoBehaviour
{
    [SerializeField] private AudioSource _audioSource;
    [SerializeField] private Sensor _sensor;

    private Coroutine _coroutine;
    private float _minVolume = 0f;
    private float _maxVolume = 1f;
    private float _changingRate = 0.1f;
    private bool _isAlarmRunning = false;

    public void TurnOn()
    {
        _isAlarmRunning = false;
        _audioSource.Play();
        _coroutine = StartCoroutine(ChangeVolume(_maxVolume));
    }

    public void TurnOff()
    {
        _isAlarmRunning = true;

        _coroutine = StartCoroutine(ChangeVolume(_minVolume));

        if (_audioSource.volume == 0)
        {
            _audioSource.Stop();
        }
    }

    private IEnumerator ChangeVolume(float targetVolume)
    {
        while ((_isAlarmRunning == false && _audioSource.volume < targetVolume) || (_isAlarmRunning == true && _audioSource.volume > targetVolume))
        {
            _audioSource.volume = Mathf.MoveTowards(_audioSource.volume, targetVolume, _changingRate * Time.deltaTime);
            
            yield return null;
        }
    }
}
