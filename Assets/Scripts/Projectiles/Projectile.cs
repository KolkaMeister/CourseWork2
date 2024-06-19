using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    [SerializeField] private Rigidbody2D _rb;
    [SerializeField] private int _damage;

    public void Launch(float forse)
    {
        _rb.AddForce(transform.right* forse);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Enemy")
        {
           var h= collision.gameObject.GetComponent<ITakeDamage>();
            h?.TakeDamage(_damage);
            Destroy(gameObject);
        }
    }
}
