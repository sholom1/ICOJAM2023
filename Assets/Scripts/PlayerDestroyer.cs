using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class PlayerDestroyer : MonoBehaviour
{
    [SerializeField]
    private GameObject effect;
    private Dictionary<uint, Vector2> positions;
    private Dictionary<uint, PlayerController_1> playersMarkedForDeath;
    private bool watchingPlayers;
    void Start()
    {
        Timer.instance.onTimerComplete.AddListener(startListening);
        Timer.instance.onTimerStart.AddListener(destroyPlayers);
    }
    private void startListening()
    {
        watchingPlayers = true;
        positions = new Dictionary<uint, Vector2>();
        playersMarkedForDeath = new Dictionary<uint, PlayerController_1>();
        foreach (var player in PlayerManager.instance.players.Values)
        {
            positions.Add(player.playerID, player.transform.position);
        }
    }
    private void Update()
    {
        if (watchingPlayers)
        {
            foreach (var player in PlayerManager.instance.players.Values)
            {
                if (positions.TryGetValue(player.playerID, out Vector2 position) && position != (Vector2)player.transform.position)
                {
                    playersMarkedForDeath.TryAdd(player.playerID, player);
                    player.deathMark.SetActive(true);
                }
            }
        }
    }
    private void destroyPlayers()
    {
        watchingPlayers = false;
        foreach (var player in playersMarkedForDeath.Values)
        {
            Destroy(player.gameObject);
            Destroy(Instantiate(effect, player.transform.position, Quaternion.identity), 1);
        }
    }
}
