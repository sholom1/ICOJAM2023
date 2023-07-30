using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraPan : MonoBehaviour
{
    [Header("Object look at settings")]
    public bool lookat = false;
    GameObject look_at_object;

    [Header("Movement settings")]
    public bool move_camera = false;
    public List<Vector3> movement_vectors = new List<Vector3>();
    public float movement_speed;
    private int movement_index = 0;

    // Start is called before the first frame update
    void Start()
    {
        transform.position = movement_vectors[0];
    }

    // Update is called once per frame
    void Update()
    {
        if(move_camera)
        {
            if (look_at_object)
            {
                gameObject.transform.LookAt(look_at_object.transform);
            }
            
            transform.position = Vector3.MoveTowards(transform.position, movement_vectors[movement_index], movement_speed * Time.deltaTime);

            if(Vector3.Distance(transform.position, movement_vectors[movement_index]) < 0.1f)
            {
                movement_index ++;

                if(movement_index == movement_vectors.Count)
                {
                    Destroy(this);
                }
            }
        }
    }

    public void TiggerTransistion()
    {
        move_camera = true;
    }
}
