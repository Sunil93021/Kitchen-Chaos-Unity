using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoveCounterSound : MonoBehaviour
{
    private AudioSource audioSource;

    [SerializeField] private StoveCounter stoveCounter;
    private float warningSoundTimer;
    private bool playWarningSound;
    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
        stoveCounter.OnStateChange += StoveCounter_OnStateChange;
        stoveCounter.OnProgressChanged += StoveCounter_OnProgressChanged;
    }
    private void Update()
    {
        if (playWarningSound)
        {
            warningSoundTimer -= Time.deltaTime;
            if (warningSoundTimer <= 0f)
            {
                float warningSoundTimerMax = .2f;
                warningSoundTimer = warningSoundTimerMax;
                SoundManager.Instance.PlayWarningSound(stoveCounter.transform.position);
            }
        }
    }
    private void StoveCounter_OnProgressChanged(object sender, IHasProgress.OnProgressChangedEventArgs e)
    {
        float burnShowProgressAmount = .5f;
        playWarningSound = stoveCounter.IsFried() && e.progressNormalized > burnShowProgressAmount;
        
    }

    private void StoveCounter_OnStateChange(object sender, StoveCounter.OnStateChangeEventArgs e)
    {
        bool audioPlay = e.state == StoveCounter.State.Fring || e.state == StoveCounter.State.Fried;
        if (audioPlay)
        {
            audioSource.Play();
        }
        else
        {
            audioSource.Pause();
        }
    }
}
