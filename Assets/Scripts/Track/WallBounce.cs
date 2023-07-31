using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallBounce : MonoBehaviour
{
    float bounceForce = 500f;
    private void OnCollisionEnter2D(Collision2D collision)
    {
        Vector2 sum = new Vector2(0, 0);
        foreach(var point in collision.contacts)
        {
            sum += point.normal;
        }
        Vector2 direction = -(sum/collision.contactCount);
        collision.collider.attachedRigidbody.AddForce(direction * bounceForce);
    }
}
