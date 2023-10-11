using UnityEngine;

public class Bullet : MonoBehaviour
{
    private Rigidbody2D _rb;

    [SerializeField]
    private float _acceleration;

    // Start is called before the first frame update
    void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        AddForce();
    }

    private void AddForce()
    {
        _rb.AddForce(_acceleration * Time.fixedDeltaTime * transform.up);
    }
}
