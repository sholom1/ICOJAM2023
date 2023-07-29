using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explosion : MonoBehaviour
{
    [SerializeField] private LineRenderer line;
    [SerializeField] private new CircleCollider2D collider;
    [SerializeField] private float maxRadius;
    [SerializeField] private float speed;

    private void FixedUpdate()
    {
        collider.radius = Mathf.Min(collider.radius + speed * Time.fixedDeltaTime, maxRadius);
        RenderCollider.renderCircle(line, transform.position, collider.radius);
    }
}
