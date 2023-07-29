using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckPoint : MonoBehaviour
{
    // Start is called before the first frame update

    public int checkpoint_number;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.GetComponent<PlayerController_1>() != null)
        {
            //is player get current checkpoint num
            var temp = collision.GetComponent<PlayerController_1>();

            if(temp.check_point_num == checkpoint_number)
            {
                temp.check_point_num++;
            }
        }
    }

    public void PassCheckPoint()
    {

    }
}
