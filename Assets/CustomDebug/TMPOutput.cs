using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TMPOutput : MonoBehaviour, IDebugLogger
{
    public TMP_Text textMeshPro;

    static Dictionary<int, string> idDB = new Dictionary<int, string>()
    {
        {0, "."},   // пустое место
        {10, "|"},  // стена
        {-1, "a"},  // противник
        {-2, "3"},  // 3X-amogus
        {6, "p"},   // игрок
        {12, "w"},  // гоооол
    };

    public void DebugLog(int[,] data)
    {
        int _id = 0;
        string debugString = "";
        for (int i = data.GetLength(0) - 1; i >= 0; i--)
        {
            for (int j = 0; j < data.GetLength(0); j++)
            {
                _id = data[i, j];

                //if (_id == 0)
                //    debugString += " || ";
                //else if (_id < 10)
                //    debugString += $" _{_id} ";
                //else
                //    debugString += $" {_id} ";

                if (idDB.ContainsKey(_id))
                    debugString += $"{idDB[_id]}";

            }
            debugString += "\n";
        }
        textMeshPro.text = debugString;
    }


}
