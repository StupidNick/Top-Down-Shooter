using UnityEngine;



public class RangeWeapon : BaseWeaponComponent
{
    public Transform projectile;



    protected override void Shoot() 
    {
        if (Owner == null) return;
        Transform bullet = Instantiate(projectile, SpawnShotPoint.position, Quaternion.identity);

        PlayerController ownewPlayer = Owner.GetComponent<PlayerController>();
        if (ownewPlayer != null)
        {
            Vector3 shootDirection = (PlayerController.GetMousePosition() - SpawnShotPoint.position).normalized;
            bullet.GetComponent<BaseProjectile>().Initialize(shootDirection, transform.rotation, Damage);
        }
        AIController ownerNPC = Owner.GetComponent<AIController>();
        if (ownerNPC != null)
        {
            Vector3 target = FindObjectOfType<PlayerController>().transform.position;
            Vector3 shootDirection = (target - SpawnShotPoint.position).normalized;
            bullet.GetComponent<BaseProjectile>().Initialize(shootDirection, transform.rotation, Damage);
        }
    
        
        base.Shoot();
    }
}
