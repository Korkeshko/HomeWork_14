using UnityEngine;
using System.Collections;

public class CubeSpawnerCoroutine : MonoBehaviour
{
    [SerializeField] private Cube cubePrefab;
    [SerializeField] private float count = 3;
    
    private IEnumerator Start()
    {   
        for (int i = 0; i < count; i++)
        {
            Cube cube = NextCube(i); 
            int randomNumber = Random.Range(1, 20);
            
            StartCoroutine(cube.MoveCoroutine(randomNumber)); 
            StartCoroutine(cube.ColorChangeCoroutine());   
            yield return null;
        }
        print($"The cube from coroutine method are over");
    }
    
    public Cube NextCube(float nextPosition)
    { 
        return Instantiate(cubePrefab, transform.position + Vector3.right * nextPosition, Quaternion.identity);
    }
}
      