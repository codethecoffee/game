using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpOnEnemy : MonoBehaviour
{
    public MovePlayer mPlayer;
    public Player player;
    //maybe set rebound force in the player
    public float reboundForce = 150.0f;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "EnemyHead")
        {
            mPlayer.isGrounded = true;
            player.GetComponent<Rigidbody2D>().AddForce(transform.up * reboundForce);
            collision.gameObject.GetComponentInParent<Enemy>().LoseLife();
            
        }
    }
}
