using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoodSprite : BaseSprite
{
    protected EnemyBase enemyBase;
    private void Start()
    {
        enemyBase = GameObject.FindAnyObjectByType<EnemyBase>();
    }
    private void FixedUpdate()
    {
        transform.position = Vector2.MoveTowards(transform.position, targetNode.transform.position, Time.deltaTime * movementSpeed);
        if (transform.position == targetNode.transform.position)
        {
            if (currentNode == LastNode)
            {
                enemyBase.enemyBaseHealth = Mathf.Clamp(enemyBase.enemyBaseHealth -
                    Mathf.CeilToInt(this.damage * Mathf.CeilToInt(this.health / this.damage))
                    , 0, 9999);
                Destroy(gameObject);
            }
            targetNode = GameObject.Find("PathNode (" + ++currentNode + ")");
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        //If this collided with Enemy calculates which loses health and which dies
        if (collision.gameObject.tag.Equals("BadSprites"))
        {
            float enemyHealth = collision.gameObject.GetComponent<BadSprite>().health;
            float enemyDamage = collision.gameObject.GetComponent<BadSprite>().damage;

            int turnsToKillThis = Mathf.CeilToInt(this.health / enemyDamage);
            int turnsToKillEnemy = Mathf.CeilToInt(enemyHealth / this.damage);

            if (turnsToKillEnemy > turnsToKillThis)
            {
                collision.gameObject.GetComponent<BadSprite>().health -= (this.damage * turnsToKillThis);
                Destroy(gameObject);
            }
            else if(turnsToKillEnemy == turnsToKillThis)
            {
                Destroy(collision.gameObject);
                Destroy(gameObject);
            }
            else
            {
                this.health -= (enemyDamage * turnsToKillEnemy);
                Destroy(collision.gameObject);
            }
        }
    }
}
