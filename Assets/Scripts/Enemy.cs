using UnityEngine;
using System;

public class Enemy : MonoBehaviour
{
    [SerializeField] private float _speed = 2f;
    [SerializeField] private float _lifeTime = 5f;

    private Vector3 _moveDirection;
    private float _lifeTimer;

    public event Action<Enemy> OnDeath;

    private void Update()
    {
        transform.Translate(_moveDirection * (_speed * Time.deltaTime), Space.World);

        _lifeTimer -= Time.deltaTime;
        
        if (_lifeTimer <= 0f)
            Die();
    }
    
    public void Initialize(Vector3 direction)
    {
        if (direction == Vector3.zero)
            direction = Vector3.forward;
        
        _moveDirection = direction.normalized;
        _lifeTimer = _lifeTime;

        transform.rotation = Quaternion.LookRotation(_moveDirection);

        gameObject.SetActive(true);
    }

    public void Deactivate()
    {
        gameObject.SetActive(false);
    }

    private void Die()
    {
        OnDeath?.Invoke(this);
    }
}