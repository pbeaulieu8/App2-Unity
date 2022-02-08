using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyController : MonoBehaviour
{
    void OnTriggerEnter(Collider player) 
    {
        player.gameObject.GetComponent<PlayerController>().changeNumKeys(1);
        Destroy(gameObject);
    }
}
