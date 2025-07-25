using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class BaseCounter : MonoBehaviour, IKitchenObjectParent
{
    [SerializeField] private Transform counterTopPoint;

    public static event EventHandler OnAnyObjectPlacedHere;
    private KitchenObject kitchenObject;

    public static void ResetStaticData()
    {
        OnAnyObjectPlacedHere = null;
    }
    public virtual void Interact(Player player)
    {
        Debug.LogError("BaseCounter.Interact();");
    }
    public virtual void InteractAlternate(Player player)
    {
        //Debug.LogError("BaseCounter.InteractAlternate();");
    }
    public Transform GetKitchenObjectFollowTransform()
    {
        return counterTopPoint;
    }

    public void SetKitchenObject(KitchenObject kitchenObject)
    {
        this.kitchenObject = kitchenObject;
        if (kitchenObject != null)
        {
            OnAnyObjectPlacedHere?.Invoke(this, EventArgs.Empty);
        }
    }
    public void ClearKitchenObject()
    {
        this.kitchenObject = null;
    }

    public KitchenObject GetKitchenObject() { 
        return kitchenObject; 
    }
    public bool HasKitchenObject()
    {
        return kitchenObject != null;
    }
}
