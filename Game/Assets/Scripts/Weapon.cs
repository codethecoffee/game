using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    public Player player;
    //weapon is inRange for attack
    private bool _inRange = false;
    private Enemy _enemy;

    public void HitEnemy(Enemy enemy)
    {
        //play weapon animation
        //play sound effect
        enemy.LoseLife();
    }

    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.gameObject.tag == "Enemy" || collider.gameObject.tag == "EnemyHead")
        {
            _inRange = true;
            if (collider.gameObject.GetComponent<Enemy>())
            {
                _enemy = collider.gameObject.GetComponent<Enemy>();
            }
            else
            {
                _enemy = collider.gameObject.GetComponentInParent<Enemy>();
            }
                
        }
    }

    private void OnTriggerExit2D(Collider2D collider)
    {
        if (collider.gameObject.tag == "Enemy" || collider.gameObject.tag == "EnemyHead")
        {
            _inRange = false;
            _enemy = null;
        }
    }

    void Update()
    {
        if (_inRange && player.isAttacking)
        {
            player.isAttacking = false;
           HitEnemy(_enemy);  
        }
    }
}
