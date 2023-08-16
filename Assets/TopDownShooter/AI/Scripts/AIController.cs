using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;


[RequireComponent (typeof(NavMeshAgent), typeof(TeamComponent), typeof(AIVision))]
public class AIController : MonoBehaviour
{
    enum AIState
    {
        Patrol,
        Search,
        Attack
    }

    struct EnemyInfo
    {
        public TeamComponent _enemyTarget;
        public Vector3 _lastTargetPosition;
    }

    private Transform _movePositionTransform;
    private NavMeshAgent _navMeshAgent;
    [HideInInspector] public TeamComponent Team;
    private AIVision _vision;
    [SerializeField] private float _patrolRadius;
    [SerializeField] private float _reachableDistance;
    private Vector3 _patrolTargetPostion;
    private EnemyInfo _enemyInfo;
    private AIState _currentAIState;
    private HealthComponent _healthComponent;
    [SerializeField] private BaseWeaponComponent _weapon;
    private float _elapsed = 0.0f; // Update frequency
    private bool _canAttack = true;
    private bool _seeEnemy = false;
    [SerializeField] private float minX, maxX, minZ, maxZ = 0;


    private void Awake()
    {
        _navMeshAgent = GetComponent<NavMeshAgent>();
        _patrolTargetPostion = transform.position;
        _vision = GetComponent<AIVision>();
        _vision.SeeEnemy += OnEnemySee;
        Team = GetComponent<TeamComponent>();
        _healthComponent = GetComponent<HealthComponent>();

        _currentAIState = AIState.Patrol;
    }


    private void Update()
    {
        _elapsed += Time.deltaTime;
        if (_elapsed > 0.5f)
        {
            _elapsed -= 0.5f;
            TakeTask();
        }
    }


    private void OnEnemySee(TeamComponent target)
    {
        _seeEnemy = true;
        _enemyInfo._enemyTarget = target;
        _enemyInfo._lastTargetPosition = target.transform.position;
        _currentAIState = AIState.Attack;
    }


    private void TakeTask()
    {
        switch (_currentAIState)
        {
            case AIState.Patrol:
                if (CheckNPCAroundTarget())
                {
                    Patrol();
                }
                break;
            case AIState.Search:
                Search();
                break;
            case AIState.Attack:
                Attack();
                break;
            default:
                return;
        }
    }


    private void Search()
    {
        _patrolTargetPostion = _enemyInfo._lastTargetPosition;
        Debug.Log("Searching");
        if (!CheckNPCAroundTarget())
        {
            _navMeshAgent.isStopped = false;
            GoToPosition(_enemyInfo._lastTargetPosition);
            Debug.DrawRay(_patrolTargetPostion, transform.position.normalized, Color.blue, 5);
            return;
        }
        _currentAIState = AIState.Patrol;
        Patrol();
    }
    
    
    private void Patrol()
    {
        _patrolTargetPostion = GetRandomPoint();
        GoToPosition(_patrolTargetPostion);
    }


    private bool CheckNPCAroundTarget()
    {
        if (Vector3.Distance(_patrolTargetPostion, transform.position) <= _reachableDistance)
        {
            Debug.Log("Around target");
            return true;
        }
        return false;
    }


    private Vector3 GetRandomPoint()
    {
        float rx = Random.Range(minX, maxX);
        float rz = Random.Range(minZ, maxZ);
        return new Vector3(rx, 0, rz);
    }
    
    
    private Vector3 GetRandomPositionInRadius(Vector3 center, float radius)
    {
        Vector3 point = new Vector3(0, 0, 0);
        NavMeshPath path = new NavMeshPath();
        do
        {
            float distance = Random.Range(_reachableDistance, radius);
            Vector3 direction = Random.insideUnitSphere.normalized;
            point = direction * distance;
        }
        while(!NavMesh.CalculatePath(transform.position, point, NavMesh.AllAreas, path));

        return point;
    }


    private void GoToPosition(Vector3 targetPosition)
    {
        _navMeshAgent.destination = targetPosition;
    }
    
    
    private void StopMoving()
    {
        _patrolTargetPostion = transform.position;
        _navMeshAgent.isStopped = true;
    }


    private void Attack()
    {
        if (!_canAttack) return;
        if (!_seeEnemy)
        {
            _currentAIState = AIState.Search;
            return;
        }
        
        if (CheckCanShoot())
        {
            if (_weapon.GetAmmoInTheClip() <= 0)
            {
                _weapon.StartReload();
                return;
            }
            StopMoving();
            RotateToTarget();
            _weapon.ResetShootingCounter();
            _weapon.StartShooting();
        }
        RotateToTarget();
        _seeEnemy = false;
    }


    private bool CheckCanShoot()
    {
        if (_enemyInfo._lastTargetPosition == null || _enemyInfo._enemyTarget == null) return false;
        
        RaycastHit hitInfo = new RaycastHit();
        Physics.Linecast(_weapon.SpawnShotPoint.transform.position, _enemyInfo._lastTargetPosition, out hitInfo);
        if (hitInfo.collider == null) return false;
        
        if (hitInfo.collider.gameObject == _enemyInfo._enemyTarget.gameObject)
        {
            return true;
        }
        return false;
    }
    
    
    private void RotateToTarget()
    {
        Vector3 direction = _enemyInfo._lastTargetPosition - transform.position;
        Quaternion rotation = Quaternion.LookRotation(direction);
        transform.rotation = rotation;
    }


    public void ReloadWeapon()
    {
        _canAttack = false;
        StartCoroutine(EndReloadWeapon());
    }


    protected virtual IEnumerator EndReloadWeapon()
    {
        yield return new WaitForSeconds(_weapon.ReloadTime);
        _canAttack = true;
    }


    public void OnDead()
    {
        GetComponent<Renderer>().enabled = false;
        gameObject.SetActive(false);
    }
}
