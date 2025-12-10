using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplosionEffect
{
    private DamageEffect _damageEffect;
    private float _radius;

    public ExplosionEffect(DamageEffect damageEffect, float radius)
    {
        _damageEffect = damageEffect;
        _radius = radius;
    }

    public void Execute(Vector3 point)
    {
        Collider[] targets = Physics.OverlapSphere(point, _radius);

        foreach (Collider target in targets)
        {
            _damageEffect.Execute(target.transform.position, target);
        }

    }
}
