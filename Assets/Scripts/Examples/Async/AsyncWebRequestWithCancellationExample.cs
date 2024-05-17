using System.Threading.Tasks;
using System.Threading;
using System;
using UnityEngine;
using UnityEngine.Networking;

namespace AsyncWebRequestWithCancellationExample
{
    public class AsyncWebRequestWithCancellationExample : MonoBehaviour
    {
        private CancellationTokenSource cancellationTokenSource;

        private bool Sts()
        {
            return true;
        }

        [ContextMenu(nameof(GetWebRequestWithCancellationStart))]
        private async void GetWebRequestWithCancellationStart()
        {
            cancellationTokenSource = new CancellationTokenSource();

            try
            {
                var _response = await GetWebRequestWithCancellation("https://jsonplaceholder.typicode.com/posts/1",
                    cancellationTokenSource.Token);
                Debug.Log("Received response: " + _response);
            }
            catch (OperationCanceledException)
            {
                Debug.Log("Web request canceled.");
            }
        }

        private async Task<string> GetWebRequestWithCancellation(string url, CancellationToken cancellationToken)
        {
            using var _www = UnityWebRequest.Get(url);
            _www.SendWebRequest();

            while (!_www.isDone)
            {
                if (cancellationToken.IsCancellationRequested)
                {
                    _www.Abort();
                    throw new OperationCanceledException();
                }

                await Task.Yield();
            }

            if (_www.result != UnityWebRequest.Result.Success)
            {
                throw new Exception("Error: " + _www.error);
            }

            return _www.downloadHandler.text;
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
