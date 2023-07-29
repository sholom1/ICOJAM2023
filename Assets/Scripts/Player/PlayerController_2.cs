using UnityEngine;

public class PlayerController_2 : PlayerControllerParent
{
    public float max_speed;
    public float speed_modifier;
    public float acceleration_mod;
    public float reverse_speed;
    public float turn_speed;
    public void Accelerate(float throttle)
    {
        if (rb.velocity.magnitude < max_speed / (throttle < 0 ? 2 : 1))
        {
            Vector2 speed = transform.up * speed_modifier * throttle;
            rb.AddForce(speed, ForceMode2D.Force);
        }
    }

    public void Reverse()
    {
        if (rb.velocity.magnitude < max_speed/2)
        {
            rb.AddForce(-transform.up * reverse_speed, ForceMode2D.Force);
        }
    }

    public void Turn(float turnSign)
    {
        print(turnSign);
        var dir = Mathf.Atan2(turnSign, transform.up.y) * Mathf.Rad2Deg;
        print(dir);
        //transform.right = dir.normalized;
    }

    public Vector2 RotateVector(Vector2 vector, float angle)
    {
        float radian = angle * Mathf.Deg2Rad;
        return vector.x * new Vector2(Mathf.Cos(radian), Mathf.Sin(radian)) + vector.y * new Vector2(Mathf.Sin(radian), Mathf.Cos(radian));
    }

}
