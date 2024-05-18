using UnityEngine;
using System.Collections;
using System.Threading.Tasks;

public class CubeSpawnerCoroutineWithAsync : MonoBehaviour
{
    [SerializeField] private Cube cubePrefab;
    [SerializeField] private float count = 3;
    
    private IEnumerator Start()
    {   
        Task[] tasks = new Task[(int)count];
        for (int i = 0; i < count; i++)
        {
            Cube cube = NextCube(i); 
            cube.GetComponent<Rigidbody>().useGravity = false;
            cube.transform.localScale = new Vector3(0.2f, 0.2f, 0.2f);
            int randomNumber = Random.Range(1, 20);
            
            tasks[i] = cube.MoveAsyncStart(randomNumber); 
            tasks[i] = cube.ColorChangeAsyncStart(); 
            yield return null;
        }
        print($"The cube from coroutine with async method are over");
    }
    
    public Cube NextCube(float nextPosition)
    { 
        return Instantiate(cubePrefab, transform.position + Vector3.right * nextPosition, Quaternion.identity);
    }
}
      