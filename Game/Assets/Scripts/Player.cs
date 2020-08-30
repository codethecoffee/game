using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    //right now using lives but could also do HP with little change to code base
    //and HP would allow different weapon attack strengths
    private int _lives = 3;
    private int _maxLives = 5;

    //rn public bc i wanna easily see the value of it but maybe write a get method instead
    public bool invincible = false;
    private float _invincibleTimer = .4f;

    public bool isAttacking = false;
    private float _attackingTimer = .4f;

    //if a player is inactive, then the level restarts
    public bool active = true;

    public IEnumerator SetAttacking()
    {
        isAttacking = true;
        yield return new WaitForSeconds(_attackingTimer);
        isAttacking = false;
        Debug.Log("can attack again");
    }

    public IEnumerator SetInvincible()
    {
        invincible = true;
        yield return new WaitForSeconds(_invincibleTimer);
        invincible = false;
    }

    public void LoseLife()
    {
        _lives--;
        Debug.Log(_lives);
        if (_lives == 0) active = false;
    }

    public void GainLife()
    {
        if (_lives + 1 <= _maxLives) _lives++;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))        {            Debug.Log("attack attack");            StartCoroutine(SetAttacking());
        }
    }
}
