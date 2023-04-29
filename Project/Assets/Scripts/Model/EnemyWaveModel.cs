using QFramework;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace ApeEvolution
{
    public interface IEnemyWaveModel:IModel
    {
        int[] GetEnemyWave();
    }

    public class EnemyWaveModel : AbstractModel, IEnemyWaveModel
    {
        Queue<int[]> waveQueue;

        public int[] GetEnemyWave()
        {
            return waveQueue.Dequeue();
        }

        protected override void OnInit()
        {
            waveQueue = new Queue<int[]>();

            waveQueue.Enqueue(new int[] {1,1});
            waveQueue.Enqueue(new int[] {1,1,2});
            waveQueue.Enqueue(new int[] { 1, 1,2, 2 });
            waveQueue.Enqueue(new int[] {1,2,2,2});
            waveQueue.Enqueue(new int[] { 1, 2, 2,3 });
            waveQueue.Enqueue(new int[] { 2, 3, 3, 4, 3, 4, 5 });
            waveQueue.Enqueue(new int[] {1,1,2,2,3});
            waveQueue.Enqueue(new int[] {1,2,2,3,3});
            waveQueue.Enqueue(new int[] { 2,2, 2,2, 2 });
            waveQueue.Enqueue(new int[] { 1,1 ,1 , 1, 1 });
            waveQueue.Enqueue(new int[] {2,2,2,3,3});
            waveQueue.Enqueue(new int[] {2,2,3,3,2});
            waveQueue.Enqueue(new int[] {3,3,4,3,3});
            waveQueue.Enqueue(new int[] {3,2,2,4,4});
            waveQueue.Enqueue(new int[] {3,3,3,3,4});
            waveQueue.Enqueue(new int[] {2,2,3,3,4});
            waveQueue.Enqueue(new int[] {3,3,3,4,4});
            waveQueue.Enqueue(new int[] {4,4,3,3,4});
        }
    }
}