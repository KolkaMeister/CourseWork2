using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Creature : MonoBehaviour, ITakeDamage
{
    protected readonly int DEAD_KEY = Animator.StringToHash("Death");
    [SerializeField] protected Animator _animator;
    [SerializeField] private int _hp;
    [SerializeField] private float _speed;
    [SerializeField] protected Rigidbody2D _rb;
    protected Vector2 _aimPoint = Vector2.zero;
    protected Vector2 _moveDirection = Vector2.zero;
    protected Vector2 _aimDirection = Vector2.zero;

    public Vector2 SetAimPoint { set
        { 
            _aimPoint = value;
            SetAimDirection = _aimPoint - (Vector2)transform.position;
        } }
    public Vector2 SetAimDirection { set { _aimDirection = value.normalized; } }
    public Vector2 SetMoveDirection { set { _moveDirection = value.normalized; } }
    
    protected Action<int, int> _onHealthChanged;

    public event Action<int, int> OnHealthChanged
    {
        add
        {
            _onHealthChanged += value;
        }

        remove
        {
            _onHealthChanged -= value;
        }
    }
    protected virtual void Awake()
    {
        OnHealthChanged += TookDamage;
    }
    private void CalculateScale()
    {
        if (_aimDirection.x>0)
            transform.localScale = new Vector3(1,1,1);
        else
            transform.localScale = new Vector3(-1, 1, 1);
    }
    protected virtual void TookDamage(int newv, int old)
    {
        if (newv <= 0)
            Death();
        else
            Hit();
    }
    protected virtual void Hit()
    {

    }
    protected virtual void  Death()
    {
        _animator.SetTrigger(DEAD_KEY);
    }
    private void CalculateMovement()
    {
        _rb.velocity = _moveDirection*_speed;

    }
   public virtual void Attack() { }
    protected virtual void FixedUpdate()
    {
        CalculateMovement();
        CalculateScale();
       
    }

    public void TakeDamage(int dam)
    {
        var old = _hp;
        _hp = old - dam;
        _onHealthChanged?.Invoke(_hp, old);
    }
}
