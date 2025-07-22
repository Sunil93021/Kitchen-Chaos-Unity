using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoveCounterVisuals : MonoBehaviour
{
    [SerializeField] private GameObject stoveBurnerGameObject;
    [SerializeField] private GameObject particleSystemGameObject;
    [SerializeField] private StoveCounter stoveCounter;



    private void Start()
    {
        stoveCounter.OnStateChange += StoveCounter_OnStateChange;
    }

    private void StoveCounter_OnStateChange(object sender, StoveCounter.OnStateChangeEventArgs e)
    {
        bool showVisual = (e.state == StoveCounter.State.Fring || e.state == StoveCounter.State.Fried);
        stoveBurnerGameObject.SetActive(showVisual);
        particleSystemGameObject.SetActive(showVisual);
    }

    
}
