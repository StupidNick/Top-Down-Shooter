using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeamComponent : MonoBehaviour
{
    public enum Team
    {
        Robots,
        People
    }
    [SerializeField] private Team _team;

    public Team Teams
    {
        get
        {
            return _team;
        }
    }
}
