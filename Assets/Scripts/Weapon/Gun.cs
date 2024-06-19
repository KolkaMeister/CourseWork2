using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : Weapon
{
    [SerializeField] protected Transform _projSpawn;
    [SerializeField] protected Projectile _projectile;
    [SerializeField] protected float _force;
    [SerializeField] protected float _shotDelay;
    [SerializeField] protected float _destroyProjTime;
    [SerializeField] private Cooldown _cooldown;
    public override void Attack()
    {
        if (!_cooldown.IsReady)
            return;
        CalculateDirection();
        _cooldown.Reset();
    }
    protected virtual void CalculateDirection()
    {
        var dir =_projSpawn.transform.position - _grip.transform.position;
        var deg =Mathf.Rad2Deg*MathF.Atan2(dir.y, dir.x);
        SpawnProjectile(Quaternion.Euler(0,0,deg));

    }
    protected void SpawnProjectile(Quaternion quatEu)
    {
        var pro = Instantiate<Projectile>(_projectile, _projSpawn.position, quatEu);
        pro.Launch(_force);
        Destroy(pro.gameObject,_destroyProjTime);
    }
}
[Serializable]
public class Cooldown
{
    [SerializeField] private float _cooldownTime;

    private float _upTime;
    public bool IsReady => Time.time > _upTime;
    public void Reset()
    {
        _upTime = Time.time+_cooldownTime;
    }
}