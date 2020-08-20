using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TouchSpike : MonoBehaviour
{
    public float reboundForce = 150.0f;

    //look more into the spikes & how to apply force for the upwards bump

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log(collision.gameObject.name);
        // uhh rethink how to detect player vs player's feet

        if (collision.gameObject.tag == "Player")
        {

            if (collision.gameObject.name == "PlayerFeetCollider")
            {
                Player p = collision.gameObject.GetComponentInParent<Player>();
                MovePlayer mp = collision.gameObject.GetComponentInParent<MovePlayer>();
                mp.isGrounded = true;
                if (!p.invincible)
                {
                    StartCoroutine(p.SetInvincible());
                    p.LoseLife();
                }
                collision.transform.parent.GetComponent<Rigidbody2D>().AddForce(transform.up * reboundForce);
            } else
            {
                Player p = collision.gameObject.GetComponent<Player>();
                MovePlayer mp = collision.gameObject.GetComponent<MovePlayer>();
                mp.isGrounded = true;
                if (!p.invincible)
                {
                    StartCoroutine(p.SetInvincible());
                    p.LoseLife();
                }
                collision.rigidbody.AddForce(transform.up * reboundForce);
            }
           
        }
    }
}
