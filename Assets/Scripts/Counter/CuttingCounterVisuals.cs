using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CuttingCounterVisuals : MonoBehaviour
{
    [SerializeField] private CuttingCounter cuttingCounter;

    private Animator animator;
    private const string CUT = "Cut";

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    private void Start()
    {
        cuttingCounter.Cut += CuttingCounter_Cut;
    }

    private void CuttingCounter_Cut(object sender, EventArgs e)
    {
        animator.SetTrigger(CUT);
    }

   
}
