using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Cube : MonoBehaviour
{
    
    [SerializeField]
    private float maxDistance = 25f;
    private new Rigidbody rigidbody;
    
    private void Awake()
    {
        rigidbody = GetComponent<Rigidbody>();
    }

    public IEnumerator Transfer(float duration)
    {
        yield return rigidbody.transform.DOMoveZ(maxDistance, duration)!.WaitForCompletion();
    }

    public IEnumerator ColorChange()
    {
        while (true)
        {
            rigidbody.GetComponent<Renderer>().material.color = new Color(Random.value, Random.value, Random.value, 1);
            yield return new WaitForSeconds(Random.Range(3, 5));
        }
    }
}
