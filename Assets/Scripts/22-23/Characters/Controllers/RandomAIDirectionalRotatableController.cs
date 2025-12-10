using UnityEngine;

public class RandomAIDirectionalRotatableController : Controller
{
    private IDirectionalRotatable _rotatable;
    private Vector3 _currentDirection;

    private float _timer;
    private float _timeToChangeDirection;

    public RandomAIDirectionalRotatableController(IDirectionalRotatable rotatable, float timeToChangeDirection)
    {
        _rotatable = rotatable;
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

        _rotatable.SetRotationDirection(_currentDirection);
    }
}
