using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class ChickenSound : MonoBehaviour
{
    public AudioClip bawk;
    public AudioClip[] baks;

    public float bawkRate = 0.5f;
    public float bawkPitchRange = 0.1f;

    float startPitch;

    int bakCount = 0;
    AudioSource audioSource;

    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
        startPitch = audioSource.pitch;
    }

    public void StartBawk()
    {
        StartCoroutine(_Bawk());
    }

    IEnumerator _Bawk()
    {
        audioSource.pitch = startPitch + Random.Range(-bawkPitchRange, bawkPitchRange);
        audioSource.clip = (bakCount == 0) ? bawk : baks[Random.Range(0, baks.Length)];
        audioSource.Play();

        yield return new WaitForSeconds(bawkRate);

        bakCount++;
        if (bakCount == 4)
            bakCount = 0;

        StartCoroutine(_Bawk());
    }

    public void SingleBawk(float pitchMod)
    {
        audioSource.pitch = startPitch + pitchMod;
        audioSource.clip = bawk;
        audioSource.Play();
    }

    public void StopBawk()
    {
        StopAllCoroutines();
    }
}
