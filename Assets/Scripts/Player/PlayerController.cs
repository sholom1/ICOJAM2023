using System.Collections;
using System.Collections.Generic;
using UnityEditorInternal;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

public class PlayerController_1 : PlayerControllerParent
{
    private Vector2 move_Position;
    public float max_speed;
    public float speed_modifier;
    public float acceleration_mod;

    private void FixedUpdate()
    {
        MovePlayer();
        UpdateRotation();
    }

    private void MovePlayer()
    {



        if (Mathf.Abs(rb.velocity.x) + Mathf.Abs(rb.velocity.y) < max_speed)
        {
            Vector2 speed = move_Position *
                (speed_modifier + (Mathf.Abs(rb.velocity.x*acceleration_mod) + Mathf.Abs(rb.velocity.y * acceleration_mod)));
            rb.AddForce(speed , ForceMode2D.Force);
        }
    }

    private void UpdateRotation()
    {
        if (move_Position != Vector2.zero)
        {
            float angle = Mathf.Atan2(-move_Position.x, move_Position.y) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.AngleAxis(angle, Vector2.up);
        }
    }

    public void updateMovement(Vector2 value)
    {
        //value.x = -value.x;
        move_Position = value;
    }
}
