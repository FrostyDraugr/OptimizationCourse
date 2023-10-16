using Unity.VisualScripting;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField]
    private Rigidbody2D _rb;

    [SerializeField]
    private float _acceleration;

    [SerializeField]
    private float _lifeTime;

    // Start is called before the first frame update
    void Start()
    {
        Destroy(gameObject, _lifeTime);
        InitialSpeed();
    }

    private void InitialSpeed()
    {
        _rb.velocity = 10 * transform.up;
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        IEnemy enemy = collision.gameObject.GetComponent<IEnemy>();
        if (enemy != null)
        {
            enemy.Damage(1);
            Destroy(gameObject);
        }
    }

}
