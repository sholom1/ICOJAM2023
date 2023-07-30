using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PowerupSelector : MonoBehaviour
{
    [SerializeField] Rigidbody2D bulletPrefab;
    [SerializeField] float bulletSpeed;
    [SerializeField] Collider2D explosionPrefab;
    enum powerups
    {
        bullet,
        explosion,
        swapObstacles,
        none
    }
    private powerups[] avaliablePowerups = { powerups.bullet, powerups.explosion, powerups.swapObstacles };
    private powerups selectedPowerup;
    public void OnPowerUp(InputAction.CallbackContext ctx)
    {
        if (!ctx.ReadValueAsButton()) return;
        // Todo add cost
        if (selectedPowerup == powerups.none)
        {
            selectedPowerup = avaliablePowerups[Random.Range(0, avaliablePowerups.Length)];
            return;
        }
        switch (selectedPowerup)
        {
            case powerups.bullet:
                var bullet = Instantiate(bulletPrefab, transform.position + transform.up, transform.rotation);
                bullet.velocity = GetComponent<Rigidbody2D>().velocity + (Vector2)transform.up * bulletSpeed;
                Destroy(bullet.gameObject, 5);
                break;
            case powerups.explosion:
                Physics2D.IgnoreCollision(Instantiate(explosionPrefab, transform.position, Quaternion.identity), GetComponent<Collider2D>());
                break;
            case powerups.swapObstacles:
                ObstacleManager.instance.Swap();
                break;
        }
        selectedPowerup = powerups.none;
    }
}
