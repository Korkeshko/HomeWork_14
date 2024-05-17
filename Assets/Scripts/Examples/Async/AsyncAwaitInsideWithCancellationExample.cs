using System;
using System.Threading;
using System.Threading.Tasks;
using UnityEngine;

namespace AsyncAwaitInsideWithCancellationExample
{
    public class AsyncAwaitInsideWithCancellationExample : MonoBehaviour
    {
        private CancellationTokenSource cancellationTokenSource;

        [ContextMenu(nameof(DoAsyncWorkWithCancellationStart))]
        private async void DoAsyncWorkWithCancellationStart()
        {
            cancellationTokenSource = new CancellationTokenSource();

            try
            {
                await DoAsyncWorkWithCancellation(cancellationTokenSource.Token);
                Debug.Log("Async operation completed.");
            }
            catch (OperationCanceledException)
            {
                Debug.Log("Async operation canceled.");
            }
        }

        private static async Task DoAsyncWorkWithCancellation(CancellationToken cancellationToken)
        {
            Debug.Log("Starting async work.");

            // Start another asynchronous operation inside
            await Task.Run(async () =>
            {
                Debug.Log("Inside another async task.");

                // Simulate work
                await Task.Delay(2000);

                Debug.Log("Inside task completed.");
            });

            // Simulate more work after the inner task
            await Task.Delay(1000);

            // Check for cancellation
            cancellationToken.ThrowIfCancellationRequested();

            Debug.Log("Async work completed.");
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
