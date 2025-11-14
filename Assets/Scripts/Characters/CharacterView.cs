using UnityEngine;

public class CharacterView : MonoBehaviour
{
    private readonly int IsRunningKey = Animator.StringToHash("IsRunning");

    private Animator animator;

    [SerializeField] private Character _character;

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    private void Update()
    {
        Running(_character.CurrentVelocity.magnitude > 0.02f);
    }

    private void Running(bool isRunning) => animator.SetBool(IsRunningKey, isRunning);
}
