using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlateIconUI : MonoBehaviour
{
    [SerializeField] private PlateKitchenObject plateKitchenObject;
    [SerializeField] private Transform iconTemplete;
    
    private void Start()
    {
        plateKitchenObject.OnIngredientAdded += PlateKitchenObject_OnIngredientAdded;
        iconTemplete.gameObject.SetActive(false);
    }

    private void PlateKitchenObject_OnIngredientAdded(object sender, PlateKitchenObject.OnIngredientAddedEventArgs e)
    {
        UpdateVisual();
    }

    private void UpdateVisual()
    {
        foreach (Transform child in transform)
        {
            if(child == iconTemplete) continue;
            Destroy(child.gameObject);

        }
        foreach (KitchenObjectsSO kitchenObjectSO in plateKitchenObject.GetKitchenObjectSOList())
        {
            Transform iconTransfrom = Instantiate(iconTemplete, transform);
            iconTransfrom.gameObject.SetActive(true);
            iconTransfrom.GetComponent<PlateIconSingleUI>().SetKicthenObjectSO(kitchenObjectSO);
            
        }
    }
}
