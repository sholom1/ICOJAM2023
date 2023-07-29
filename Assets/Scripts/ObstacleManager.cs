using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleManager : MonoBehaviour
{
    [SerializeField]
    private Obstacle[] obstacles;
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
