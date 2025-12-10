using UnityEngine;
using UnityEngine.AI;

public class PlayerAgentCharacterController : Controller
{
    //private AgentCharacter _agentCharacter;
    private PlayerAgentCharacter _playerAgentCharacter;
    private Vector3 _target;

    private NavMeshPath _pathToTarget = new NavMeshPath();
    private LayerMask _groundLayerMask = 6;

    private bool _isMoving = false;

    public PlayerAgentCharacterController(PlayerAgentCharacter playerAgentCharacter)
    {
        _playerAgentCharacter = playerAgentCharacter;
        //_target = target;
        _target = _playerAgentCharacter.Position;
    }

    protected override void UpdateLogic(float deltaTime)
    {

        if (Input.GetMouseButtonDown(0))
            DetectGroundWithMouse();

        //if (_isMoving == false)
        //    return;

        if (_playerAgentCharacter.IsOnNavMeshLink(out OffMeshLinkData linkData))
        {
            if (_playerAgentCharacter.InJumpProcess == false)
            {
                _playerAgentCharacter.SetRotationDirection(linkData.endPos - linkData.startPos);

                _playerAgentCharacter.Jump(linkData);
            }

            return;
        }

        _playerAgentCharacter.SetRotationDirection(_playerAgentCharacter.CurrentVelocity);

        if (_playerAgentCharacter.TryGetPath(_target, _pathToTarget))
        {
                _playerAgentCharacter.ResumeMove();
                _playerAgentCharacter.SetDestination(_target);
            return;

            //float distanceToTarget = NavMeshUtils.GetPathLength(_pathToTarget);

            //if (IsTargetReached(distanceToTarget))
            //    _idleTimer = _timeForIdle;

            //if (InAgroRange(distanceToTarget) && IdleTimerIsUp())
            //{
            //    _agentCharacter.ResumeMove();
            //    _agentCharacter.SetDestination(_target.position);
            //    return;
            //}
        }

        _playerAgentCharacter.StopMove();
        //_isMoving = false;
    }

    public void DetectGroundWithMouse()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(ray, out RaycastHit hitInfo, Mathf.Infinity, ~0, QueryTriggerInteraction.Ignore))
            if (hitInfo.collider.gameObject.layer == _groundLayerMask)
                OnGroundClicked(hitInfo);
    }

    private void OnGroundClicked(RaycastHit hit)
    {
        _target = hit.point;
        //_isMoving = true;

        CreateHitMarker(hit.point);
    }

    private void CreateHitMarker(Vector3 position)
    {
        GameObject marker = GameObject.CreatePrimitive(PrimitiveType.Sphere);
        marker.transform.position = position + Vector3.up * 0.1f;
        marker.transform.localScale = Vector3.one * 0.5f;
        marker.GetComponent<Renderer>().material.color = Color.red;

        GameObject.Destroy(marker, 0.3f);
    }


    //private bool IsTargetReached(float distanceToTarget) => distanceToTarget <= _minDistanceToTarget;
}
