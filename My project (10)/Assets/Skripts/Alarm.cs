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

    private void OnEnable()
    {
        _sensor.ThiefEntered += TurnOn;
        _sensor.ThiefNotDetected += TurnOff;
    }

    private void OnDisable()
    {
        _sensor.ThiefEntered -= TurnOn;
        _sensor.ThiefNotDetected -= TurnOff;
    }

    private void TurnOn()
    {      
        CheckCoroutine();

        _isAlarmRunning = false;
        _coroutine = StartCoroutine(ChangeVolume());
    }

    private void TurnOff()
    {
        CheckCoroutine();

        _isAlarmRunning = true;
        _coroutine = StartCoroutine(ChangeVolume());
    }

    private void CheckCoroutine()
    {
        if (_coroutine != null)
        {
            StopCoroutine(_coroutine);
        }
    }

    private IEnumerator ChangeVolume()
    {
        float targetVolume;

        _audioSource.Play();

        while (enabled)
        {
            if (_isAlarmRunning == true)
            {
                targetVolume = _minVolume;
            }
            else
            {
                targetVolume = _maxVolume;
            }

            _audioSource.volume = Mathf.MoveTowards(_audioSource.volume, targetVolume, _changingRate * Time.deltaTime);
            yield return null;
        }

        if (_audioSource.volume == 0)
        {
            _audioSource.Stop();
        }
    }
}
