using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreKeeper : MonoBehaviour
{
    int totalRounds;
    Dictionary<uint, uint> score;
    PlayerManager playerManager;

    // Start is called before the first frame update
    void Start()
    {
        playerManager = GameObject.FindObjectOfType<PlayerManager>();
        playerManager.OnPlayerDestroy.AddListener(PlayerDestroyed);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void StartRound()
    {
        playerManager.ResetPlayerPositions();
    }

    public void PlayerDestroyed()
    {
        bool endRound = false;
        int alivePlayers = 0;

        PlayerController_1 leadPlayer = null;
        foreach (KeyValuePair<uint, PlayerController_1> player in playerManager.players)
        {
            if (player.Value.dead == false)
            {
                alivePlayers++;
            }

            if (leadPlayer == null)
            {
                leadPlayer = player.Value;
            }
            else
            {
                if(leadPlayer.laps_completed < player.Value.laps_completed && leadPlayer.check_point_num < player.Value.check_point_num)
                {
                    leadPlayer = player.Value;
                }
            }
        }
        if(alivePlayers <= 1)
        {
            endRound = true;
        }
    }
}
