using UnityEngine;

public class DirectionalRotator
{
    private Transform _transform;
    private float _rotationSpeed;

    private Vector3 _ñurrentDirection;

    public Quaternion CurrentRotation => _transform.rotation;

    public DirectionalRotator(Transform transform, float rotationSpeed)
    {
        _transform = transform;
        _rotationSpeed = rotationSpeed;
    }

    public void SetCurrentDirection(Vector3 direction) => _ñurrentDirection = direction;

    public void Update(float deltaTime)
    {
        if (_ñurrentDirection.magnitude < 0.05f)
            return;

        Quaternion lookRotation = Quaternion.LookRotation(_ñurrentDirection.normalized);
        float _step = _rotationSpeed * deltaTime;



        _transform.localRotation = Quaternion.Lerp(_transform.localRotation, lookRotation, _step);
    }
}
