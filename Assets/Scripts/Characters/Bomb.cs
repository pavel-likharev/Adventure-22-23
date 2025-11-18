using UnityEngine;

public class Bomb : MonoBehaviour
{
    private DamageEffect _damageEffect;
    private ExplosionEffect _explosionEffect;

    [SerializeField] private int _damageValue;
    [SerializeField] private float _radius;
    [SerializeField] private float _timeToExplotion;
    [SerializeField] private MeshRenderer _bombView;
    private float _timer;

    private bool _isLaunch;

    private IDamagable _damagableTarget;

    private void Awake()
    {
        _damageEffect = new(_damageValue);
        _explosionEffect = new(_damageEffect, _radius);
    }

    private void Update()
    {
        if (_isLaunch)
        {
            _timer -= Time.deltaTime;

            if (_timer < 0)
            {
                _explosionEffect.Execute(transform.position);
                Destroy(gameObject);
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out IDamagable damagable))
        {
            _bombView.material.color = Color.red;
            _timer = _timeToExplotion;        
            _isLaunch = true;
            GetComponent<Collider>().enabled = false;
        }
    }
}
