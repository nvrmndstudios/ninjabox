using System.Collections.Generic;
using UnityEngine;

public class SpawnerDifficultyController : MonoBehaviour
{
    [Header("Link every spawner in scene")]
    [SerializeField] private List<ItemSpawner> _spawners;

    [Header("Score thresholds per spawner")]
    [Tooltip("At score S, ActiveCount = S / scorePerSpawner + 1")]
    [SerializeField] private int scorePerSpawner = 10;

    [Header("Interval shrink")]
    [SerializeField] private AnimationCurve intervalScale =
        AnimationCurve.Linear(0, 1, 1, 0.4f); // From full to tight interval

    [SerializeField] private float hardestMinInterval = 0.8f;
    [SerializeField] private float hardestMaxInterval = 2.0f;

    private List<int> _activeIndices = new();

    public void OnScoreChanged(int score)
    {
        int wantedActiveCount = Mathf.Clamp(score / scorePerSpawner + 1, 1, _spawners.Count);

        // Pick random spawners for this difficulty
        List<int> allIndices = new List<int>();
        for (int i = 0; i < _spawners.Count; i++) allIndices.Add(i);
        Shuffle(allIndices);

        _activeIndices = allIndices.GetRange(0, wantedActiveCount);

        for (int i = 0; i < _spawners.Count; i++)
        {
            bool isActive = _activeIndices.Contains(i);
            _spawners[i].EnableSpawning(isActive);

            if (isActive)
            {
                float t = Mathf.InverseLerp(1, _spawners.Count, wantedActiveCount);
                float scale = intervalScale.Evaluate(t);
                float min = Mathf.Lerp(3f, hardestMinInterval, scale);
                float max = Mathf.Lerp(10f, hardestMaxInterval, scale);

                _spawners[i].SetIntervalRange(min, max);
            }
        }
    }

    private void Shuffle(List<int> list)
    {
        for (int i = list.Count - 1; i > 0; i--)
        {
            int j = Random.Range(0, i + 1);
            (list[i], list[j]) = (list[j], list[i]);
        }
    }
}