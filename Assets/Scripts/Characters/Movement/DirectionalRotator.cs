using UnityEngine;

public class DirectionalRotator
{
    private Transform _transform;
    private float _rotationSpeed;

    private Vector3 _ñurrentDirection;
    private Vector3 _lastValidDirection;

    public Quaternion CurrentRotation => _transform.rotation;

    public DirectionalRotator(Transform transform, float rotationSpeed)
    {
        _transform = transform;
        _rotationSpeed = rotationSpeed;
    }

    public void SetCurrentDirection(Vector3 direction) => _ñurrentDirection = direction;

    public void Update(float deltaTime)
    {
            _lastValidDirection = _ñurrentDirection.normalized;
        if (_ñurrentDirection.magnitude < 0.01f)
            return;

        Quaternion lookRotation = Quaternion.LookRotation(_lastValidDirection);
        float _step = _rotationSpeed * deltaTime;
        _transform.rotation = Quaternion.Slerp(_transform.rotation, lookRotation, _step);

    }
}
