using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleManager : MonoBehaviour
{
    [SerializeField]
    private Obstacle[] obstacles;
    public static ObstacleManager instance;
    private void Awake()
    {
        if (instance)
            Destroy(instance);
        instance = this;
    }
    private void Start()
    {
        Timer.instance.onTimerStart.AddListener(randomlySwap);
    }
    public void randomlySwap()
    {
        foreach (var obstacle in obstacles)
        {
            obstacle.swap();
        }
    }
}
