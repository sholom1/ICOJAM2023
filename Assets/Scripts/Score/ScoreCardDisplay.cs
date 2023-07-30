using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class ScoreCardDisplay : MonoBehaviour
{
    PlayerController_1 player;
    [SerializeField]
    private TextMeshProUGUI playerText;
    [SerializeField]
    private Image playerImage;
    [SerializeField]
    private TextMeshProUGUI roundsText;
    [SerializeField]
    private TextMeshProUGUI lapsText;
    [SerializeField]
    private TextMeshProUGUI checkPointsText;
    [SerializeField]
    private TextMeshProUGUI coinsText;
    public void SetPlayer(PlayerController_1 _player)
    {
        player = _player;
        playerText.text = "Player " + player.playerID;
        playerImage.color = player.p_material.color;
    }
    public uint GetPlayerID()
    {
        return player.playerID;
    }
    public void UpdateScore(uint score, uint coins)
    {
        roundsText.text = score.ToString();
        lapsText.text = player.laps_completed.ToString();
        checkPointsText.text = player.check_point_num.ToString();
        coinsText.text = coins.ToString();
    }
}
