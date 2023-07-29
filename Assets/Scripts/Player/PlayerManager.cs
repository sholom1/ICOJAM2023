using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PlayerManager : MonoBehaviour
{
    public static PlayerManager instance;
    public List<GameObject> playerStartPos = new List<GameObject>();
    public Dictionary<uint, PlayerController_1> players = new Dictionary<uint, PlayerController_1>();
    public int playerCount = 0;
    private int maxPlayers  = 0;

    public UnityEvent OnPlayerDestroy;

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
    public void PlayerDied()
    {
        OnPlayerDestroy.Invoke();
    }

    public void ResetPlayerPositions()
    {
        int position = 0;
        foreach(KeyValuePair<uint, PlayerController_1> player in players)
        {
            player.Value.transform.position = playerStartPos[position].transform.position;
            position++;
        }
    }
}
