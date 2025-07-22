using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlateCounterVisuals : MonoBehaviour
{
    [SerializeField] private Transform plateVisualPrefab;
    [SerializeField] private Transform counterTopPoint;
    [SerializeField] private PlateCounter plateCounter;

    private List<GameObject> plateVisualGameObjectList;

    private void Start()
    {
        plateVisualGameObjectList = new List<GameObject>();
        plateCounter.OnPlateSpawned += PlateCounter_OnPlateSpawned;
        plateCounter.OnplateRemoved += PlateCounter_OnplateRemoved;
    }

    private void PlateCounter_OnplateRemoved(object sender, System.EventArgs e)
    {
        if(plateVisualGameObjectList.Count > 0)
        {
            GameObject plateVisualGameObject = plateVisualGameObjectList[plateVisualGameObjectList.Count-1];

            plateVisualGameObjectList.Remove(plateVisualGameObject);
            Destroy(plateVisualGameObject);
        }
    }

    private void PlateCounter_OnPlateSpawned(object sender, System.EventArgs e)
    {
        Transform plateVisualTransform = Instantiate(plateVisualPrefab, counterTopPoint);

        float positionOffSetY = .1f;
        plateVisualTransform.localPosition = new Vector3(0, positionOffSetY * plateVisualGameObjectList.Count, 0);
        plateVisualGameObjectList.Add(plateVisualTransform.gameObject);

    }
}
