using System;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    [SerializeField] protected GameObject spawnablePrefab;

    protected void Spawn()
    {
        Instantiate(spawnablePrefab);
    }

    private void Start()
    {
        GameManager.Instance.OnGameStartedEvent += Spawn;
    }
    
    private void OnDisable()
    {
        GameManager.Instance.OnGameStartedEvent -= Spawn;
    }
}