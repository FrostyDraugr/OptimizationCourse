using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    [SerializeField]
    private GameObject _playerObj;
    private GameObject _player;

    [SerializeField]
    private GameObject _enemySpawnerObj;
    private GameObject _enemySpawner;

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        SetUpLevel();
    }

    private void SetUpLevel()
    {
        _player = Instantiate(_playerObj, Vector2.zero, transform.rotation, null);
        _enemySpawner = Instantiate(_enemySpawnerObj, Vector2.zero, transform.rotation, null);
    }

    public Vector3 PlayerPos()
    {
        return _player.transform.position;
    }

    public void GameOver()
    {
        if(_player != null)
        {
            Destroy(_player);
        }
        if(_enemySpawner != null)
        {
            Destroy( _enemySpawner);
        }
        SetUpLevel();
    }
}
