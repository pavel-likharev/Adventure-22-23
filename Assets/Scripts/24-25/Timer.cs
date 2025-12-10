using System.Collections;
using UnityEngine;

public class Timer
{
    private float _timeLimit;
    private float _ellapsedTime;

    private MonoBehaviour _coroutineRunner;
    private Coroutine _process;

    public Timer(MonoBehaviour coroutineRunner)
    {
        _coroutineRunner = coroutineRunner;
    }

    public float TimeLimit => _timeLimit;

    public bool InProcess(out float ellipsedTime)
    {
        if (_process == null)
        {
            ellipsedTime = TimeLimit;
            return false;
        }

        ellipsedTime = _ellapsedTime;
        return true;
    } 

    public void StartProcess(float time)
    {
        _timeLimit = time;

        if (_process != null)
            _coroutineRunner.StopCoroutine(_process);

        _process = _coroutineRunner.StartCoroutine(Process());
    }

    private IEnumerator Process()
    {
        _ellapsedTime = 0;

        while (_ellapsedTime < _timeLimit)
        {
            _ellapsedTime += Time.deltaTime;

            if (_ellapsedTime >= _timeLimit)
                _ellapsedTime = _timeLimit;

            yield return null;
        }

        _process = null;
    }
}