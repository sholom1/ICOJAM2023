using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class PlayerDestroyer : MonoBehaviour
{
    [SerializeField]
    private GameObject effect;
    [SerializeField]
    private float movementThreshold = 0.1f;
    private Dictionary<uint, Vector2> positions = new Dictionary<uint, Vector2>();
    private Dictionary<uint, PlayerController_1> playersMarkedForDeath = new Dictionary<uint, PlayerController_1>();
    private bool watchingPlayers;
    void Start()
    {
        Timer.instance.onTimerComplete.AddListener(startListening);
        Timer.instance.onTimerStart.AddListener(destroyPlayers);
    }
    public void RestartGame()
    {
        positions.Clear();
        playersMarkedForDeath.Clear();
    }

    private void startListening()
    {
        watchingPlayers = true;
        //positions = new Dictionary<uint, Vector2>();
        //playersMarkedForDeath = new Dictionary<uint, PlayerController_1>();
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
                if (positions.TryGetValue(player.playerID, out Vector2 position) && Vector2.Distance(position, (Vector2)player.transform.position) > movementThreshold)
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
            Destroy(Instantiate(effect, player.transform.position, Quaternion.identity), 1);
            player.Die();
            AudioManager.instance.CrowdLaugh();

        }
        playersMarkedForDeath.Clear();
        positions.Clear();
    }
}
