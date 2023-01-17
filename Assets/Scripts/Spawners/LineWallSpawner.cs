using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Spawners
{
    public class LineWallSpawner : Spawner
    {
        private float SpawnOffset => GameManager.Instance.MaxPlayerOffset;
        private float _wallSize;
        [SerializeField] private float _startSpawnPositionX = 15;
        private Vector2 _startSpawnPosition;
        private List<Wall> Walls = new();
        
        private void Start()
        {
            GameManager.Instance.OnGameStartedEvent += OnGameStarted;
        }
    
        private void OnDisable()
        {
            GameManager.Instance.OnGameStartedEvent -= OnGameStarted;
        }

        private void OnGameStarted()
        {
            if (Walls.Count > 0)
            {
                foreach (var wall in Walls)
                {
                    Destroy(wall.gameObject);
                }
                Walls.Clear();
            }
            
            _startSpawnPosition = Vector2.right * _startSpawnPositionX;
            _wallSize = spawnablePrefab.GetComponent<SpriteRenderer>().size.y;
            StartCoroutine(SpawnWalls());
        }
        
        private IEnumerator SpawnWalls()
        {
            for (int i = 1; i <= 3; i++)
            {
                var wall = Instantiate(
                        spawnablePrefab,
                        Random.Range(-(SpawnOffset - _wallSize / 2), SpawnOffset - _wallSize / 2) * Vector2.up +
                        _startSpawnPosition,
                        Quaternion.identity).GetComponent<Wall>();
                Walls.Add(wall);
                    
                yield return new WaitWhile(() => wall != null && wall.transform.position.x >= Vector2.zero.x);
                
            }
        }
    }
}
