using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControlls : MonoBehaviour
{
    [SerializeField]
    private float _acceleration;
    private Rigidbody2D _rb;

    [SerializeField]
    private GameObject _bullet;

    // Start is called before the first frame update
    void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        SetRotation();

        if(Input.GetKey(KeyCode.W))
        {
            AddForce(1);
        }
    }

    private void SetRotation()
    {
        Vector3 mousePosition = Input.mousePosition;
        mousePosition = Camera.main.ScreenToWorldPoint(mousePosition);

        Quaternion rot = Quaternion.LookRotation(transform.position - mousePosition, Vector3.forward);
        transform.rotation = rot;
        transform.eulerAngles = new Vector3(0, 0, transform.eulerAngles.z);
    }

    private void Fire()
    {
        Instantiate(_bullet, transform);
    }

    private void AddForce(int dir)
    {
        _rb.AddForce(dir *_acceleration * Time.deltaTime * transform.up);
    }
}
