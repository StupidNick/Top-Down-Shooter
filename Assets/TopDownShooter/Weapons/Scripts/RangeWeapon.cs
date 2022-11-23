using UnityEngine;



public class RangeWeapon : BaseWeaponComponent
{
    public float ReloadTime = 3;
    public Transform projectile;

    void Start()
    {
        
    }


    protected override void Shoot() 
    {
        var bullet = Instantiate(projectile, SpawnShotPoint.position, Quaternion.identity);

        Vector3 shootDirection = (PlayerController.GetMousePosition() - SpawnShotPoint.position).normalized;
        bullet.GetComponent<BaseProjectile>().Initialize(shootDirection, transform.rotation);
        base.Shoot();
    }
}
