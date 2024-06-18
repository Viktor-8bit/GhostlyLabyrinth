
using UnityEngine;
using System.Threading.Tasks;
using System.IO.MemoryMappedFiles;
using Assets.CustomDebug;
using AStar;
using System;
using System.Diagnostics;

public class DebugLogger : MonoBehaviour
{

    public IDebugLogger DebLogger;

    public int _size = 0;

    private int[,] _data;

    void Awake()
    {
        _data = new int[_size, _size];
        DebLogger = this.gameObject.GetComponent<IDebugLogger>();
        if (DebLogger == null)
            throw new Exception("«ачем ты ставишь debugger, если не весишь логер ??");

    }

    public void MakeSliceAndSend(Coardinates coardinates)
    {
        for (int i = _size - 1; i >= 0; i--)
        {
            for (int j = 0; j < _size; j++)
            {
                int value = coardinates.GetValue(y: i, x: j);
                _data[i, j] = value;
            }
        }

        DebLogger.DebugLog(_data);
    }

}
