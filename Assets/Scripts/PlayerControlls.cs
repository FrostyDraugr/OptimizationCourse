using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerControlls : MonoBehaviour
{
    [SerializeField]
    private float _acceleration;

    [SerializeField]
    private Transform _firePoint;

    [SerializeField]
    private Rigidbody2D _rb;

    [SerializeField]
    private GameObject _bullet;

    private GameManager _gameManager;
    private void Start()
    {
        _gameManager = GameManager.Instance;
    }

    void Update()
    {
        SetRotation();

        if(Input.anyKey)
        {
            if (Input.GetKey(KeyCode.W))
            {
                AddForce(1);
            }

            if(Input.GetKey(KeyCode.Mouse0))
            {
                Fire();
            }
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
        GameObject obj = Instantiate(_bullet, _firePoint.position, transform.rotation,null);
    }

    private void AddForce(int dir)
    {
        _rb.AddForce(dir *_acceleration * Time.deltaTime * transform.up);
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        IEnemy enemy = collision.gameObject.GetComponent<IEnemy>();
        if (enemy != null)
        {
            _gameManager.GameOver();
        }
    }
}
