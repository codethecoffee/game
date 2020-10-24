using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public int speed;
    private Rigidbody2D rb;

    private int _maxhp = 100;
    private int _hp = 100;

    private float _hurtTimer = .06f;
    private Animator _animator;

    public Transform healthBar;
    private float _maxHealthBarLength;
    private float _healthBarLength;

    public Player player;

    bool rightFacing;

    void Start()
    {
        _animator = gameObject.GetComponent<Animator>();
        _maxHealthBarLength = healthBar.localScale.x;

        _animator.SetBool("isWalking", true);

        rb = gameObject.GetComponent<Rigidbody2D>();
        rb.velocity = new Vector2(speed, 0f);
        rightFacing = speed > 0 ? true : false;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag != "Player")
        {
            speed *= -1;
            rightFacing = !rightFacing;
            rb.velocity = new Vector2(speed, 0f);
            GetComponent<SpriteRenderer>().flipX = !rightFacing ? false : true;
        }
    }

    public IEnumerator playHurtAnimation()
    {
        _animator.SetBool("isHurt", true);
        yield return new WaitForSeconds(_hurtTimer);
        _animator.SetBool("isHurt", false);
    }

    public void LoseHP(int hpLoss)
    {
        StartCoroutine(playHurtAnimation());
        _healthBarLength = ((float)(_hp - hpLoss) / _maxhp) * _maxHealthBarLength;
        healthBar.localScale = new Vector3(
            _healthBarLength, 
            healthBar.localScale.y, 
            healthBar.localScale.z);
        _hp -= hpLoss;


        if (_hp <= 0)
        {
            EnemyDeath();
        }

    }

    public void EnemyDeath()
    {
        Destroy(gameObject);
        player.UpdateScoreOnEnemyKill();
    }
}
