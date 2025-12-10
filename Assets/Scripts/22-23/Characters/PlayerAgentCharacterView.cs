using UnityEngine;

public class PlayerAgentCharacterView : MonoBehaviour
{
    private readonly int IsMovingKey = Animator.StringToHash("IsMoving");

    private Animator _animator;

    [SerializeField] private PlayerAgentCharacter _character;

    private void Awake()
    {
        _animator = GetComponent<Animator>();
    }

    private void Update()
    {
        Running(_character.CurrentVelocity.magnitude > 0.05f);
    }

    private void Running(bool isRunning)
    {
        //_animator.SetBool(IsMovingKey, isRunning);
        _animator.SetFloat("Velocity", _character.CurrentVelocity2);
    }
}
