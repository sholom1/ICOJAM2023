using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerManager : MonoBehaviour
{
    public static PlayerManager instance;
    public List<GameObject> playerStartPos = new List<GameObject>();
    public Dictionary<uint, PlayerController_1> players = new Dictionary<uint, PlayerController_1>();
    private int playerCount = 0;
    private int maxPlayers  = 0;

    [Header("Player materials")]
    public Material[] playerMats;

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
            {
                player.p_material = playerMats[playerCount];
                player.setMaterial();
                playerCount++;
            }

            player.transform.position = playerStartPos[playerCount].transform.position;
        }
    }

    public void OnLeave(PlayerController_1 player)
    {
        players.Remove(player.playerID);
        playerCount--;
    }

    public void ChangePlayerInput(string value)
    {
        foreach(PlayerController_1 player in players.Values)
        {
            player.GetComponent<PlayerInput>().currentActionMap = player.GetComponent<PlayerInput>().actions.FindActionMap(value);
        }
    }
}
