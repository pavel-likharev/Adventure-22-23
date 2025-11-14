using UnityEngine;
using UnityEngine.AI;

public class AgroAIDirectionalMovableController : Controller
{
    private const int MinCornersCountInPathToMove = 2;
    private const int StartCornerIndex = 0;
    private const int TargetCornerIndex = 1;
    private IDirectionalMovable _movable;
    private Transform _target;

    private float _agroRange;
    private float _minDistanceToTarget;

    private NavMeshQueryFilter _queryFilter;

    private float _idleTimer;
    private float _timeForIdle;

    private NavMeshPath _pathToTarger = new NavMeshPath();

    public AgroAIDirectionalMovableController(
        IDirectionalMovable movable, 
        Transform target, 
        float agroRange, 
        float minDistanceToTarget, 
        NavMeshQueryFilter queryFilter, 
        float timeForIdle)
    {
        _movable = movable;
        _target = target;
        _agroRange = agroRange;
        _minDistanceToTarget = minDistanceToTarget;
        _queryFilter = queryFilter;
        _timeForIdle = timeForIdle;
    }

    protected override void UpdateLogic(float deltaTime)
    {
        if (_idleTimer > 0)
            _idleTimer -= deltaTime;

        if (NavMeshUtils.TryGetPath(_movable.Position, _target.position, _queryFilter, _pathToTarger))
        {
            float distanceToTarget = NavMeshUtils.GetPathLength(_pathToTarger);

            if (IsTargetReached(distanceToTarget))
                _idleTimer = _timeForIdle;

            if (InAgroRange(distanceToTarget)
                && EnoughCornersInPath(_pathToTarger)
                && IdleTimerIsUp())
            {
                _movable.SetMoveDirection(_pathToTarger.corners[TargetCornerIndex] - _pathToTarger.corners[StartCornerIndex]);
                return;
            }
        }

        _movable.SetMoveDirection(Vector3.zero);
    }

    private bool IsTargetReached(float distanceToTarget) => distanceToTarget <= _minDistanceToTarget;

    private bool InAgroRange(float distanceToTarget) => distanceToTarget <= _agroRange;

    private bool EnoughCornersInPath(NavMeshPath path) => _pathToTarger.corners.Length >= MinCornersCountInPathToMove;

    private bool IdleTimerIsUp() => _idleTimer <= 0;
}
