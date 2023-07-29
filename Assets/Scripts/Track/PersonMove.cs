using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental;
using UnityEngine;

public class PersonMove : MonoBehaviour
{
    [Header("movement ammount")]
    public float max_move = 0.1f;
    private bool move = false;
    private bool flip = false;

    private Vector2 start_pos;

    // Start is called before the first frame update
    private void Start()
    {
        start_pos = transform.localPosition;
    }

    // Update is called once per frame
    void Update()
    {
        if (move)
        {
            float move_value = Random.Range(0.001f, 0.005f);

            if (!flip)
            {
                transform.localPosition = new Vector2(transform.localPosition.x, transform.localPosition.y + move_value);

                if (transform.localPosition.y >= start_pos.y + max_move)
                {
                    flip = true;
                }
            }

            else if (flip)
            {
                transform.localPosition = new Vector2(transform.localPosition.x, transform.localPosition.y - move_value);

                if (transform.localPosition.y <= start_pos.y - max_move)
                {
                    flip = false;
                }
            }
        }
    }

    public void setMove(bool value)
    {
        move = value;
    }

    public void setFlip(bool value)
    {
        flip |= value;
    }
}
