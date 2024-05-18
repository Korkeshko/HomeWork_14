using UnityEngine;
using System.Threading.Tasks;

public class CubeSpawnerAsync : MonoBehaviour
{
    [SerializeField] private Cube cubePrefab;
    [SerializeField] private float count = 3;
    
    private void Awake()
    {
        Task[] tasks = new Task[(int)count];
        for (int i = 0; i < count; i++)
        {
            Cube cube = NextCube(i); 
            int randomNumber = Random.Range(1, 20);
 
            tasks[i] = cube.MoveAsyncStart(randomNumber); 
            tasks[i] = cube.ColorChangeAsyncStart();
        }
        print($"The cube from async method are over");
    }
    
    public Cube NextCube(float nextPosition)
    { 
        return Instantiate(cubePrefab, transform.position + Vector3.right * nextPosition, Quaternion.identity);
    }
}
      