using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CuttingCounter : BaseCounter,IHasProgress
{
    [SerializeField] private KitchenObjectRecipeSO[] kitchenObjectRecipeSOArray;


    private int cuttingProgress;

    public static event EventHandler OnAnyCut;
    public event EventHandler Cut;
    public event EventHandler<IHasProgress.OnProgressChangedEventArgs> OnProgressChanged;

    new public static void ResetStaticData()
    {
        OnAnyCut = null;
    }

    public override void Interact(Player player)
    {
        if (!HasKitchenObject())
        {
            //dont have any kitchen object on counter
            if (player.HasKitchenObject() && HasRecipeWithInput(player.GetKitchenObject().GetKitchenObjectsSO()))
            {
                //player holding an kitchen object
                player.GetKitchenObject().SetIKitchenObjectParent(this);
                cuttingProgress = 0;
                KitchenObjectRecipeSO cuttingRecipe = GetCuttingRecipeSOWithInput(GetKitchenObject().GetKitchenObjectsSO());
                float progress = (float)cuttingProgress/cuttingRecipe.cuttingProgressMax;
                OnProgressChanged?.Invoke(this, new IHasProgress.OnProgressChangedEventArgs
                { 
                    progressNormalized = progress 
                });
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
                //player carring kitchen object
                if(player.GetKitchenObject().TryGetPlate(out PlateKitchenObject plateKitchenObject))
                {
                    //player holding plate
                    if (plateKitchenObject.TryAddIngredient(GetKitchenObject().GetKitchenObjectsSO()))
                    {
                        GetKitchenObject().DestroySelf();
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

    public override void InteractAlternate(Player player)
    {
        if (HasKitchenObject() && HasRecipeWithInput(GetKitchenObject().GetKitchenObjectsSO()))
        {  
            cuttingProgress += 1;

            Cut?.Invoke(this,EventArgs.Empty);
            OnAnyCut?.Invoke(this,EventArgs.Empty);
            KitchenObjectRecipeSO cuttingRecipe = GetCuttingRecipeSOWithInput(GetKitchenObject().GetKitchenObjectsSO());
            float progress = (float)cuttingProgress / cuttingRecipe.cuttingProgressMax;

            OnProgressChanged?.Invoke(this, new IHasProgress.OnProgressChangedEventArgs
            {
                progressNormalized = progress
            });
            if (cuttingProgress >= cuttingRecipe.cuttingProgressMax )
            {


                KitchenObjectsSO cuttingRecipeOutput = GetOutputForInput(GetKitchenObject().GetKitchenObjectsSO());
                GetKitchenObject().DestroySelf();
                KitchenObject.SpawnKitchenObject(cuttingRecipeOutput, this);
            }
        }
    }
    public bool HasRecipeWithInput(KitchenObjectsSO inputKitchenObjectSO)
    {
        return GetCuttingRecipeSOWithInput(inputKitchenObjectSO) != null;
    }
    public KitchenObjectsSO GetOutputForInput(KitchenObjectsSO inputKitchenObjectSO)
    {
        KitchenObjectRecipeSO cuttingRecipe = GetCuttingRecipeSOWithInput(inputKitchenObjectSO);
        if(cuttingRecipe != null)
        {
            return cuttingRecipe.output;
        }
        return null;
    }

    public KitchenObjectRecipeSO GetCuttingRecipeSOWithInput(KitchenObjectsSO inputKitchenObjectSO)
    {
        foreach (KitchenObjectRecipeSO cuttingRecipeSO in kitchenObjectRecipeSOArray)
        {
            if (cuttingRecipeSO.input == inputKitchenObjectSO)
            {
                return cuttingRecipeSO;
            }
        }
        return null;
    }
}
