using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class AgentJumper
{
    private NavMeshAgent _agent;
    private MonoBehaviour _coroutineRunner;
    private float _speed;
    private float _progress;

    private Coroutine _jumpProcess;
    private AnimationCurve _jumpCurve;

    public AgentJumper(NavMeshAgent navMeshAgent, float speed, MonoBehaviour coroutineRunner, AnimationCurve jumpCurve)
    {
        _agent = navMeshAgent;
        _speed = speed;
        _coroutineRunner = coroutineRunner;
        _jumpCurve = jumpCurve;
    }

    public bool InProcess => _jumpProcess != null;

    public float JumpDuration => _progress;

    public void Jump(OffMeshLinkData offMeshLinkData)
    {
        if (InProcess)
            return;

        _jumpProcess = _coroutineRunner.StartCoroutine(JumpProcess(offMeshLinkData));
    }

    private IEnumerator JumpProcess(OffMeshLinkData offMeshLinkData)
    {
        Vector3 startPosition = offMeshLinkData.startPos;
        Vector3 endPosition = offMeshLinkData.endPos;

        float duration = Vector3.Distance(startPosition, endPosition) / _speed;

        _progress = 0;
        float jumpOffset = 0;

        while (_progress < duration)
        {
            jumpOffset = _jumpCurve.Evaluate(_progress / duration);

            _agent.transform.position = Vector3.Lerp(startPosition, endPosition, _progress / duration) + Vector3.up * jumpOffset;
            _progress += Time.deltaTime;

            yield return null;
        }

        _agent.CompleteOffMeshLink();
        _jumpProcess = null;
    }
}
