using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IDamageable
{
    void Damage(float damageAmount, float aimAngle, GameObject sender);
    
}
