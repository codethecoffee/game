using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class Weapon : MonoBehaviour
{
    public Player player;
    public int damage;
    public bool right;
    //weapon is inRange for attack
    private bool _inRange = false;
    private Enemy _enemy;
    private Vector3 _weaponRotationAnimationAngle;
    private float _weaponAnimationTimer = .1f;

    private void Start()
    {
        _weaponRotationAnimationAngle = right ? Vector3.forward * -10 : Vector3.forward * 10;
    }
    public void HitEnemy(Enemy enemy)
    {
        enemy.LoseHP(damage);
    }

    private void OnTriggerEnter2D(Collider2D collider)
    {
        string[] pickupTypes = { "Present", "Cherry" };

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
        else if (pickupTypes.Contains(collider.gameObject.tag))
        {
            Debug.Log("Pick up collision!");
            player.UpdateScore(collider.gameObject.tag);
            Destroy(collider.gameObject);
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

    IEnumerator playRotationAnimation()
    {
        var fromAngle = transform.parent.transform.rotation;
        var toAngle = Quaternion.Euler(transform.eulerAngles + _weaponRotationAnimationAngle);
        for (var t = 0f; t < 1; t += Time.deltaTime / _weaponAnimationTimer)
        {
            transform.parent.transform.rotation = Quaternion.Lerp(fromAngle, toAngle, t);
            yield return null;
        }
        for (var t = 0f; t < 1; t += Time.deltaTime / _weaponAnimationTimer)
        {
            transform.parent.transform.rotation = Quaternion.Lerp(toAngle, fromAngle, t);
            yield return null;
        }
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            StartCoroutine(playRotationAnimation());
        }

        if (_inRange && player.isAttacking)
        {
            player.isAttacking = false;
           HitEnemy(_enemy);  
        }
    }
}
