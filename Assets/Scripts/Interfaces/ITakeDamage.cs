using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ITakeDamage 
{
    public event Action<int,int> OnHealthChanged;
    public void TakeDamage(int dam);

}
