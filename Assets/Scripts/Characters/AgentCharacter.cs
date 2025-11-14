using UnityEngine;
using UnityEngine.AI;

public class AgentCharacter : MonoBehaviour, IDirectionalMovable, IDirectionalRotatable
{
    private NavMeshAgent _agent;

    private AgentMover _mover;
    private DirectionalRotator _rotator;

    [SerializeField] private Transform _target;

    [SerializeField] private float _rotationSpeed;
    [SerializeField] private float _moveSpeed;

    public Quaternion CurrentRotation => _rotator.CurrentRotation;

    public Vector3 Position => transform.position;

    public Vector3 CurrentVelocity => _mover.CurrentVelocity;

    private void Awake()
    {
        _agent = GetComponent<NavMeshAgent>();
        _agent.updateRotation = false;

        _rotator = new(transform, _rotationSpeed);
        _mover = new(_agent, _moveSpeed);
    }

    private void Update()
    {
        _rotator.Update(Time.deltaTime);
    }

    public void SetDestination(Vector3 destination) => _mover.SetDestination(destination);

    public void SetRotationDirection(Vector3 currentDirection) => _rotator.SetCurrentDirection(currentDirection);

    public void StopMove() => _mover.Stop();

    public void ResumeMove() => _mover.Resume();

    public bool TryGetPath(Vector3 targetPosition, NavMeshPath pathToTarget)
        => NavMeshUtils.TryGetPath(_agent, targetPosition, pathToTarget);

    public void SetMoveDirection(Vector3 direction)
    {
        throw new System.NotImplementedException();
    }
}
