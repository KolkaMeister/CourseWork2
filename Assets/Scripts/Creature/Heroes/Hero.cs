using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hero : Creature
{
    [SerializeField] private GameObject _hand;
    private Weapon _weapon;

    public GameObject Hand => _hand;
    private readonly int MOVEMENT_KEY = Animator.StringToHash("Movement");
    private void Start()
    {
        TestInit();
    }
    protected override void Death()
    {
        base.Death();
        var ses = FindObjectOfType<LevelSession>();
        ses.PlayerDeath();
    }
    private void TestInit()
    {
       _weapon = GetComponentInChildren<Weapon>();
        SetWeapon( _weapon );
    }
    private void RotateHand()
    {
      var dir = _aimPoint - (Vector2)_hand.transform.position;
        _hand.transform.rotation = Quaternion.Euler(0,0,Mathf.Rad2Deg*Mathf.Atan(dir.y/dir.x));
    }
    public override void Attack()
    {
        _weapon.Attack();
    }

    public void SetWeapon(Weapon wep)
    {
        wep.transform.SetParent(_hand.transform);
        wep.transform.localPosition = Vector3.zero;
        wep.transform.localPosition = -wep.Grip.transform.localPosition;
    }
    protected override void FixedUpdate()
    {
        base.FixedUpdate();
        _animator.SetFloat(MOVEMENT_KEY,_rb.velocity.magnitude);
        RotateHand();
    }
}
