using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    public static PlayerManager instance;
    public List<GameObject> playerStartPos = new List<GameObject>();
    public Dictionary<uint, PlayerController_1> players = new Dictionary<uint, PlayerController_1>();
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
            if (players.TryAdd(player.playerID, player))
                playerCount++;

            player.transform.position = playerStartPos[playerCount].transform.position;
        }
    }

    public void OnLeave(PlayerController_1 player)
    {
        players.Remove(player.playerID);
        playerCount--;
    }
}
