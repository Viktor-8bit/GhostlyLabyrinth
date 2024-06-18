using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using Random = UnityEngine.Random;
using AStar;

public class Amogus : MonoBehaviour, IEnemy
{
    public int MoveCount { get; set; } = 1;

    public int id;
    public int Id
    {
        get
        {
            return this.id;
        }
    }

    void IEnemy.AwakeAndWriteSelf(ref MapFabric mapFabric)
    {
        int x = (int)Math.Ceiling(this.transform.position.x), 
            y = (int)Math.Ceiling(this.transform.position.y);
        mapFabric!.SetId(new Point() { Id = this.Id, x = x, y = y });
    }


    Vector3? IEnemy.ChooseDirection(MapFabric mapFabric)
    {
        Vector3? tempVector3 = new Vector3();
        List<Vector3> moves = new List<Vector3>();

        int x = (int)Math.Ceiling(this.transform.position.x), y = (int)Math.Ceiling(this.transform.position.y);

        // right
        int id = mapFabric.GetId(x: x + 1, y: y );
        if (id is 0 or 6)
            moves.Add(new Vector3(x + 1, y));

        // left
        id = mapFabric.GetId(x: x - 1, y: y);
        if (id is 0 or 6)
            moves.Add(new Vector3(x - 1, y));

        // down
        id = mapFabric.GetId(x: x, y: y - 1);
        if (id is 0 or 6)
            moves.Add(new Vector3(x, y - 1));

        // up
        id = mapFabric.GetId(x: x, y: y + 1);
        if (id is 0 or 6)
            moves.Add(new Vector3(x, y + 1));

        if (moves.Count > 0)
            tempVector3 = moves[Random.Range(0, moves.Count)];
        else
            tempVector3 = null;
        
        return tempVector3;
    }

}
