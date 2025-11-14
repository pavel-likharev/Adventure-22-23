using UnityEngine;

public class RandomAICharacterController : Controller
{
    private Character _character;
    private Vector3 _currentDirection;

    private float _timer;
    private float _timeToChangeDirection;

    public RandomAICharacterController(Character character, float timeToChangeDirection)
    {
        _character = character;
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

        _character.SetMoveDirection(_currentDirection);
        _character.SetRotationDirection(_currentDirection);
    }
}
