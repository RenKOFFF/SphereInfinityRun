using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallStopper : MonoBehaviour
{
    [SerializeField] private float _wallOffset = 17; 
    
    public void UpdatePositionThisWall(Wall wall)
    {
        wall.transform.position = new Vector3(
            wall.transform.position.x + Vector2.right.x * _wallOffset,
            wall.transform.position.y + Vector2.right.y * _wallOffset);
        
    }
}
