using System.Collections;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class Radio : MonoBehaviour
{
    [SerializeField] private AudioClip[] _clips;
    [SerializeField] private AudioClip _changeSound;

    public bool TurnedOn { get; private set; }

    private AudioSource _source;
    private int _trackIndex;

    private Coroutine _changeTrackRoutine;

    private void Awake()
    {
        _source = GetComponent<AudioSource>();
    }

    public void NextTrack()
    {
        if (!TurnedOn) return;

        _trackIndex++;
        if (_trackIndex >= _clips.Length) _trackIndex = 0;
        else if (_trackIndex < 0) _trackIndex = _clips.Length - 1;

        if (_changeTrackRoutine != null) StopCoroutine(_changeTrackRoutine);
        _changeTrackRoutine = StartCoroutine(ChangeTrackRoutine());
    }

    public void PreviousTrack()
    {
        if (!TurnedOn) return;

        _trackIndex--;

        if (_trackIndex >= _clips.Length) _trackIndex = 0;
        else if (_trackIndex < 0) _trackIndex = _clips.Length - 1;

        if (_changeTrackRoutine != null) StopCoroutine(_changeTrackRoutine);
        _changeTrackRoutine = StartCoroutine(ChangeTrackRoutine());
    }

    public void HandleTurnOn()
    {
        TurnedOn = !TurnedOn;

        if (_changeTrackRoutine != null) StopCoroutine(_changeTrackRoutine);
        if (TurnedOn) _changeTrackRoutine = StartCoroutine(ChangeTrackRoutine());
        else _source.Stop();
    }

    private IEnumerator ChangeTrackRoutine()
    {
        _source.Stop();
        _source.PlayOneShot(_changeSound);
        yield return new WaitForSeconds(_changeSound.length);
        _source.clip = _clips[_trackIndex];
        _source.Play();
    }
}
