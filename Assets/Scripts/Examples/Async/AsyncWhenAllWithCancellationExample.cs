using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using UnityEngine;

namespace AsyncWhenAllWithCancellationExample
{
    public class AsyncWhenAllWithCancellationExample : MonoBehaviour
    {
        private CancellationTokenSource cancellationTokenSource;

        [ContextMenu(nameof(SimulateTaskStart))]
        private async void SimulateTaskStart()
        {
            cancellationTokenSource = new CancellationTokenSource();

            var tasks = new List<Task>
            {
                SimulateTask("Task 1", 2000, cancellationTokenSource.Token),
                SimulateTask("Task 2", 3000, cancellationTokenSource.Token),
                SimulateTask("Task 3", 1500, cancellationTokenSource.Token)
            };

            try
            {
                await Task.WhenAll(tasks);
                Debug.Log("All tasks completed.");
            }
            catch (OperationCanceledException)
            {
                Debug.Log("Tasks canceled.");
            }
        }

        private static async Task SimulateTask(string taskName, int delayMs, CancellationToken cancellationToken)
        {
            Debug.Log($"Starting {taskName}");

            try
            {
                await Task.Delay(delayMs, cancellationToken);
                Debug.Log($"{taskName} completed");
            }
            catch (OperationCanceledException)
            {
                Debug.Log($"{taskName} canceled");
                throw;
            }
        }

        private void OnDestroy()
        {
            if (cancellationTokenSource != null)
            {
                cancellationTokenSource.Cancel();
                cancellationTokenSource.Dispose();
            }
        }
    }
}
