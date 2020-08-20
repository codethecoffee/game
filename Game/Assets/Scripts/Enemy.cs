using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    //default 2 lives
    private int _lives = 2;

    private void Update()
    {
        if (_lives <= 0)
        {
            EnemyDeath();
        }
    }

    public void LoseLife()
    {
        _lives--;
    }

    public void EnemyDeath()
    {
        Destroy(gameObject);
    }
}
