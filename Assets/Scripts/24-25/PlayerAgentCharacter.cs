using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class PlayerAgentCharacter : MonoBehaviour, IDirectionalMovable, IDirectionalRotatable, IDamagable
{
    private NavMeshAgent _agent;

    private AgentMover _mover;
    private DirectionalRotator _rotator;
    private AgentJumper _jumper;

    private Health _health;
    private HealSpawner _healSpawner;


    [SerializeField] private float _moveSpeed;
    [SerializeField] private float _rotationSpeed;
    [SerializeField] private int _healthValue;
    [SerializeField] private AnimationCurve _jumpCurve;

    [SerializeField] private GameObject _healPrefab;
    [SerializeField] private float _spawnHealDistance;
    [SerializeField] private float _spawnTime;
    [SerializeField] private int _healValue;

    public Vector3 CurrentVelocity => _mover.CurrentVelocity;

    public float CurrentVelocity2 => CurrentVelocity.magnitude;

    public Quaternion CurrentRotation => _rotator.CurrentRotation;

    public Vector3 Position => transform.position;

    public bool InJumpProcess => _jumper.InProcess;

    public float CurrentValue => _health.CurrentValue;

    private Coroutine _spawnHealProcess;

    //private NavMeshPath _pathToTarget;

    private void Awake()
    {
        _agent = GetComponent<NavMeshAgent>();
        _agent.updateRotation = false;


        _mover = new AgentMover(GetComponent<NavMeshAgent>(), _moveSpeed);
        _rotator = new(transform, _rotationSpeed);
        _jumper = new(_agent, _moveSpeed, this, _jumpCurve);

        _health = new(_healthValue);
        _healSpawner = new(_healPrefab, transform, new Timer(this), _spawnTime, this, _healValue);
        _healSpawner.Enable();
    }

    private void Update()
    {
        //_mover.Update(Time.deltaTime);
        _rotator.Update(Time.deltaTime);
    }

    public void Heal(int value) => _health.AddValue(value);

    public void SwitchWorkStatusHealSpawner() => _healSpawner.SwitchWorkStatus();

    public void StopMove() => _mover.Stop();

    public void ResumeMove() => _mover.Resume();

    public void SetDestination(Vector3 destination) => _mover.SetDestination(destination);
    //public void SetMoveDirection(Vector3 inputDirection) => _mover.SetDestination(inputDirection);

    public void SetRotationDirection(Vector3 inputDirection) => _rotator.SetCurrentDirection(inputDirection);

    public void TakeDamage(int damage) => _health.TakeDamage(damage);

    public bool TryGetPath(Vector3 targetPosition, NavMeshPath pathToTarget)
        => NavMeshUtils.TryGetPath(_agent, targetPosition, pathToTarget);

    public bool IsOnNavMeshLink(out OffMeshLinkData linkData)
    {
        if (_agent.isOnOffMeshLink)
        {
            linkData = _agent.currentOffMeshLinkData;
            return true;
        }

        linkData = default(OffMeshLinkData);
        return false;
    }

    public void Jump(OffMeshLinkData linkData) => _jumper.Jump(linkData);

    public void SetMoveDirection(Vector3 direction)
    {
        throw new System.NotImplementedException();
    }
}
