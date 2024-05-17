using System;
using System.Threading;
using System.Threading.Tasks;
using UnityEngine;

namespace AsyncWhenAnyWithCancellationExample
{
    public class AsyncWhenAnyWithCancellationExample : MonoBehaviour
    {
        private CancellationTokenSource cancellationTokenSource;

        [ContextMenu(nameof(SimulateTaskStart))]
        private async void SimulateTaskStart()
        {
            cancellationTokenSource = new CancellationTokenSource();

            var task1 = SimulateTask("Task 1", 2000, cancellationTokenSource.Token);
            var task2 = SimulateTask("Task 2", 3000, cancellationTokenSource.Token);
            var task3 = SimulateTask("Task 3", 1500, cancellationTokenSource.Token);

            var completedTask = await Task.WhenAny(task1, task2, task3);

            if (completedTask.IsFaulted)
            {
                Debug.Log("At least one task encountered an error.");
            }
            else
            {
                Debug.Log("Task completed");
            }

            CancelRemainingTasks(task1, task2, task3);
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

        private void CancelRemainingTasks(params Task[] tasks)
        {
            cancellationTokenSource.Cancel();
            foreach (var task in tasks)
            {
                if (!task.IsCompleted)
                {
                    task.ContinueWith(t => Debug.Log($"Task '{t.AsyncState}' was canceled."));
                }
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
