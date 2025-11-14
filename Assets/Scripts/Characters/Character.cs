using UnityEngine;
using UnityEngine.AI;

public class Character : MonoBehaviour, IDirectionalMovable, IDirectionalRotatable
{
    private DirectionalMover _mover;
    private DirectionalRotator _rotator;

    [SerializeField] private float _moveSpeed;
    [SerializeField] private float _rotationSpeed;

    public Vector3 CurrentVelocity => _mover.CurrentVelocity;
    public Quaternion CurrentRotation => _rotator.CurrentRotation;

    public Vector3 Position => transform.position;

    //private NavMeshPath _pathToTarget;

    private void Awake()
    {
        _mover = new(GetComponent<CharacterController>(), _moveSpeed);
        _rotator = new(transform, _rotationSpeed);

      //  _pathToTarget = new NavMeshPath();
    }

    private void Update()
    {
        _mover.Update(Time.deltaTime);
        _rotator.Update(Time.deltaTime);
    }

    public void SetMoveDirection(Vector3 inputDirection) => _mover.SetCurrentDirection(inputDirection);

    public void SetRotationDirection(Vector3 inputDirection) => _rotator.SetCurrentDirection(inputDirection);


}
