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
            // enemy.OnDeath += ReturnEnemy;
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
        
        // enemy.OnDeath -= ReturnEnemy;
        enemy.OnDeath += ReturnEnemy;
        
        return enemy;
    }

    // private Enemy CreateNewEnemy()
    // {
    //     Enemy enemy = Instantiate(_enemyPrefab, transform);
    //     
    //     enemy.gameObject.SetActive(false);
    //     enemy.OnDeath += ReturnEnemy;
    //     _pool.Enqueue(enemy);
    //     
    //     return enemy;
    // }

    private void ReturnEnemy(Enemy enemy)
    {
        enemy.OnDeath -= ReturnEnemy;
        enemy.Deactivate();
        _pool.Enqueue(enemy);
    }
}