using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IObject
{
    int Id { get; }
    void AwakeAndWriteSelfId();
    void DestroyAndRemoveSelfId();
}
