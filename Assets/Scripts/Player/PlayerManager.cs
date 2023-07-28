using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    public static PlayerManager instance;
    public List<GameObject> playerStartPos = new List<GameObject>();
    public List<PlayerController_1> players = new List<PlayerController_1>();
    private int playerCount = 0;
    private int maxPlayers  = 0;

    private void Start()
    {
        if (instance)
            Destroy(instance);
        instance = this;
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
}
