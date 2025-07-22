using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlateCounter : BaseCounter
{

    [SerializeField] private KitchenObjectsSO plateKitchenObjectsSO;

    public event EventHandler OnPlateSpawned;
    public event EventHandler OnplateRemoved;

    private float plateSpawnTimer;
    private float plateSpawnTimerMax = 4f;
    private int plateSpawnAmount;
    private int plateSpawnAmountMax = 4;
    public void Update()
    {
        plateSpawnTimer += Time.deltaTime;
        if (plateSpawnTimer > plateSpawnTimerMax)
        {
            plateSpawnTimer = 0;
            if (GameManager.Instance.IsGamePlaying() && plateSpawnAmount < plateSpawnAmountMax)
            {
                plateSpawnAmount += 1;
                OnPlateSpawned?.Invoke(this, EventArgs.Empty);
            }
        }
    }

    public override void Interact(Player player)
    {
        if (!player.HasKitchenObject())
        {
            if(plateSpawnAmount > 0)
            {
                plateSpawnAmount--;
                KitchenObject.SpawnKitchenObject(plateKitchenObjectsSO, player);
                OnplateRemoved?.Invoke(this, EventArgs.Empty);
            }
        }
    }


}
