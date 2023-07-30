using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explosion : MonoBehaviour
{
    [SerializeField] private LineRenderer line;
    [SerializeField] private new CircleCollider2D collider;
    [SerializeField] private float maxRadius;
    [SerializeField] private float speed;
    [SerializeField] private float explosiveForce;

    private void FixedUpdate()
    {
        collider.radius = Mathf.Min(collider.radius + speed * Time.fixedDeltaTime, maxRadius);
        if (maxRadius - collider.radius < .01f)
            Destroy(gameObject);
        RenderCollider.renderCircle(line, transform.position, collider.radius);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        Vector2 dir = (collision.transform.position - transform.position).normalized;
        collision.attachedRigidbody.AddForce(dir * explosiveForce, ForceMode2D.Impulse);
    }
}
