using System.Collections;
using UnityEngine;



public class EnergyRangeWeapon : RangeWeapon
{
    private EnergyComponent Energy;
    public FloatEvent OnHeatChanged;
    public float EnergyToFire, MaxHeatLevel, HeatingPerShot;

    protected float _heatLevel = 0;
    void Start()
    {
        OnHeatChanged?.Invoke(_heatLevel);
        base.Start();
        Energy = gameObject.GetComponentInParent<EnergyComponent>();
    }


    protected override bool CheckCanShoot()
    {
        if(isReloading || !canShooting || Time.time < nextShootTimer || _heatLevel >= MaxHeatLevel || Energy == null) return false;

        if (Energy.Energy >= EnergyToFire)
        {
            return true;
        }
        return false;
    }


    protected override void Shoot()
    {
        base.Shoot();
        Energy.RemoveEnergy(EnergyToFire);
        _heatLevel += HeatingPerShot;
        OnHeatChanged?.Invoke(_heatLevel);
    }


    protected override IEnumerator EndReload()
    {
        yield return new WaitForSeconds(ReloadTime);

        _heatLevel = 0;
        OnHeatChanged?.Invoke(_heatLevel);
        isReloading = false;
    }
}
