using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
public class CameraPan : MonoBehaviour
{
    [Header("Movement settings")]
    public bool move_camera = false;
    public List<Vector3> movement_vectors = new List<Vector3>();
    public float movement_speed;
    private int movement_index = 0;

    public int screen_index = 0;

    [Header("Help screen")]
    public Vector3 help_location;
    public Quaternion help_rotation;

    private Quaternion start_rot;

    public UnityEvent OnCompleteZoom;

    // Start is called before the first frame update
    void Start()
    {
        transform.position = movement_vectors[0];
        start_rot = transform.rotation;
    }

    // Update is called once per frame
    void Update()
    {
        if (move_camera)
        {
            transform.position = Vector3.MoveTowards(transform.position, movement_vectors[movement_index], movement_speed * Time.deltaTime);

            if(Vector3.Distance(transform.position, movement_vectors[movement_index]) < 0.1f)
            {
                movement_index ++;

                if(movement_index == movement_vectors.Count)
                {
                    OnCompleteZoom.Invoke();
                    Destroy(this);
                }
            }
        }

        if(!move_camera)
        {
            if (screen_index == 1)
            {
                //move to help on left
                transform.position = Vector3.MoveTowards(transform.position, help_location, movement_speed * Time.deltaTime);

                transform.rotation = Quaternion.Lerp(transform.rotation, help_rotation, movement_speed*1.5f * Time.deltaTime);
            }
            else if (screen_index == 0)
            {
                //move back to right
                transform.position = Vector3.MoveTowards(transform.position, movement_vectors[0], movement_speed * Time.deltaTime);
                transform.rotation = Quaternion.Lerp(transform.rotation, start_rot, movement_speed*1.5f * Time.deltaTime);
            }
        }
    }

    public void moveCameraLeft(Vector2 value)
    {
        if (value.x > 0.5)
        {
            screen_index = 1;

        }
        else if(value.x < -0.5 && screen_index != 0)
        {
            //move back to right
            screen_index = 0;
        }
    }

    public bool TiggerTransistion()
    {
        if (screen_index == 0 && Vector3.Distance(this.gameObject.transform.position, movement_vectors[0]) < 0.01f &&
            transform.rotation == start_rot)
        {
            move_camera = true;
            return true;
        }
        return false;
    }
}
