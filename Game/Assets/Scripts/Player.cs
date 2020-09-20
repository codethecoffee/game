using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class Player : MonoBehaviour
{
    // Score stuff!
    public float totalScore = 0;
    public int numPresents = 0; // Number of present pick-ups
    public int numCherries = 0; // Number of cherry pick-ups

    //things related to HP
    private int _maxhp = 100;
    private int _hp = 100;
    public Transform healthBar;
    private float _healthBarLength;
    private float _maxHealthBarLength;

    //rn public bc i wanna easily see the value of it but maybe write a get method instead
    public bool invincible = false;
    private float _invincibleTimer = .4f;

    public bool isAttacking = false;
    private float _attackingTimer = .2f;

    //if a player is inactive, then the level restarts
    public bool active = true;

    void Start()
    {
        _maxHealthBarLength = healthBar.localScale.x;
    }

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

    public void LoseHP(int hpLoss)
    {
        _healthBarLength = ((float)(_hp - hpLoss) / _maxhp) * _maxHealthBarLength;
        healthBar.localScale = new Vector3(
            _healthBarLength, 
            healthBar.localScale.y, 
            healthBar.localScale.z);
        _hp -= hpLoss;
        if (_hp <= 0) active = false;
    }

    public void GainHP(int hpGain)
    {
        if (_hp + hpGain >= 100)
        {
            //full health
            _hp = 100;
            healthBar.localScale = new Vector3(
                _maxHealthBarLength,
                healthBar.localScale.y,
                healthBar.localScale.z);

        } else {
            _healthBarLength = ((float)(_hp + hpGain) / _maxhp) * _maxHealthBarLength;
            healthBar.localScale = new Vector3(
                _healthBarLength, 
                healthBar.localScale.y, 
                healthBar.localScale.z);
            _hp += hpGain;
        }
    }

    // Update score based on the type of pick-up
    public void UpdateScore(string pickupType)
    {
        switch (pickupType)
        {
            case "Present":
                numPresents += 1;
                totalScore += 10;
                break;
            case "Cherry":
                numCherries += 1;
                totalScore += 2;
                break;
            default:
                break;
        }
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Debug.Log("attack attack");
            StartCoroutine(SetAttacking());
        }
    }

    // TODO: Factor this out to so it's a script attached to pick up objects
    // rather than the player object

    private void OnTriggerEnter2D(Collider2D collision)
    {
        string collisionTag = collision.gameObject.tag;

        string[] pickupTypes = { "Present", "Cherry" };

        // If it is a pick-up...
        if (pickupTypes.Contains(collisionTag))
        {
            Debug.Log("Pick up collision!");
            UpdateScore(collision.gameObject.tag);
            Destroy(collision.gameObject);
        }
    }
}
