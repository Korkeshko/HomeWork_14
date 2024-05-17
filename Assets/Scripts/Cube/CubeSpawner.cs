using UnityEngine;
using System.Collections;

public class CubeSpawner : MonoBehaviour
{
    [SerializeField] private Cube cubePrefab;
    [SerializeField] private float count = 3;
    //private new Renderer renderer;
    
    private IEnumerator Start()
    {  
        
        for (int i = 0; i < count; i++)
        {
            Cube cube = NextCube(i); 
            int randomNumber = Random.Range(1, 20);
            //Debug.Log($"The cube number {i} is {randomNumber} duration");
            StartCoroutine(cube.Transfer(randomNumber)); 
            StartCoroutine(cube.ColorChange());   
            yield return null;
        }
        print($"The cube are over");
    }
    
    public Cube NextCube(float nextPosition)
    { 
        //cubePrefab.GetComponent<Renderer>().renderer.material.color = new Color(Random.value, Random.value, Random.value, 1);
        return Instantiate(cubePrefab, transform.position + Vector3.right * nextPosition, Quaternion.identity);
    }
}
      