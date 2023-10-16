using UnityEngine;


public interface IEnemy
{
    void Damage(float damage);

    void Spawn(Vector2 transform);

}