using UnityEngine;
using UnityEngine.AI;

public class InputExample : MonoBehaviour 
{
    [SerializeField] private Character _character;
    [SerializeField] private Character _enemy;
    [SerializeField] private AgentCharacter _agentCharacter;

    private Controller _characterController;
    private Controller _enemyController;

    private NavMeshPath _path;

    private void Awake()
    {
        _path = new NavMeshPath();

        _characterController = new CompositeController(
            new PlayerDirectionalMovableController(_character),
            new PlayerDirectionalRotatableController(_character));
        _characterController.Enable();

        //_enemyController = new CompositeController(
        //    new RandomAIDirectionalMovableController(_enemy, 2f),
        //    new DependentFromVelocityRotatableController(_enemy, _enemy));

        //NavMeshQueryFilter queryFilter = new NavMeshQueryFilter();
        //queryFilter.agentTypeID = 0;
        //queryFilter.areaMask = NavMesh.AllAreas;

        //_enemyController = new CompositeController(
        //    new AgroAIDirectionalMovableController(_enemy, _character.transform, 30, 2, queryFilter, 1),
        //    new DependentFromVelocityRotatableController(_enemy, _enemy));
        //_enemyController = new AgentCharacterController(_agentCharacter, _character.transform, 30, 2, 2);
        //_enemyController.Enable();

    }

    private void Start()
    {
        //_enemy.gameObject.SetActive(false);
    }

    private void Update()
    {
        _characterController.Update(Time.deltaTime);
        //_enemyController.Update(Time.deltaTime);
    }

    //private void OnDrawGizmosSelected()
    //{
    //    NavMeshQueryFilter queryFilter = new NavMeshQueryFilter();
    //    queryFilter.agentTypeID = 0;
    //    queryFilter.areaMask = NavMesh.AllAreas;

    //    NavMesh.CalculatePath(_enemy.transform.position, _character.transform.position, queryFilter, _path);

    //    Gizmos.color = Color.red;

    //    if (_path.status != NavMeshPathStatus.PathInvalid)
    //    {
    //        foreach (Vector3 corner in _path.corners)
    //        {
    //            Gizmos.DrawSphere(corner, 0.3f);
    //        }
    //    }
    //}
}
