using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class PlayerDestroyer : MonoBehaviour
{
    [SerializeField]
    private GameObject[] players;
    private List<Vector3> positions;
    private bool watchingPlayers;
    void Start()
    {
           //Timer.instance.onTimerComplete()
    }
    private void startListening()
    {
        watchingPlayers = true;
        positions = new List<Vector3>(players.Length);
        foreach (var player in players)
        {
            positions.Add(player.transform.position);
        }
    }
    private void Update()
    {
        if (watchingPlayers)
        {

        }
    }
}
