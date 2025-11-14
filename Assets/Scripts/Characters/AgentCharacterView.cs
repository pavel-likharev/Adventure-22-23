using UnityEngine;

public class AgentCharacterView : MonoBehaviour
{
    private readonly int IsRunningKey = Animator.StringToHash("IsRunning");

    private Animator animator;

    [SerializeField] private AgentCharacter _agentCharacter;

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    private void Update()
    {
        Running(_agentCharacter.CurrentVelocity.magnitude > 0.02f);
        //fdfsdf
    }

    private void Running(bool isRunning) => animator.SetBool(IsRunningKey, isRunning);
}
