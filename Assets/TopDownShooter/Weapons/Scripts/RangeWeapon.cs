using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangeWeapon : BaseWeaponComponent
{
    public float reloadTime = 3;


    void Start()
    {
        
    }


    protected override void Shoot() 
    {
        Ray ray = new Ray(spawnShotPoint.position, spawnShotPoint.forward);
        RaycastHit hit;

        float shotDistance = 20;

        if(Physics.Raycast(ray, out hit, shotDistance))
        {
            shotDistance = hit.distance;
        }
        Debug.DrawRay(ray.origin, ray.direction * shotDistance, Color.red, 2);
        base.Shoot();
    }
}
