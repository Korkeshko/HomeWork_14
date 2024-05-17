using System;
using System.Threading;
using System.Threading.Tasks;
using UnityEngine;

namespace WhileSomethingIsTrue
{
    public class AsyncWhileSomethingIsTrueExample : MonoBehaviour
    {
        private CancellationTokenSource cancellationTokenSource;

        [SerializeField] private bool condition = true;

        [ContextMenu(nameof(RunWhileLoopWithCancellationStart))]
        private async void RunWhileLoopWithCancellationStart()
        {
            cancellationTokenSource = new CancellationTokenSource();

            await RunWhileLoopWithCancellation();
        }

        private async Task RunWhileLoopWithCancellation()
        {
            var cancellationToken = cancellationTokenSource.Token;

            while (CheckCondition())
            {
                // Check condition
                if (condition)
                {
                    Debug.Log("Condition is true, continuing operation...");
                }
                else
                {
                    Debug.Log("Condition is false, stopping operation.");
                    break;
                }

                // Delay for 1 second
                await Task.Delay(1000, cancellationToken);
            }

            Debug.Log("While loop completed.");
        }

        private bool CheckCondition()
        {
            // Simulated condition check
            return condition;
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