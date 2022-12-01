using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent(typeof(Rigidbody))]
public class BaseProjectile : MonoBehaviour
{
    [SerializeField]
    private float _lifeTime = 3;
    [SerializeField]
    private float _mooveSpeed = 500;
    [SerializeField]
    private int _numOfObstacle = 0;
    private float _damage;
    private Vector3 _shootDirection;


    private void Start() 
    {
    }
    
    public void Initialize(Vector3 shootDirection, Quaternion rotation, float damage)
    {
        _damage = damage;
        _shootDirection = shootDirection;
        transform.rotation = rotation;
        Destroy(gameObject, _lifeTime);
    }

    
    void Update()
    {
        transform.position += _shootDirection * _mooveSpeed * Time.deltaTime;
    }

    private void OnTriggerEnter(Collider collider) 
    {
        if (collider == null) return;
        if (collider.tag == "Impenetrable")
        {
            Destroy(gameObject);
        }
        var health = collider.GetComponent<HealthComponent>();

        if (health != null)
        {
            health.MakeDamage(_damage);
        }
        DecrementObstacleCounter();
    }


    private void DecrementObstacleCounter()
    {
        _numOfObstacle--;
        if (_numOfObstacle < 0)
        {
            Destroy(gameObject);
        }
    }
}
