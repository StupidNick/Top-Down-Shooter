using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;



[RequireComponent (typeof(AIController))]
public class AIVision : MonoBehaviour
{
    private TeamComponent[] _targets;
    private AIController AI;
    [SerializeField] private float FOVRadius;
    [SerializeField] private Transform _viewPoint;
    [SerializeField] private float FOV;

    public event Action<TeamComponent> SeeEnemy;


    private void Awake()
    {
        AI = GetComponent<AIController>();
    }

    // Start is called before the first frame update
    void Start()
    {
        _targets = FindObjectsOfType<TeamComponent>();
    }

    // Update is called once per frame
    void Update()
    {
        CheckTargetsInFov();
    }


    private void CheckTargetsInFov()
    {
        List<TeamComponent> targets = GetAllTargetsInRadius();

        foreach (TeamComponent target in targets)
        {
            if (CheckInFOV(target) && target.Teams != AI.Team.Teams && CheckObstacleInFrontOfTarget(target))
            {
                SeeEnemy?.Invoke(target);
            }
        }
    }


    private List<TeamComponent> GetAllTargetsInRadius()
    {
        List<TeamComponent> targetsInRadius = new List<TeamComponent>();
        foreach (TeamComponent target in _targets)
        {
            if (Vector3.Distance(target.transform.position, transform.position) < FOVRadius)
            {
                targetsInRadius.Add(target);
            }
        }
        return targetsInRadius;
    }


    private bool CheckInFOV(TeamComponent target)
    {
        Vector3 distance = target.transform.position - _viewPoint.position;
        
        if (Mathf.Abs(Vector3.SignedAngle(distance, _viewPoint.forward, Vector3.up)) > FOV/2) return false;
        return true;
    }
    
    
    private bool CheckObstacleInFrontOfTarget(TeamComponent target)
    {
        RaycastHit hitInfo = new RaycastHit();
        Physics.Linecast(target.transform.position, transform.position, out hitInfo);
        if (hitInfo.collider == null) return false;
        
        if (hitInfo.collider.gameObject == gameObject)
        {
            return true;
        }
        return false;
    }
}
