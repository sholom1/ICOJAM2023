using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

public class PlayerManager : MonoBehaviour
{
    public static PlayerManager instance;
    public List<GameObject> playerStartPos = new List<GameObject>();
    public Dictionary<uint, PlayerController_1> players = new Dictionary<uint, PlayerController_1>();
    public int playerCount = 0;
    private int maxPlayers  = 0;


    [Header("Player materials")]
    public Material[] playerMats;

    public UnityEvent OnPlayerDestroy;
    public UnityEvent OnPlayerJoin;


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

    public void RestartGame()
    {
        foreach (var player in players.Values)
        {
            Destroy(player.gameObject);
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
        OnPlayerJoin.Invoke();
        player.GetComponent<PlayerInput>().currentActionMap = player.GetComponent<PlayerInput>().actions.FindActionMap("UI");
        ResetPlayerPositions();
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
            player.Value.transform.eulerAngles = new Vector3(0, 0, 0);
            player.Value.gameObject.GetComponent<Rigidbody2D>().velocity = new Vector2(0, 0);
            position++;
        }
    }
    public void ResetPlayers()
    {
        foreach (KeyValuePair<uint, PlayerController_1> player in players)
        {
            player.Value.ResetPlayer();
        }
    }

    public void ChangePlayerInput(string value)
    {
        foreach(PlayerController_1 player in players.Values)
        {
            player.GetComponent<PlayerInput>().currentActionMap = player.GetComponent<PlayerInput>().actions.FindActionMap(value);
        }
    }
}
