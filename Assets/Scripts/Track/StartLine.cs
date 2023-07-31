using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class StartLine : MonoBehaviour
{
    private int checkpoint_count;
    public CrowdPeople GO_to_cheer;
    public UnityEvent<PlayerController_1> OnPlayerCrossLine;

    // Start is called before the first frame update
    void Start()
    {
        CheckPoint[] checkpoints = GameObject.FindObjectsOfType<CheckPoint>();
        checkpoint_count = checkpoints.Length;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.GetComponent<PlayerController_1>() != null)
        {
            var Player = collision.GetComponent<PlayerController_1>();
            if (Player.check_point_num == checkpoint_count)
            {
                Player.laps_completed++;
                Player.check_point_num = 0;
                OnPlayerCrossLine.Invoke(Player);
                AudioManager.instance.CrowdCheer();
                GO_to_cheer.TiggerCheer();
            }
        }
    }
}
