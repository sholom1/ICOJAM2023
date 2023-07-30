using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boost : MonoBehaviour
{
    [SerializeField]
    float forceAmount;

    private void OnTriggerStay2D(Collider2D collision)
    {
        Vector2 dir = collision.attachedRigidbody.velocity;
        collision.attachedRigidbody.AddForce(dir * forceAmount);
    }
}
