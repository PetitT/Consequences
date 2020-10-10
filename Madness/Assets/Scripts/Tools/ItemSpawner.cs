using LowTeeGames;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemSpawner : MonoBehaviour
{
    public GameObject itemToSpawn;

    public void Spawn()
    {
        LTPool.Instance.GetItemFromPool(itemToSpawn, transform.position, Quaternion.identity);
    }
}
