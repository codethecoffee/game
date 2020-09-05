using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TouchSpike : MonoBehaviour
{
    public float reboundForce = 150.0f;
    public int damage;

    //look more into the spikes & how to apply force for the upwards bump

    private void OnCollisionEnter2D(Collision2D collision)
    {

        if (collision.gameObject.tag == "Player")
        {

            if (collision.gameObject.name == "PlayerFeetCollider")
            {
                Player p = collision.gameObject.GetComponentInParent<Player>();
                MovePlayer mp = collision.gameObject.GetComponentInParent<MovePlayer>();
                MoveAnimation ma = collision.gameObject.GetComponent<MoveAnimation>();
                mp.isGrounded = true;
                if (!p.invincible)
                {
                    StartCoroutine(ma.playHurtAnimation());
                    StartCoroutine(p.SetInvincible());
                    p.LoseHP(damage);
                }
                collision.transform.parent.GetComponent<Rigidbody2D>().AddForce(transform.up * reboundForce);
            } else
            {
                Player p = collision.gameObject.GetComponent<Player>();
                MovePlayer mp = collision.gameObject.GetComponent<MovePlayer>();
                MoveAnimation ma = collision.gameObject.GetComponent<MoveAnimation>();
                mp.isGrounded = true;
                if (!p.invincible)
                {
                    StartCoroutine(ma.playHurtAnimation());
                    StartCoroutine(p.SetInvincible());
                    p.LoseHP(damage);
                }
                collision.rigidbody.AddForce(transform.up * reboundForce);
            }
           
        }
    }
}
