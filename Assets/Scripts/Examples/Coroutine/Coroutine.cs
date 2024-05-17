using UnityEngine;
using System.Collections;
using UnityEngine.Networking;


public class Coroutine : MonoBehaviour
{

    #region TimeScale

    [ContextMenu(nameof(ScaleTimeStart))]
    private void ScaleTimeStart()
    {
        StartCoroutine(ScaleTime());
    }

    private static IEnumerator ScaleTime()
    {
        Debug.Log("Time.timeScale before: " + Time.timeScale);

        // Slow down time
        Time.timeScale = 0.5f;
        yield return new WaitForSeconds(2.0f);

        Debug.Log("Time.timeScale after: " + Time.timeScale);

        // Reset time back to normal
        Time.timeScale = 1.0f;
    }

    #endregion

    #region Unscaled

    [ContextMenu(nameof(CountTimeStart))]
    private void CountTimeStart()
    {
        StartCoroutine(CountTime());
    }

    private static IEnumerator CountTime()
    {
        var _startTime = Time.unscaledTime;
        var _elapsedTime = 0f;

        while (_elapsedTime < 5f)
        {
            _elapsedTime = Time.unscaledTime - _startTime;
            Debug.Log("Elapsed Time (unscaled): " + _elapsedTime);
            yield return null;
        }

        Debug.Log("Countdown finished!");
    }

    #endregion

    #region While something is true

    [ContextMenu(nameof(CheckConditionStart))]
    private void CheckConditionStart()
    {
        StartCoroutine(CheckCondition());
    }

    [SerializeField, Tooltip("CheckCondition")]
    private bool checkConditionCondition = true;
    private IEnumerator CheckCondition()
    {
        while (checkConditionCondition)
        {
            Debug.Log("Condition is true!");
            yield return new WaitForSeconds(1.0f);
        }

        Debug.Log("Condition is now false, exiting coroutine.");
    }

    #endregion

    #region CheckUntilCondition

    [ContextMenu(nameof(CheckUntilConditionStart))]
    private void CheckUntilConditionStart()
    {
        StartCoroutine(CheckUntilCondition());
    }

    [Space]
    [SerializeField, Tooltip("CheckUntilCondition")]
    private bool checkUntilCondition;
    private IEnumerator CheckUntilCondition()
    {
        do
        {
            Debug.Log("Waiting for condition to be true...");
            yield return new WaitForSeconds(1.0f);
        } while (!checkUntilCondition);

        Debug.Log("Condition is now true, exiting coroutine.");
    }

    #endregion

    #region GetDataFromServer

    [ContextMenu(nameof(GetDataFromServerStart))]
    private void GetDataFromServerStart()
    {
        StartCoroutine(GetDataFromServer());
    }

    private static IEnumerator GetDataFromServer()
    {
        var www = UnityWebRequest.Get("https://jsonplaceholder.typicode.com/posts/1");

        yield return www.SendWebRequest();

        if (www.result != UnityWebRequest.Result.Success)
        {
            Debug.Log("Error: " + www.error);
        }
        else
        {
            Debug.Log("Received: " + www.downloadHandler.text);
        }
    }

    #endregion

    #region SkipFrame

    [ContextMenu(nameof(SkipFrameStart))]
    private void SkipFrameStart()
    {
        StartCoroutine(nameof(SkipFrame));
    }

    private IEnumerator SkipFrame()
    {
        Debug.Log("Before skipping frame");

        yield return null; // Skip the frame

        Debug.Log("After skipping frame");
    }

    #endregion

    #region Another coroutine inside

    [ContextMenu(nameof(OuterCoroutineStart))]
    private void OuterCoroutineStart()
    {
        StartCoroutine(OuterCoroutine());
    }

    private IEnumerator OuterCoroutine()
    {
        Debug.Log("Outer coroutine started");

        StartCoroutine(OuterCoroutine2());
        yield return StartCoroutine(InnerCoroutine());

        Debug.Log("Outer coroutine completed");
    }

    private static IEnumerator InnerCoroutine()
    {
        Debug.Log("Inner coroutine started");
        yield return new WaitForSeconds(2);
        Debug.Log("Inner coroutine completed");
    }

    private static IEnumerator OuterCoroutine2()
    {
        Debug.LogWarning("Outlaw coroutine started!");
        yield return new WaitForEndOfFrame();
        Debug.LogWarning("Outlaw coroutine completed!");
    }

    #endregion

}
