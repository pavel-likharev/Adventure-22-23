using UnityEngine;

public class DependentFromVelocityRotatableController : Controller
{
    private IDirectionalMovable _movable;
    private IDirectionalRotatable _rotatable;

    public DependentFromVelocityRotatableController(IDirectionalMovable movable, IDirectionalRotatable rotatable)
    {
        _movable = movable;
        _rotatable = rotatable;
    }

    protected override void UpdateLogic(float deltaTime)
    {
        _rotatable.SetRotationDirection(_movable.CurrentVelocity);
    }
}
