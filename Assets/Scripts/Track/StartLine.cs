using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartLine : MonoBehaviour
{

    private int checkpoint_count;
    public CrowdPeople GO_to_cheer;
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

                GO_to_cheer.TiggerCheer();
            }
        }
    }
}
