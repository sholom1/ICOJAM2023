using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController_2 : PlayerControllerParent
{
    [SerializeField] Rigidbody2D rb;
    public float max_speed;
    public float speed_modifier;
    public float acceleration_mod;
    public float drag_mag;
    public float turn_speed;

    private PlayerManager input_manager;

    public int playerID;

    private Vector2 direction = Vector2.right;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        input_manager = GameObject.FindObjectOfType<PlayerManager>();

        input_manager.OnJoin(this);
    }

    public void Accelerate()
    {
        if (rb.velocity.magnitude < max_speed)
        {
            Vector2 speed = direction * speed_modifier;
            rb.AddForce(speed, ForceMode2D.Force);
        }
    }

    public void Break()
    {
        if(rb.velocity.magnitude > 0f)
        {
            rb.AddForce(-direction * Mathf.Min(drag_mag, rb.velocity.magnitude), ForceMode2D.Force);
        }
    }

    public void TurnLeft()
    {   
        direction = RotateVector(direction, turn_speed);
    }
    public void TurnRight()
    {
        direction = RotateVector(direction, -turn_speed);
    }

    public Vector2 RotateVector(Vector2 vector, float angle)
    {
        float radian = angle * Mathf.Deg2Rad;
        return vector.x * new Vector2(Mathf.Cos(radian), Mathf.Sin(radian)) + vector.y * new Vector2(Mathf.Sin(radian), Mathf.Cos(radian));
    }
}
