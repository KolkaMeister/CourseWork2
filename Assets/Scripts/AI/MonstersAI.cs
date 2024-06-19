using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class MonstersAI : MonoBehaviour
{
    [SerializeField] private Monster mons;
    [SerializeField] private float _gapDistance;
    private Hero target;

    private void Start()
    {
        target = FindObjectOfType<Hero>();
        
    }


    private void Update()
    {
        if (target != null) 
        {
            if ((transform.position - target.transform.position).magnitude > _gapDistance)
                mons.SetMoveDirection = target.transform.position - mons.transform.position;
            else
                mons.SetMoveDirection = Vector2.zero;
            mons.SetAimPoint=target.transform.position;
        }
    }
}
