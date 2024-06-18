


using System.IO.MemoryMappedFiles;
using UnityEngine;


namespace Assets.CustomDebug
{
    public class Writer : MonoBehaviour, IDebugLogger
    {

        public void DebugLog(int[,] data)
        {
            Writer.WriteToSharedMemory(data);
        }

        // пишем в общую память с дебаггером 
        public static void WriteToSharedMemory(int[,] data)
        {
            // размер int * на кол элеменов массива
            int size = sizeof(int) * data.Length;
            MemoryMappedFile sharedMemory = MemoryMappedFile.CreateOrOpen("Mass512", size + sizeof(int));

            // шаг смещения между слоями двумерного массива
            int step = data.GetLength(0) * sizeof(int);
            int position = sizeof(int);

            using (MemoryMappedViewAccessor writer = sharedMemory.CreateViewAccessor(0, size + sizeof(int)))
            {
                // пишем размер int[,] массива
                writer.Write(0, data.GetLength(0));
                for (int i = data.GetLength(0) - 1; i >= 0; i--)
                {
                    int[] tempMass = new int[data.GetLength(1)];
                    for (int j = 0; j < data.GetLength(1); j++)
                    {
                        tempMass[j] = data[i, j];
                    }
                    writer.WriteArray<int>(position, tempMass, 0, tempMass.Length);
                    position += step;
                }
            }
        }
    }
}
