using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class BulletShell : MonoBehaviour
{
    [SerializeField]
    private float _lifeTime = 10;
    [SerializeField]
    private float _mooveSpeed = 50;
    private Vector3 _shootDirection;
    private Rigidbody _rb;


    public void Initialize(Vector3 direction, Quaternion rotation)
    {
        _shootDirection = direction;
        transform.rotation = rotation;
        Destroy(gameObject, _lifeTime);
    }
    void Start()
    {
        
    }
}
