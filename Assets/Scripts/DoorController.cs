using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorController : MonoBehaviour
{
    public string doorNum;

    bool isLocked;

    void Start() 
    {
        isLocked = true;
    }

    void OnTriggerEnter(Collider player)
    {
        if(player.gameObject.GetComponent<PlayerController>().hasKeys() && isLocked)
        {
            isLocked = false;
            player.gameObject.GetComponent<PlayerController>().changeNumKeys(-1);
        }
        if(!isLocked)
        {
            GameObject.FindWithTag("SF_Door" + doorNum).GetComponent<Animation>().Play("open");
        }
    }

    void OnTriggerExit(Collider player)
    {
        if(!isLocked)
        {
            GameObject.FindWithTag("SF_Door" + doorNum).GetComponent<Animation>().Play("close");
        }
    }
}
