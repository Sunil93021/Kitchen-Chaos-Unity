using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KitchenObject : MonoBehaviour
{
    [SerializeField] private KitchenObjectsSO kitchenObjectsSO;
    private IKitchenObjectParent iKitchenObjectParent;

    public KitchenObjectsSO GetKitchenObjectsSO()
    {
        return kitchenObjectsSO;
    }

    public void SetIKitchenObjectParent(IKitchenObjectParent iKitchenObjectParent)
    {
        if (this.iKitchenObjectParent != null)
        {
            this.iKitchenObjectParent.ClearKitchenObject();

        }
        this.iKitchenObjectParent = iKitchenObjectParent;
        if (iKitchenObjectParent.HasKitchenObject())
        {
            Debug.LogError("IKitchenObjectParent already has Kitchen Object");
        }
        iKitchenObjectParent.SetKitchenObject(this);

        transform.parent = iKitchenObjectParent.GetKitchenObjectFollowTransform();
        transform.localPosition = Vector3.zero;
        

    }

    public IKitchenObjectParent GetIKitchenObjectParent()
    {
        return iKitchenObjectParent;
    }

    public void DestroySelf()
    {
        iKitchenObjectParent.ClearKitchenObject() ;
        Destroy(gameObject);
    }

    public static KitchenObject SpawnKitchenObject(KitchenObjectsSO kitchenObjectsSO, IKitchenObjectParent kitchenObjectParent)
    {
        Transform kitchenObjectTransform = Instantiate(kitchenObjectsSO.prefab);
        KitchenObject kitchenObject = kitchenObjectTransform.GetComponent<KitchenObject>();
        kitchenObject.SetIKitchenObjectParent(kitchenObjectParent);

        return kitchenObject;
    }

    public bool TryGetPlate(out PlateKitchenObject plateKitchenObject)
    {
        if(this is PlateKitchenObject)
        {
            plateKitchenObject = this as PlateKitchenObject;
            return true;
        }
        else
        {
            plateKitchenObject=null;
            return false;
        }
    }
}
