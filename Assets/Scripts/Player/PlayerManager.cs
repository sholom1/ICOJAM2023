using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    public static PlayerManager instance;
    public List<GameObject> playerStartPos = new List<GameObject>();
    public Dictionary<uint, PlayerControllerParent> players = new Dictionary<uint, PlayerControllerParent>();
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

    public void OnJoin(PlayerControllerParent player)
    {
        if (playerCount != maxPlayers)
        {
            if (players.TryAdd(player.playerID, player))
                playerCount++;

            player.transform.position = playerStartPos[playerCount].transform.position;
            player.transform.up = playerStartPos[playerCount].transform.up;
        }
    }

    public void OnLeave(PlayerControllerParent player)
    {
        players.Remove(player.playerID);
        playerCount--;
    }
}
