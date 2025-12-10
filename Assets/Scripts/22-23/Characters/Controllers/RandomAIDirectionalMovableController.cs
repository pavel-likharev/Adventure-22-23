using UnityEngine;

public class RandomAIDirectionalMovableController : Controller
{
    private IDirectionalMovable _movable;
    private Vector3 _currentDirection;

    private float _timer;
    private float _timeToChangeDirection;

    public RandomAIDirectionalMovableController(IDirectionalMovable movable, float timeToChangeDirection)
    {
        _movable = movable;
        _timeToChangeDirection = timeToChangeDirection;

        _timer = _timeToChangeDirection;
    }

    protected override void UpdateLogic(float deltaTime)
    {
        _timer += deltaTime;

        if (_timer > _timeToChangeDirection)
        {
            _currentDirection = new Vector3(Random.Range(-1f, 1f), 0, Random.Range(-1f, 1f));
            _timer = 0;
        }

        _movable.SetMoveDirection(_currentDirection);
    }
}
