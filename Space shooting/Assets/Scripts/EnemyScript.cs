using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyScript : MonoBehaviour
{
    public Transform gunPoint1;
    public Transform gunPoint2;
    public GameObject enemyBullet;

    public GameObject enemyExplosionPrefab;
    public Healthbar healthbar;
    public GameObject damageEffect;

    public float speed = 1f;
    public float health = 10f;
    public float bulletDamage = 1f;
    public float missileDamage = 10;

    float barSize = 1f;
    float damage = 0;

    public float enemyBulletSpawnTime= 0.5f;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(EnemyShooting());
        damage = barSize / health;
    }

    // Update is called once per frame
    void Update()
    {
        // boss fight no moving ,this can be cut off
        transform.Translate(Vector2.down * speed * Time.deltaTime);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag=="PlayerBullet")
        {
            DamageHealthbar(bulletDamage);
            Destroy(collision.gameObject);
            GameObject damageVfx = Instantiate(damageEffect, collision.transform.position, Quaternion.identity);
            Destroy(damageVfx, 0.05f);
            if(health<= 0)
            {
                Destroy(gameObject);
                GameObject enemyExplosion = Instantiate(enemyExplosionPrefab, transform.position, Quaternion.identity);
                Destroy(enemyExplosion, 0.4f);
            }
            
        }

        if (collision.tag == "PlayerMissile")
        {
            DamageHealthbar(missileDamage);
            Destroy(collision.gameObject);
            GameObject damageVfx = Instantiate(damageEffect, collision.transform.position, Quaternion.identity);
            Destroy(damageVfx, 0.05f);
            if (health <= 0)
            {
                Destroy(gameObject);
                GameObject enemyExplosion = Instantiate(enemyExplosionPrefab, transform.position, Quaternion.identity);
                Destroy(enemyExplosion, 0.4f);
            }

        }

    }

    void DamageHealthbar(float d)
    {
        if (health> 0)
        {
            health -= d;
            barSize = barSize - damage * d;
            healthbar.SetSize(barSize);
        }
    }


    void enemyFire()
    {
        Instantiate(enemyBullet, gunPoint1.position, Quaternion.identity);
        Instantiate(enemyBullet, gunPoint2.position, Quaternion.identity);

    }

    IEnumerator EnemyShooting()
    {
        while(true)
        {
            yield return new WaitForSeconds(enemyBulletSpawnTime);
            enemyFire();
        }
        
    }


}
