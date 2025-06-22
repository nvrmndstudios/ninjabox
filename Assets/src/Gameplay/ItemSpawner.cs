using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class ItemSpawner : MonoBehaviour
{
    [SerializeField] private List<GameObject> _itemPrefabs;
    
    private Vector3 startingPos;

    private float _nextGenerationTime = 0;

    private const float finalY = 20;

    private float getGenerationInterval () {
        return Random.Range (3.0f, 10.0f);
    }

    private void Awake()
    {
        _nextGenerationTime = Time.time + getGenerationInterval();
        startingPos = this.transform.position;
    }

    void Update()
    {
        CheckAndGenerateItems ();
    }

    private void CheckAndGenerateItems ()
    {
        if (!(_nextGenerationTime <= Time.time)) return;
        
        _nextGenerationTime = Time.time + getGenerationInterval();
        GameObject itemObject = Instantiate (_itemPrefabs[Random.Range (0, _itemPrefabs.Count)], transform, true) as GameObject;
        itemObject.GetComponent<Item>().targetPosition = new Vector3 (startingPos.x, startingPos.y - finalY, startingPos.z);
        itemObject.transform.position = startingPos;
    }
}
