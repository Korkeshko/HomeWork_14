using System;
using System.Collections;
using UnityEngine;
using DG.Tweening;
using System.Threading;
using System.Threading.Tasks;

public class Cube : MonoBehaviour
{
    [SerializeField]
    private float finalPositionZ = 20f;
    private new Rigidbody rigidbody;
    private CancellationTokenSource cancellationTokenSource;
    
    private void Awake()
    {
        rigidbody = GetComponent<Rigidbody>();
    }

    #region Coroutine
    public IEnumerator MoveCoroutine(float duration)
    {
        yield return rigidbody.transform.DOMoveZ(finalPositionZ, duration)!.WaitForCompletion();
    }

    public IEnumerator ColorChangeCoroutine()
    {
        while (true)
        {
           Color color = new (UnityEngine.Random.value, UnityEngine.Random.value, UnityEngine.Random.value, 1);
           rigidbody.GetComponent<Renderer>().material.color = color;
                                                                            
            yield return new WaitForSeconds(UnityEngine.Random.Range(3, 5));
        }
    }
    #endregion

    #region Async
    public async Task MoveAsyncStart(float duration)
    {
        cancellationTokenSource = new CancellationTokenSource();

        try
        {
            CancellationToken cancellationToken = cancellationTokenSource.Token;
            
            await rigidbody.transform.DOMoveZ(finalPositionZ, duration).AsyncWaitForCompletion();       
            cancellationToken.ThrowIfCancellationRequested();
        }
        catch (OperationCanceledException)
        {
            Debug.Log("MoveAsync canceled.");
        }
    }

    public async Task ColorChangeAsyncStart()
    {
        cancellationTokenSource = new CancellationTokenSource();

        try
        {
            CancellationToken cancellationToken = cancellationTokenSource.Token;

            while (true)
            {
                Color color = new (UnityEngine.Random.value, UnityEngine.Random.value, UnityEngine.Random.value, 1);
                rigidbody.GetComponent<Renderer>().material.color = color;
                                                                                
                await Task.Delay(UnityEngine.Random.Range(3000, 5000));
            }
        }
        catch (OperationCanceledException)
        {
            Debug.Log("ColorChangeAsync canceled.");
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
    #endregion

    #region CoroutineWithAsync

    #endregion
}
