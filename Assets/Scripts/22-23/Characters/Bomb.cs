using UnityEngine;

public class Bomb : MonoBehaviour
{
    private DamageEffect _damageEffect;
    private ExplosionEffect _explosionEffect;
    private Timer _timer;

    [SerializeField] private int _damageValue;
    [SerializeField] private float _radius;
    [SerializeField] private float _timeToExplotion;
    [SerializeField] private MeshRenderer _bombView;
    //private float _timer;

    private bool _isLaunch;

    private IDamagable _damagableTarget;

    private void Awake()
    {
        _damageEffect = new(_damageValue);
        _explosionEffect = new(_damageEffect, _radius);

        _timer = new(this);
    }

    private void Update()
    {
        if (_isLaunch)
        {
            if (_timer.InProcess(out float elapsedTime) == false)
            {
                Debug.Log(elapsedTime);
                _explosionEffect.Execute(transform.position);
                Destroy(gameObject);
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out IDamagable damagable))
        {
            Debug.Log("in trigger bomb");
            _bombView.material.color = Color.red;
            //_timer = _timeToExplotion;        
            _timer.StartProcess(_timeToExplotion);
            _isLaunch = true;
            GetComponent<Collider>().enabled = false;
        }
    }
}
