using UnityEngine;
using UnityEngine.AI;
using UnityEngine.TextCore.Text;

public class PlayerDirectionalMovableMouseController : Controller
{

    private const int MinCornersCountInPathToMove = 2;
    private const int StartCornerIndex = 0;
    private const int TargetCornerIndex = 1;

    private IDirectionalMovable _movable;
    private Vector3 _target;
    private LayerMask _groundLayerMask = 6;

    private NavMeshQueryFilter _queryFilter;

    private NavMeshPath _pathToTarget = new NavMeshPath();

    private bool _isMoving = false;

    public PlayerDirectionalMovableMouseController(IDirectionalMovable movable, NavMeshQueryFilter queryFilter)
    {
        _movable = movable;
        _queryFilter = queryFilter;
        _target = movable.Position;
    }

    protected override void UpdateLogic(float deltaTime)
    {
        if (Input.GetMouseButtonDown(0))
        {
            DetectGroundWithMouse();
        }

        if (_isMoving == false)
            return;

        if (NavMeshUtils.TryGetPath(_movable.Position, _target, _queryFilter, _pathToTarget))
        {
            float distanceToTarget = NavMeshUtils.GetPathLength(_pathToTarget);

            if (IsTargetReached(distanceToTarget))
            {
                Debug.Log(_target);
                _isMoving = false;
                _movable.SetMoveDirection(Vector3.zero);
                return;
            }

            if (EnoughCornersInPath(_pathToTarget))
            {
                _movable.SetMoveDirection(_pathToTarget.corners[TargetCornerIndex] - _pathToTarget.corners[StartCornerIndex]);
                return;
            }

        }
    }

    public void DetectGroundWithMouse()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(ray, out RaycastHit hitInfo))
        {
            if(hitInfo.collider.gameObject.layer == _groundLayerMask)
                {
                    Debug.Log(hitInfo.point);
                    OnGroundClicked(hitInfo);
                }
                else
                    Debug.Log("Клик не по земле");
        }
        else
        {
            Debug.Log("Ничего не попало");
        }
    }

    private void OnGroundClicked(RaycastHit hit)
    {
        // Ваш код при клике по земле
        // Например: перемещение персонажа, построение здания и т.д.
        _target = hit.point;
        _isMoving = true;

        // Визуализация точки попадания (опционально)
        CreateHitMarker(hit.point);
    }
    private bool IsTargetReached(float distanceToTarget) => distanceToTarget <= 0.2f;
    private bool EnoughCornersInPath(NavMeshPath path) => _pathToTarget.corners.Length >= MinCornersCountInPathToMove;
    private void CreateHitMarker(Vector3 position)
    {
        // Создаем временный объект для визуализации точки попадания
        GameObject marker = GameObject.CreatePrimitive(PrimitiveType.Sphere);
        marker.transform.position = position + Vector3.up * 0.1f;
        marker.transform.localScale = Vector3.one * 1f;
        marker.GetComponent<Renderer>().material.color = Color.red;

        // Удаляем через 2 секунды
        GameObject.Destroy(marker, 0.5f);
    }
}
