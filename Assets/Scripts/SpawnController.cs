using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnController : MonoBehaviour
{
    public GameObject[] objectsToSpawn;

    private void Start()
    {
        int randomIndex = Random.Range(0, objectsToSpawn.Length);
        Instantiate(objectsToSpawn[randomIndex], transform.position, Quaternion.identity);
        gameObject.SetActive(false);
    }
}
