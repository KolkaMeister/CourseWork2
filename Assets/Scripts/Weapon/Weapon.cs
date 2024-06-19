using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Weapon : MonoBehaviour
{
    [SerializeField]protected Transform _grip;
    public Transform Grip => _grip;
    public abstract void Attack();
}
