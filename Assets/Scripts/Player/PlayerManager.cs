using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{

    public List<GameObject> playerStartPos = new List<GameObject>();
    public List<PlayerControllerParent> players = new List<PlayerControllerParent>();
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

    public void OnJoin(PlayerController_1 player)
    {
        if (playerCount != maxPlayers)
        {
            players.Add(player);
            player.transform.position = playerStartPos[playerCount].transform.position;
            playerCount++;
        }
    }
    public void OnJoin(PlayerController_2 player)
    {
        if (playerCount != maxPlayers)
        {
            players.Add(player);
            player.transform.position = playerStartPos[playerCount].transform.position;
            playerCount++;
        }
    }
}
