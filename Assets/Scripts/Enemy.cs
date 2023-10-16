using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour, IEnemy
{
    [SerializeField]
    int _maxHealth;
    float _health;

    [SerializeField]
    int _speed;

    [SerializeField]
    Rigidbody2D _rigidBody;

    // Start is called before the first frame update
    GameManager _gameManager;

    void Start()
    {
        _gameManager = GameManager.Instance;
        Spawn(Vector2.zero);
    }

    void FixedUpdate()
    {
        _rigidBody.AddForce( Time.fixedDeltaTime * _speed * (_gameManager.PlayerPos() - transform.position).normalized);
    }

    public void Damage(float damage)
    {
        _health -= damage;
        if (_health <= 0)
            Die();
    }

    public void Spawn(Vector2 transform)
    {
        _health = _maxHealth;
    }

    private void Die()
    { 
        Destroy(gameObject);
    }


}
