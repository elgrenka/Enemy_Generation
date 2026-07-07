using UnityEngine;
using System.Collections.Generic;

public class EnemyPool : MonoBehaviour
{
    [SerializeField] private Enemy _enemyPrefab;
    [SerializeField] private int _initialPoolSize = 10;

    private Queue<Enemy> _pool = new();

    private void Awake()
    {
        for (int i = 0; i < _initialPoolSize; i++)
        {
            Enemy enemy = Instantiate(_enemyPrefab, transform);

            enemy.gameObject.SetActive(false);
            _pool.Enqueue(enemy);
        }
    }

    public Enemy GetEnemy()
    {
        Enemy enemy;

        if (_pool.Count == 0)
        {
            enemy = Instantiate(_enemyPrefab, transform);
            enemy.gameObject.SetActive(false);
        }
        else
        {
            enemy = _pool.Dequeue();
        }

        enemy.Death += ReturnEnemy;

        return enemy;
    }

    private void ReturnEnemy(Enemy enemy)
    {
        enemy.Death -= ReturnEnemy;
        enemy.Deactivate();
        _pool.Enqueue(enemy);
    }
}