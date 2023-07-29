using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController_2 : PlayerControllerParent
{
    [SerializeField] Rigidbody2D rb;
    public float max_speed;
    public float speed_modifier;
    public float acceleration_mod;
    public float reverse_speed;
    public float turn_speed;

    private PlayerManager input_manager;
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

    public void Reverse()
    {
        if (rb.velocity.magnitude < max_speed/2)
        {
            rb.AddForce(-direction * reverse_speed, ForceMode2D.Force);
        }
    }

    public void Turn(float turnSign)
    {   
        direction = RotateVector(direction, turnSign * turn_speed);
        transform.right = direction.normalized;
    }

    public Vector2 RotateVector(Vector2 vector, float angle)
    {
        Debug.Log("rotating");
        float radian = angle * Mathf.Deg2Rad;
        return vector.x * new Vector2(Mathf.Cos(radian), Mathf.Sin(radian)) + vector.y * new Vector2(Mathf.Sin(radian), Mathf.Cos(radian));
    }

}
