using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public int speed;
    //if not horizontal moving, will be vertical moving
    public bool horzMoving;
    public float upperBoundary;
    public float lowerBoundary;
    private Rigidbody2D rb;

    private int _maxhp = 100;
    private int _hp = 100;

    private float _hurtTimer = .06f;
    private Animator _animator;

    public Transform healthBar;
    private float _maxHealthBarLength;
    private float _healthBarLength;

    bool rightFacing;

    void Start()
    {
        _animator = gameObject.GetComponent<Animator>();
        _maxHealthBarLength = healthBar.localScale.x;

        _animator.SetBool("isWalking", true);

        rb = gameObject.GetComponent<Rigidbody2D>();
        rb.velocity = horzMoving ? new Vector2(speed, 0) : new Vector2(0, speed);
        rightFacing = speed > 0 ? true : false;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag != "Player")
        {
            speed *= -1;
            rightFacing = !rightFacing;
            rb.velocity = horzMoving ? new Vector2(speed, 0) : new Vector2(0, speed);
            if (horzMoving){
                GetComponent<SpriteRenderer>().flipX = !rightFacing ? false : true;
            }
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
    }

    void Update(){
        if (horzMoving){
            //check boundaries for horizontal movement
            if (transform.position.x > upperBoundary){
                speed *= -1;
                rightFacing = !rightFacing;
                rb.velocity = new Vector2(speed, 0);
                GetComponent<SpriteRenderer>().flipX = !rightFacing ? false : true;
            } else if (transform.position.x < lowerBoundary){
                speed *= -1;
                rightFacing = !rightFacing;
                rb.velocity = new Vector2(speed, 0);
                GetComponent<SpriteRenderer>().flipX = !rightFacing ? false : true;
            }       
        } else {
            //check boundaries for vertical movement
            if (transform.position.y > upperBoundary){
                speed *= -1;
                rightFacing = !rightFacing;
                rb.velocity = new Vector2(0, speed);
            } else if (transform.position.y < lowerBoundary){
                speed *= -1;
                rightFacing = !rightFacing;
                rb.velocity = new Vector2(0, speed);
            } 
        }      
    }
}
