using UnityEngine;

public class Character : MonoBehaviour, IDirectionalMovable, IDirectionalRotatable, IDamagable
{
    private DirectionalMover _mover;
    private DirectionalRotator _rotator;

    private Health _health;

    [SerializeField] private float _moveSpeed;
    [SerializeField] private float _rotationSpeed;
    [SerializeField] private int _healthValue;

    public Vector3 CurrentVelocity => _mover.CurrentVelocity;
    public float CurrentVelocity2 => _mover.CurrentVelocity2;
    public Quaternion CurrentRotation => _rotator.CurrentRotation;

    public Vector3 Position => transform.position;

    //private NavMeshPath _pathToTarget;

    private void Awake()
    {
        _mover = new(GetComponent<CharacterController>(), _moveSpeed);
        _rotator = new(transform, _rotationSpeed);

        _health = new(_healthValue);

      //  _pathToTarget = new NavMeshPath();
    }

    private void Update()
    {
        _mover.Update(Time.deltaTime);
        _rotator.Update(Time.deltaTime);
    }

    public void SetMoveDirection(Vector3 inputDirection) => _mover.SetCurrentDirection(inputDirection);

    public void SetRotationDirection(Vector3 inputDirection) => _rotator.SetCurrentDirection(inputDirection);

    public void TakeDamage(int damage) => _health.TakeDamage(damage);
}
