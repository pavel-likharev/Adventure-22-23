using UnityEngine;
using UnityEngine.AI;

public class InputExample : MonoBehaviour 
{
    [SerializeField] private Character _character;

    private Controller _characterController;

    private NavMeshPath _path;

    private void Awake()
    {
        _path = new NavMeshPath();

        //_characterController = new CompositeController(
        //    new PlayerDirectionalMovableController(_character),
        //    new PlayerDirectionalRotatableController(_character));
        NavMeshQueryFilter queryFilter = new NavMeshQueryFilter();
        queryFilter.agentTypeID = 0;
        queryFilter.areaMask = NavMesh.AllAreas;


        _characterController = new CompositeController(
            new PlayerDirectionalMovableMouseController(_character, queryFilter),
            new DependentFromVelocityRotatableController(_character, _character));
        _characterController.Enable();
    }

    private void Update()
    {
        _characterController.Update(Time.deltaTime);
        //_enemyController.Update(Time.deltaTime);
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;

        Ray mousePos = Camera.main.ScreenPointToRay(Input.mousePosition);
        Gizmos.DrawRay(mousePos.origin, mousePos.direction * 100);
    }
}
