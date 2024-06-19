using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Monster : Creature
{
    [SerializeField] private SpriteRenderer _spriteRenderer;
    [SerializeField] private int _killBounty;
    [SerializeField] private int _contactDamage;
    private readonly int HIT_KEY = Animator.StringToHash("Hit");

    protected override void Awake()
    {
        base.Awake();

    }

    protected override void Hit()
    {
        _animator.SetTrigger(HIT_KEY);
        
    }
    protected override void Death()
    {
        base.Death();
        var ses = FindObjectOfType<LevelSession>();
        ses.AddCoins(_killBounty);
        _spriteRenderer.sortingOrder = 0;
        _moveDirection=Vector2.zero;
        GetComponent<Collider2D>().enabled = false;
        GetComponent<MonstersAI>().enabled = false;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            var h = collision.gameObject.GetComponent<ITakeDamage>();
            h?.TakeDamage(_contactDamage);

        }
    }
}
