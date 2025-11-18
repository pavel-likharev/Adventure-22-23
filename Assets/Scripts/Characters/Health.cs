using UnityEngine;

public class Health
{
    private int _currentValue;
    private int _maxValue;
    private float _criticalValue = 0.3f;

    private bool _isDead = false;
    private bool _isCriticalValue = false;

    public int CurrentValue => _currentValue;

    public Health(int value)
    {
        _maxValue = value;
        _currentValue = value;
    }

    public void TakeDamage(int damageValue)
    {
        if (damageValue < 0)
        {
            Debug.LogWarning("урон меньше 0");
            return;
        }

        _currentValue -= damageValue;

        if (IsCriticalHealth())
        {
            Debug.Log((float)_currentValue / _maxValue);
            Debug.Log("critical value");
            _isCriticalValue = true;
        }

        CheckDead();
    }

    private void CheckDead()
    {
        if (_currentValue <= 0)
        {
            _currentValue = 0;
            _isDead = true;
            Debug.Log("dead");
        }
    }

    private bool IsCriticalHealth()
    {
        return (float)_currentValue / _maxValue <= _criticalValue;
    }
}
