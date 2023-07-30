using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Timer : MonoBehaviour
{
    public static Timer instance;
    public UnityEvent onTimerStart;
    public UnityEvent<float> onTimerUpdate;
    public UnityEvent onTimerComplete;
    [SerializeField]
    private float maxTime = 20;
    [SerializeField]
    private float minTime = 6;
    [SerializeField]
    private float timeRemaining;

    private void Awake()
    {
        if (instance)
            Destroy(instance);
        instance = this;
    }
    public void Update()
    {
        if (timeRemaining > 0)
        {
            timeRemaining -= Time.deltaTime;
            onTimerUpdate.Invoke(timeRemaining);
            if (timeRemaining <= 0) onTimerComplete.Invoke();
        }
    }
    public void StartTimer(float time)
    {
        onTimerStart.Invoke();
        timeRemaining = time;
        onTimerUpdate.Invoke(time);
    }
    public void RestartTimer()
    {
        StartTimer(Random.Range(minTime, maxTime));
    }
}
