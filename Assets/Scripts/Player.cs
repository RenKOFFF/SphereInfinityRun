using System;
using UnityEngine;

public class Player : MonoBehaviour
{
    public static event Action OnPlayerDiedEvent;

    private float Speed => GameManager.Instance.PlayerMoveSpeed;

    private float MaxOffset => GameManager.Instance.MaxPlayerOffset;

    void Update()
    {
        MoveTo(GetMoveDirection());
    }

    private Vector2 GetMoveDirection()
    {
        return Input.GetKey(KeyCode.UpArrow) ? Vector2.up : Vector2.down;
    }

    private void MoveTo(Vector2 direction)
    {
        transform.position = Vector2.MoveTowards(
            transform.position, 
            direction * MaxOffset, 
            Speed * Time.deltaTime);
    }
    
    private void OnPlayerHitWall()
    {
        Die();
    }

    private void Die()
    {
        OnPlayerDiedEvent?.Invoke();
        Destroy(gameObject);
    }
    
    private void OnEnable()
    {
        Wall.OnPlayerHitWallEvent += OnPlayerHitWall;
    }

    private void OnDisable()
    {
        Wall.OnPlayerHitWallEvent -= OnPlayerHitWall;
    }
}
