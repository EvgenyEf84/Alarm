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

    private void TurnOn()
    {
        if (_coroutine != null)
        {
            StopCoroutine(_coroutine);
        }

        _coroutine = StartCoroutine(VolumeUp());
    }

    private void TurnOff()
    {
        if (_coroutine != null)
        {
            StopCoroutine(_coroutine);
        }

        _coroutine = StartCoroutine(VolumeDown());
    }

    private IEnumerator VolumeUp()
    {
        _audioSource.Play();
        _audioSource.volume = _minVolume;

        while (_audioSource.volume < _maxVolume)
        {
            _audioSource.volume = Mathf.MoveTowards(_audioSource.volume, _maxVolume, _changingRate * Time.deltaTime);
            yield return null;
        }
    }

    private IEnumerator VolumeDown()
    {
        while (_audioSource.volume > _minVolume)
        {
            _audioSource.volume = Mathf.MoveTowards(_audioSource.volume, _minVolume, _changingRate * Time.deltaTime);
            yield return null;
        }

        _audioSource.Stop();
    }
}
