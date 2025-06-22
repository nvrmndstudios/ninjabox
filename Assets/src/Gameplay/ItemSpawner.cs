using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class ItemSpawner : MonoBehaviour
{
    [SerializeField] private List<GameObject> _itemPrefabs;

    [Header("Base interval range (seconds)")]
    [SerializeField] private float _minInterval = 3f;
    [SerializeField] private float _maxInterval = 10f;

    private Vector3 _startPos;
    private float _nextSpawnTime;
    private bool  _spawningEnabled = false;

    private const float FINAL_Y = 20f;

    void Awake()
    {
        _startPos      = transform.position;
        _nextSpawnTime = Time.time + GetNextInterval();
    }

    void Update()
    {
        if (!_spawningEnabled) return;               // <-- NEW GUARD
        if (Time.time < _nextSpawnTime) return;

        _nextSpawnTime = Time.time + GetNextInterval();

        var prefab = _itemPrefabs[Random.Range(0, _itemPrefabs.Count)];
        var obj    = Instantiate(prefab, transform, true);

        obj.transform.position               = _startPos;
        obj.GetComponent<Item>().targetPosition =
            new Vector3(_startPos.x, _startPos.y - FINAL_Y, _startPos.z);
    }

    /* ---------- helpers ---------- */

    float GetNextInterval() => Random.Range(_minInterval, _maxInterval);

    public void EnableSpawning(bool value) => _spawningEnabled = value;

    public void SetIntervalRange(float min, float max)
    {
        _minInterval = Mathf.Max(0.1f, min);
        _maxInterval = Mathf.Max(_minInterval, max);
    }
}