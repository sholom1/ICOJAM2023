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
    private float max = 6;
    [SerializeField]
    private float min = 1;
    [SerializeField]
    private float timeRemaining;
    public bool firstTimer = true;

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

            if (timeRemaining <= 0)
            {
                onTimerComplete.Invoke();
                AudioManager.instance.Stop();
            } 
                
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
        print("reset");
        if (firstTimer)
        {
            print("first time dun");
            StartTimer(15);
            //AudioManager.instance.Play("15loopintro");
            firstTimer = false;
            return;
        }
        print("on some next shit");
        int n = (int)Random.Range(min, max);
        if(n == 1)
        {
            StartTimer(6);
            AudioManager.instance.Play("6loop");

        }
            
        else if (n == 2)
        {
            StartTimer(9);
            AudioManager.instance.Play("9loop");
        }
            
        else if (n == 3)
        {
            StartTimer(12);
            AudioManager.instance.Play("12loop");
        }
            
        else if (n == 4)
        {
            StartTimer(15);
            AudioManager.instance.Play("15loop");
        }
            
        else if (n == 5)
        {
            StartTimer(18);
            AudioManager.instance.Play("18loop");
        }
        
    }
}
