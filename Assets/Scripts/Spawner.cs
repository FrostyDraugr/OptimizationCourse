using System.Collections;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    [SerializeField]
    private GameObject _enemy;

    [SerializeField]
    private float _spawnTime;

    [SerializeField]
    private float _radius;

    private int _spawnNum = 1;

    void Start()
    {
        StartCoroutine(SpawnerCycle());
    }

    IEnumerator SpawnerCycle()
    {
        while (true)
        {
            yield return new WaitForSeconds(_spawnTime);
            for(int i = 0; i < _spawnNum; i++)
            {
            Instantiate(_enemy, RandomPosition(), transform.rotation, null);
            }
            _spawnNum++;
        }
    }

    Vector2 RandomPosition()
    {
        Vector2 pos = Vector2.zero;
        
        int angle = Random.Range(0, 360);
        
        pos.x = _radius * Mathf.Sin(angle * Mathf.Deg2Rad);
        pos.y = _radius * Mathf.Cos(angle * Mathf.Deg2Rad);

        return pos;
    }
}
