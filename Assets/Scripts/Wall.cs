using System;
using UnityEngine;

public class Wall : MonoBehaviour
{
    public static event Action OnPlayerHitWallEvent;
    
    private readonly Vector3 _moveDirection = Vector2.left;
    private float _moveSpeed;

    private void Start()
    {
        _moveSpeed = GameManager.Instance.StartPlayerMoveSpeed;
    }

    private void Update()
    {
        var currentPosition = transform.position;
        
        currentPosition = Vector2.MoveTowards(
            currentPosition,
            currentPosition + _moveDirection,
            _moveSpeed * Time.deltaTime
        );
        transform.position = currentPosition;
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        var player = col.GetComponent<Player>();
        if (player)
        {
            OnPlayerHitWallEvent?.Invoke();
        }

        var wallStopper = col.GetComponent<WallStopper>();
        if (wallStopper)
        {
            wallStopper.UpdatePositionThisWall(this);
        }
    }
}
