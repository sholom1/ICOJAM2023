using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrowdPeople : MonoBehaviour
{

    public List<GameObject> crowd_members = new List<GameObject>();
    public float cheer_time = 1.0f;
    private float current_cheer_time;

    private bool trigger_cheer = false;


    // Start is called before the first frame update
    void Start()
    {
        current_cheer_time = cheer_time;

        for (int i = 0; i != transform.childCount; i++)
        {
            crowd_members.Add(transform.GetChild(i).gameObject);
        }
    }

    public void TiggerCheer()
    {

        foreach (GameObject people in crowd_members)
        {
            people.GetComponent<PersonMove>().setMove(true);
            trigger_cheer = true;
            current_cheer_time = cheer_time;
        }
    }

    private void Update()
    {
        if (trigger_cheer)
        {
            updateCheer();
        }
    }

    private void updateCheer()
    {
        if(current_cheer_time >= 0.0f)
        {
            current_cheer_time -= Time.deltaTime;
        }
        else
        {

            foreach (GameObject people in crowd_members)
            {
                people.GetComponent<PersonMove>().setMove(false);
            }

            trigger_cheer = false;
        }
    }
}
