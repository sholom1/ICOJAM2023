using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour
{

    public List<GameObject> playerStartPos = new List<GameObject>();
    public List<GameObject> players = new List<GameObject>();
    private int playerCount = 0;
    private int maxPlayers  = 0;

    private void Start()
    {
        var startPositions = GameObject.FindGameObjectsWithTag("StartPos");

        foreach (var position in startPositions)
        {
            playerStartPos.Add(position);
            maxPlayers++;
        }


    }

    public void OnJoin(GameObject player)
    {
        if (playerCount != maxPlayers)
        {
            players.Add(player);
            player.transform.position = playerStartPos[playerCount].transform.position;
            playerCount++;
        }
    }
}
