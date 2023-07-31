using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PowerupSelector : MonoBehaviour
{
    [SerializeField] Rigidbody2D bulletPrefab;
    [SerializeField] float bulletSpeed;
    [SerializeField] Collider2D explosionPrefab;
    private bool isSelecting;
    private ScoreKeeper scoreKeeper;
    private PlayerController_1 player;
    [SerializeField]
    private int cost = 5;
    [SerializeField]
    private int leadPenalty = 2;
    private int cost_actual { get { return (cost * (player.inLead ? 1 : leadPenalty)); } }
    
    private PowerUpTypes[] avaliablePowerups = { PowerUpTypes.bullet, PowerUpTypes.explosion, PowerUpTypes.swapObstacles };
    private PowerUpTypes selectedPowerup = PowerUpTypes.none;
    private void Start()
    {
        player = GetComponent<PlayerController_1>();
        scoreKeeper = FindObjectOfType<ScoreKeeper>();
    }
    public void OnPowerUp(InputAction.CallbackContext ctx)
    {
        if (!ctx.performed) return;
        if (isSelecting) return;
        // Todo add cost
        if (selectedPowerup == PowerUpTypes.none)
        {
            if (scoreKeeper.GetCoins(player.playerID) < cost_actual)
                return;
            int index = Random.Range(0, avaliablePowerups.Length);
            selectedPowerup = avaliablePowerups[index];
            scoreKeeper.SubtractCoins(player.playerID, (uint)cost_actual);
            isSelecting = true;
            player.players_stick.selectPowerUp(index, () => isSelecting = false);
            return;
        }
        switch (selectedPowerup)
        {
            case PowerUpTypes.bullet:
                var bullet = Instantiate(bulletPrefab, transform.position + transform.up, transform.rotation);
                bullet.velocity = GetComponent<Rigidbody2D>().velocity + (Vector2)transform.up * bulletSpeed;
                Destroy(bullet.gameObject, 5);
                break;
            case PowerUpTypes.explosion:
                Physics2D.IgnoreCollision(Instantiate(explosionPrefab, transform.position, Quaternion.identity), GetComponent<Collider2D>());
                break;
            case PowerUpTypes.swapObstacles:
                ObstacleManager.instance.Swap();
                break;
        }
        selectedPowerup = PowerUpTypes.none;
        player.players_stick.resetPowerUpImage();
    }
}
enum PowerUpTypes
{
    bullet,
    explosion,
    swapObstacles,
    none
}