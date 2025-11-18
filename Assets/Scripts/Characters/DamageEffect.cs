using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageEffect
{
    private int _damage;

    public DamageEffect(int damage)
    {
        _damage = damage;
    }

    public void Execute(Vector3 point, Collider collider)
    {
        if (collider.TryGetComponent(out IDamagable damagable))
        {
            damagable.TakeDamage(_damage);
        }
    }
}
