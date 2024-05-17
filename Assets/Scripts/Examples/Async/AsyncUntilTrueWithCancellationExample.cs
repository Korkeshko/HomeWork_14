using System.Threading;
using System;
using System.Threading.Tasks;
using UnityEngine;

namespace UntilSomethingWillBeTrue
{
    public class AsyncUntilTrueWithCancellationExample : MonoBehaviour
    {
        private CancellationTokenSource cancellationTokenSource;

        [ContextMenu(nameof(WaitUntilConditionWithCancellationStart))]
        private async void WaitUntilConditionWithCancellationStart()
        {
            cancellationTokenSource = new CancellationTokenSource();

            await WaitUntilConditionWithCancellation();
        }

        private async Task WaitUntilConditionWithCancellation()
        {
            var cancellationToken = cancellationTokenSource.Token;

            try
            {
                while (!CheckCondition())
                {
                    Debug.Log("Condition is not true, waiting...");
                    await Task.Delay(1000, cancellationToken);
                }

                Debug.Log("Condition is now true.");
            }
            catch (OperationCanceledException)
            {
                Debug.Log("Waiting operation canceled.");
            }
        }

        private static bool CheckCondition()
        {
            // Simulated condition check
            return true;
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
