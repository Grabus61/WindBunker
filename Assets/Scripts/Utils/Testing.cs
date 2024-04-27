using System.Collections;
using UnityEngine;

public class Testing : MonoBehaviour
{
    public GameObject obj;
    public void Start()
    {
        for (int i = 0; i < 5000; i++)
        {
            StartCoroutine(SpawnObj());
        }
    }

    IEnumerator SpawnObj()
    {
        Instantiate(obj);
        yield return null;
    }
}
