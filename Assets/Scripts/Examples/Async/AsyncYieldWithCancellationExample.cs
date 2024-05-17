using System;
using System.Threading;
using System.Threading.Tasks;
using UnityEngine;

namespace AsyncYieldWithCancellationExample
{
    public class AsyncYieldWithCancellationExample : MonoBehaviour
    {
        private CancellationTokenSource cancellationTokenSource;

        [ContextMenu(nameof(DoAsyncWorkWithYieldAndCancellationStart))]
        private async void DoAsyncWorkWithYieldAndCancellationStart()
        {
            cancellationTokenSource = new CancellationTokenSource();

            try
            {
                await DoAsyncWorkWithYieldAndCancellation(cancellationTokenSource.Token);
                Debug.Log("Async operation completed.");
            }
            catch (OperationCanceledException)
            {
                Debug.Log("Async operation canceled.");
            }
        }

        private static async Task DoAsyncWorkWithYieldAndCancellation(CancellationToken cancellationToken)
        {
            for (var _i = 0; _i < 10; _i++)
            {
                Debug.Log($"Iteration {_i}");

                // Yield to skip a frame
                await YieldFrame(cancellationToken);

                if (cancellationToken.IsCancellationRequested)
                {
                    throw new OperationCanceledException();
                }
            }
        }

        private static async Task YieldFrame(CancellationToken cancellationToken)
        {
            await Task.Yield();
            // Check for cancellation before yielding the frame
            cancellationToken.ThrowIfCancellationRequested();
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
