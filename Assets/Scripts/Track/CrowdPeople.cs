using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrowdPeople : MonoBehaviour
{

    public List<GameObject> crowd_members = new List<GameObject>();
    public float cheer_time = 1.0f;
    private bool is_cheering = false;

    // Start is called before the first frame update
    void Start()
    {
        for(int i = 0; i != transform.childCount; i++)
        {
            crowd_members.Add(transform.GetChild(i).gameObject);
        }
    }

    public void TiggerCheer()
    {
        is_cheering = true;
    }

    private void Update()
    {
        updateCheer();
    }

    private void updateCheer()
    {

    }
}
