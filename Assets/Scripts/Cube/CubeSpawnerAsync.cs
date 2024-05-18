using UnityEngine;
using System.Collections;

public class CubeSpawnerAsync : MonoBehaviour
{
    [SerializeField] private Cube cubePrefab;
    [SerializeField] private float count = 3;
    
    private async void Awake()
    {
        for (int i = 0; i < count; i++)
        {
            Cube cube = NextCube(i); 
            int randomNumber = Random.Range(1, 20);
            await cube.MoveAsyncStart(randomNumber);
            await cube.ColorChangeAsyncStart();
        }
        print($"The cube from async method are over");
    }
    
    public Cube NextCube(float nextPosition)
    { 
        return Instantiate(cubePrefab, transform.position + Vector3.right * nextPosition, Quaternion.identity);
    }
}
      