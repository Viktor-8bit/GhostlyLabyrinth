using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using UnityEngine;
using AStar;
using UnityEditor;
using UnityEditor.Rendering;
using Vector3 = UnityEngine.Vector3;

public class ThreeXAmogus : MonoBehaviour, IEnemy
{
    public int MoveCount { get; set; } = 3;

    public int id;
    public int Id
    {
        get
        {
            return this.id;
        }
    }

    private int _index = 0;
    private bool _haveIWay = true;

    void IEnemy.AwakeAndWriteSelf(ref MapFabric mapFabric)
    {
        int x = (int)Math.Ceiling(this.transform.position.x), 
            y = (int)Math.Ceiling(this.transform.position.y);
        mapFabric!.SetId(new Point() { Id = this.Id, x = x, y = y });
    }


    // TODO что это за ужас ?
    private Vector3[] _tempVector3;
    public Vector3? ChooseDirection(MapFabric mapFabric)
    {

        if (_index >= MoveCount || !_haveIWay)
        {
            _haveIWay = true;
            _index = 0;
        }

        if (_index == 0)
        {
            _tempVector3 = new Vector3[MoveCount];
            _haveIWay = true;

            int x = (int)Math.Ceiling(this.transform.position.x),
                y = (int)Math.Ceiling(this.transform.position.y);

            Coardinates countMap = mapFabric.CalculatedCoardinates;

            for (int i = 0; i < MoveCount; i++)
            {
                int left;
                int right;
                int up;
                int down;

                if (i == 0)
                {
                    left  =  countMap.GetValue(x: x - 1, y: y);
                    right =  countMap.GetValue(x: x + 1, y: y);
                    up    =  countMap.GetValue(x: x,     y: y + 1);
                    down  =  countMap.GetValue(x: x,     y: y - 1);
                }
                else
                {
                    x = (int)_tempVector3[i - 1].x;
                    y = (int)_tempVector3[i - 1].y;

                    left  = countMap.GetValue(x: x - 1, y: y);
                    right = countMap.GetValue(x: x + 1, y: y);
                    up    = countMap.GetValue(x: x,     y: y + 1);
                    down  = countMap.GetValue(x: x,     y: y - 1);
                }

                List<(int, char)> waysTuple = new List<(int, char)>();

                if (left > 0) 
                    waysTuple.Add((left,  'l'));
                if (right > 0)
                    waysTuple.Add((right, 'r'));
                if (up > 0)
                    waysTuple.Add((up,    'u'));
                if (down > 0)
                    waysTuple.Add((down,  'd'));


                if (waysTuple.Count == 0)
                {
                    _haveIWay = false;
                    return null;
                }

                char? optimal = waysTuple
                    .FirstOrDefault(wT => 
                        wT.Item1 == (waysTuple.Min(wT1 => wT1.Item1)
                    )
                ).Item2;

                switch (optimal)
                {
                    case 'l':
                        _tempVector3[i] = new Vector3(x - 1, y);
                        break;
                    case 'r':
                        _tempVector3[i] = new Vector3(x + 1, y);
                        break;
                    case 'u':
                        _tempVector3[i] = new Vector3(x, y + 1);
                        break;
                    case 'd':
                        _tempVector3[i] = new Vector3(x, y - 1);
                        break;
                    default:
                        return null;
                }

            }
            _index++;
        }
        else
        {
            _index++;
        }

        return _tempVector3[_index - 1];
    }
}
