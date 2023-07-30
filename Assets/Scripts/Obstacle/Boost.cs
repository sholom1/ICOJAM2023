using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boost : MonoBehaviour
{
    AreaEffector2D areaEffector;
    CircleCollider2D circleCollider;
    [SerializeField]
    float forceAmount;
    // Start is called before the first frame update
    void Start()
    {
        areaEffector = GetComponent<AreaEffector2D>();
    }

    // Update is called once per frame
    void Update()
    {
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        Vector2 dir = collision.attachedRigidbody.velocity;
        collision.attachedRigidbody.AddForce(dir * forceAmount);
    }
}
