using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public interface IEnemy
{
    public int Id { get; }
    int MoveCount { get; set; }
    Vector3? ChooseDirection(MapFabric MapFabric);
    void AwakeAndWriteSelf(ref MapFabric MapFabric);
}
