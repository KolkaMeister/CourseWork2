using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShotGun : Gun
{
    [SerializeField] private int _projPerShot;
    [SerializeField] private float _sprayInDeg;
    protected override void CalculateDirection()
    {
        var dir = _projSpawn.transform.position - _grip.transform.position;
        var deg = Mathf.Rad2Deg * MathF.Atan2(dir.y, dir.x);
        var start = deg - _sprayInDeg;
        var step = (2 * _sprayInDeg) / _projPerShot;
        for (int i = 0; i < _projPerShot; i++)
        {
            SpawnProjectile(Quaternion.Euler(0,0,start + step * i));
        }
    }
}
