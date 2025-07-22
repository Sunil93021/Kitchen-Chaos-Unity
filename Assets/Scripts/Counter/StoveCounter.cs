using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;


public class StoveCounter : BaseCounter,IHasProgress
{
    [SerializeField] private FringRecipeSO[] fringRecipeSOArray;
    [SerializeField] private BurningRecipeSO[] burningRecipeSOArray;

    public event EventHandler<IHasProgress.OnProgressChangedEventArgs> OnProgressChanged;
    public event EventHandler<OnStateChangeEventArgs> OnStateChange;
    public class OnStateChangeEventArgs : EventArgs {
        public State state;
    }


    public enum State
    {
        Idle,
        Fring,
        Fried,
        Burned,
    }

    private float fringTime;
    private float burningTime;
    private FringRecipeSO fringRecipeSO;
    private BurningRecipeSO burningRecipeSO;
    private State state;


    private void Start()
    {
        state = State.Idle;
    }

    private void Update()
    {
        if (HasKitchenObject())
        {

            switch (state)
            {
                case State.Idle:
                    break;
                case State.Fring:
                    fringTime += Time.deltaTime;
                    OnProgressChanged?.Invoke(this, new IHasProgress.OnProgressChangedEventArgs
                    {
                        progressNormalized = fringTime / fringRecipeSO.fryingTimerMax
                    });
                    if (fringTime >= fringRecipeSO.fryingTimerMax)
                    {
                        fringTime = 0f;

                        GetKitchenObject().DestroySelf();
                        KitchenObject.SpawnKitchenObject(fringRecipeSO.output, this);

                        state = State.Fried;
                        OnStateChange?.Invoke(this, new OnStateChangeEventArgs
                        {
                            state = state
                        });
                        
                        burningTime = 0f;
                        burningRecipeSO = GetBurningRecipeSOWithInput(GetKitchenObject().GetKitchenObjectsSO());
                    }
                    break;
                case State.Fried:
                    burningTime += Time.deltaTime;
                    OnProgressChanged?.Invoke(this, new IHasProgress.OnProgressChangedEventArgs
                    {
                        progressNormalized = burningTime/ burningRecipeSO.burningTimerMax
                    });
                    if (burningTime >= burningRecipeSO.burningTimerMax)
                    {
                        burningTime = 0f;
                        

                        GetKitchenObject().DestroySelf();
                        KitchenObject.SpawnKitchenObject(burningRecipeSO.output, this);

                        state = State.Burned;
                        OnStateChange?.Invoke(this, new OnStateChangeEventArgs
                        {
                            state = state
                        });

                        OnProgressChanged?.Invoke(this, new IHasProgress.OnProgressChangedEventArgs
                        {
                            progressNormalized = 0f
                        });

                    }
                    break;
                case State.Burned:
                    break;

            }
          
        }
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

                fringTime = 0f;
                fringRecipeSO = GetFringRecipeSOWithInput(GetKitchenObject().GetKitchenObjectsSO());


                OnProgressChanged?.Invoke(this, new IHasProgress.OnProgressChangedEventArgs
                {
                    progressNormalized = 0f
                });
                state = State.Fring;
                OnStateChange?.Invoke(this, new OnStateChangeEventArgs
                {
                    state = state
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
                if (player.GetKitchenObject().TryGetPlate(out PlateKitchenObject plateKitchenObject))
                {
                    //player holding plate
                    if (plateKitchenObject.TryAddIngredient(GetKitchenObject().GetKitchenObjectsSO()))
                    {
                        GetKitchenObject().DestroySelf();
                        state = State.Idle;
                        OnStateChange?.Invoke(this, new OnStateChangeEventArgs
                        {
                            state = state
                        });
                        OnProgressChanged?.Invoke(this, new IHasProgress.OnProgressChangedEventArgs
                        {
                            progressNormalized = 0f
                        });
                    }
                }
            }
            else
            {
                GetKitchenObject().SetIKitchenObjectParent(player);
                state = State.Idle;
                OnStateChange?.Invoke(this, new OnStateChangeEventArgs
                {
                    state = state
                });
                OnProgressChanged?.Invoke(this, new IHasProgress.OnProgressChangedEventArgs
                {
                    progressNormalized = 0f
                });
                //player not carrying kitchen object
            }
        }
    }

    public bool HasRecipeWithInput(KitchenObjectsSO inputKitchenObjectSO)
    {
        return GetFringRecipeSOWithInput(inputKitchenObjectSO) != null;
    }
    public KitchenObjectsSO GetOutputForInput(KitchenObjectsSO inputKitchenObjectSO)
    {
        FringRecipeSO fringRecipe = GetFringRecipeSOWithInput(inputKitchenObjectSO);
        if (fringRecipe != null)
        {
            return fringRecipe.output;
        }
        return null;
    }

    public FringRecipeSO GetFringRecipeSOWithInput(KitchenObjectsSO inputKitchenObjectSO)
    {
        foreach (FringRecipeSO fringRecipeSO in fringRecipeSOArray)
        {
            if (fringRecipeSO.input == inputKitchenObjectSO)
            {
                return fringRecipeSO;
            }
        }
        return null;
    }
    public BurningRecipeSO GetBurningRecipeSOWithInput(KitchenObjectsSO inputKitchenObjectSO)
    {
        foreach (BurningRecipeSO burningRecipeSO in burningRecipeSOArray)
        {
            if (burningRecipeSO.input == inputKitchenObjectSO)
            {
                return burningRecipeSO;
            }
        }
        return null;
    }
    public bool IsFried()
    {
        return state == State.Fried;
    }

}
