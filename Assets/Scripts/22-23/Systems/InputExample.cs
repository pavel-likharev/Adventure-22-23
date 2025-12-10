using UnityEngine;
using UnityEngine.AI;

public class InputExample : MonoBehaviour 
{
    [SerializeField] private Character _character;
    [SerializeField] private PlayerAgentCharacter _playerAgentCharacter;
    //[SerializeField] private AgentCharacter _agentCharacter;

    [SerializeField] private Transform _target;

    private Controller _agetnCharacterController;
    private Controller _characterController;

    private NavMeshPath _path;

    private void Awake()
    {
        //_path = new NavMeshPath();

        //_characterController = new CompositeController(
        //    new PlayerDirectionalMovableController(_character),
        //    new PlayerDirectionalRotatableController(_character));
        //NavMeshQueryFilter queryFilter = new NavMeshQueryFilter();
        //queryFilter.agentTypeID = 0;
        //queryFilter.areaMask = NavMesh.AllAreas;

        //_agentController = new AgentAgroCharacterController(_agentCharacter, _target, 30, 2, 2);
        //_agentController.Enable();

        //_characterController = new CompositeController(
        //    new PlayerDirectionalMovableMouseController(_character, queryFilter),
        //    new DependentFromVelocityRotatableController(_character, _character));
        //_characterController.Enable();

        _agetnCharacterController = new PlayerAgentCharacterController(_playerAgentCharacter);
        _agetnCharacterController.Enable();
    }

    private void Update()
    {
        _agetnCharacterController.Update(Time.deltaTime);
        //_agentController.Update(Time.deltaTime);
        //_characterController.Update(Time.deltaTime);
        //_enemyController.Update(Time.deltaTime);

        if (Input.GetKeyDown(KeyCode.F))
            _playerAgentCharacter.SwitchWorkStatusHealSpawner();
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;

        Ray mousePos = Camera.main.ScreenPointToRay(Input.mousePosition);
        Gizmos.DrawRay(mousePos.origin, mousePos.direction * 100);
    }
}