using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu()]
public class KitchenObjectRecipeSO : ScriptableObject
{

    public KitchenObjectsSO input;
    public KitchenObjectsSO output;
    public int cuttingProgressMax;
}
