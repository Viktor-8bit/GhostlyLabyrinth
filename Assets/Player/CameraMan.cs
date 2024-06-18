using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMan : MonoBehaviour
{

    public GameObject Player;

    void Update()
    {
        int x = (int)Math.Ceiling(Player.transform.position.x);
        int y = (int)Math.Ceiling(Player.transform.position.y);

        this.transform.position = new Vector3(x, y, -10f); 
    }
}
