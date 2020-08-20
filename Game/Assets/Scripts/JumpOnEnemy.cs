using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpOnEnemy : MonoBehaviour
{
    public MovePlayer player;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (gameObject.name == "PlayerFeetCollider" 
            && collision.gameObject.tag == "EnemyHead")
        {
            Debug.Log("Jumping on Enemy");
            player.isGrounded = true;
            GameObject enemyObject = collision.gameObject;
            // Destroy the enemy
            Destroy(collision.transform.root.gameObject);
        }
    }
}
