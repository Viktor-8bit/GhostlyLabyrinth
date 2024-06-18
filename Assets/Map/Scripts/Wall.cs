using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using AStar;

public class Wall : MonoBehaviour, IObject
{
    
    private MapFabric MapFabric;
    public bool IDebug = false;

    public int id;
    [SerializeField]
    public int Id {
        get
        {
            return this.id;
        }
    }

    void Awake()
    {
        MapFabric = GameObject.Find("MapFabric").GetComponent<MapFabric>();
        if (MapFabric == null)
        {
            throw new Exception("где MapFabric на сцене ?");
        }
        else
        {
            this.transform.position = new Vector2((int)transform.position.x, (int)transform.position.y);

            AwakeAndWriteSelfId();
        }
    }

    public void AwakeAndWriteSelfId()
    {
        int x = (int)Math.Ceiling(this.transform.position.x);
        int y = (int)Math.Ceiling(this.transform.position.y);
        Point point = new Point() { Id = this.Id, x = x, y = y };

        if (IDebug)
            Debug.Log($"{point}, " +
                      $"\n Position x: {this.transform.position.x} y: {this.transform.position.y}" +
                      $"\n Math.Ceiling x: {(int)Math.Ceiling(this.transform.position.x)} y: {(int)Math.Ceiling(this.transform.position.y)}");

        MapFabric!.SetId(point);
    }

    public void DestroyAndRemoveSelfId()
    {
        throw new Exception("почему стену разрушают ?");
    }


}
