using UnityEngine;
using System.Collections;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private EnemyPool _enemyPool;
    [SerializeField] private SpawnPoint[] _spawnPoints;
    [SerializeField] private float _spawnInterval = 2f;

    private Coroutine _spawnRoutine;

    private void Start()
    {
        // if (_enemyPool == null)
        //     _enemyPool = FindAnyObjectByType<EnemyPool>();
        
        _spawnRoutine = StartCoroutine(SpawnRoutine(_spawnInterval));
    }

    private void OnDisable()
    {
        if (_spawnRoutine is not null)
            StopCoroutine(_spawnRoutine);
    }

    private IEnumerator SpawnRoutine(float spawnInterval)
    {
        var wait = new WaitForSeconds(spawnInterval);
        
        while (enabled)
        {
            yield return wait;
            SpawnEnemy();
        }
    }

    private void SpawnEnemy()
    {
        if (_spawnPoints is null || _spawnPoints.Length == 0 || _enemyPool is null)
            return;

        SpawnPoint randomPoint = _spawnPoints[Random.Range(0, _spawnPoints.Length)];
        Enemy enemy = _enemyPool.GetEnemy();
        enemy.transform.position = randomPoint.transform.position;
        enemy.Initialize(randomPoint.direction);
    }
}