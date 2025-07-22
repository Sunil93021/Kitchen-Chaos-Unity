using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClearCounter : BaseCounter
{
    [SerializeField] private KitchenObjectsSO kitchenObjectSO;
 
    public override void Interact(Player player)
    {
        if (!HasKitchenObject())
        {
            //dont have any kitchen object on counter
            if (player.HasKitchenObject())
            {
                //player holding an kitchen object
                player.GetKitchenObject().SetIKitchenObjectParent(this);
            }
            else
            {
                //player not carrying anything 
            }
        }
        else
        {
            //have an kitchen object on counter
            if (player.HasKitchenObject())
            {
                //player carring something
                PlateKitchenObject plateKitchenObject;
                if (player.GetKitchenObject().TryGetPlate(out plateKitchenObject))
                {
                    //player holding a plate
                    if (plateKitchenObject.TryAddIngredient(GetKitchenObject().GetKitchenObjectsSO()))
                    {
                        GetKitchenObject().DestroySelf();
                    }
                }
                else
                {
                    //its not a plate player holding something else
                    if(GetKitchenObject().TryGetPlate(out plateKitchenObject))
                    {
                        //Counter has a plate
                        if (plateKitchenObject.TryAddIngredient(player.GetKitchenObject().GetKitchenObjectsSO()))
                        {
                            player.GetKitchenObject().DestroySelf() ;
                        }
                    }
                }
            }
            else
            {
                GetKitchenObject().SetIKitchenObjectParent(player);
                //player not carrying kitchen object
            }
        }
    }

}
