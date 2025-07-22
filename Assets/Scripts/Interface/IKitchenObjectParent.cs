using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IKitchenObjectParent 
{
    public Transform GetKitchenObjectFollowTransform();
    public void SetKitchenObject(KitchenObject kitchenObject);
    public void ClearKitchenObject();
    public KitchenObject GetKitchenObject();
    public bool HasKitchenObject();
}
