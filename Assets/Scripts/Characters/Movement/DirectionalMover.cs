using UnityEngine;

public class DirectionalMover
{
    private CharacterController _characterController;
    private float _moveSpeed;

    private Vector3 _currentDirection;

    public Vector3 CurrentVelocity { get; private set; }

    public DirectionalMover(CharacterController characterController, float moveSpeed)
    {
        _characterController = characterController;
        _moveSpeed = moveSpeed;
    }

    public void SetCurrentDirection(Vector3 direction) => _currentDirection = direction;

    public void Update(float deltaTime)
    {
        CurrentVelocity = _currentDirection.normalized * _moveSpeed * deltaTime;
        _characterController.Move(CurrentVelocity);
    }
}
