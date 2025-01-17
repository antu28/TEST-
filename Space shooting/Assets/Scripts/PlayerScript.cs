﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerScript : MonoBehaviour
{
    public GameObject damageEffect;
    public GameObject explosion;
    public PlayerHealthbarScript playerHealthbar;
    public GameController gameController;

    public float speed = 10f;
    public float padding = 0.8f;
    float minX;
    float maxX;
    float minY;
    float maxY;

    public float health = 20f;
    float barFillAmount = 1f;
    float damage = 0;

    // Start is called before the first frame update
    void Start()
    {
        FindBoundaries();
        damage = barFillAmount / health;
    }

    void FindBoundaries()
    {
        Camera gameCamera = Camera.main;
        minX = gameCamera.ViewportToWorldPoint(new Vector3(0, 0, 0)).x + padding;
        maxX = gameCamera.ViewportToWorldPoint(new Vector3(1, 0, 0)).x - padding;
        minY = gameCamera.ViewportToWorldPoint(new Vector3(0, 0, 0)).y + padding;
        maxY = gameCamera.ViewportToWorldPoint(new Vector3(0, 1, 0)).y - padding;
    }

    // Update is called once per frame
    void Update()
    {
        //float deltaY = Input.GetAxis("Vertical")*Time.deltaTime * speed;
        //float deltaX = Input.GetAxis("Horizontal") * Time.deltaTime * speed;

        //float newXpos = Mathf.Clamp(transform.position.x + deltaX, minX, maxX);
        //float newYpos = Mathf.Clamp(transform.position.y + deltaY, minY, maxY);


        //transform.position = new Vector2(newXpos, newYpos);

        if (Input.GetMouseButton(0))
        {
            Vector2 newPos = Camera.main.ScreenToWorldPoint(new Vector2(Input.mousePosition.x, Input.mousePosition.y));
            transform.position = Vector2.Lerp(transform.position, newPos, 10 * Time.deltaTime);
        }

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        print(collision.gameObject.tag);
        if(collision.gameObject.tag=="EnemyBullet")
        {
            DamagePlayerHealthbar();
            Destroy(collision.gameObject);
            GameObject damageVfx= Instantiate(damageEffect, collision.transform.position, Quaternion.identity);
            Destroy(damageVfx, 0.05f);
            if(health<=0)
            {
                gameController.GameOver();
                Destroy(gameObject);
                GameObject blast = Instantiate(explosion, transform.position, Quaternion.identity);
                Destroy(blast, 2f);
            }

            
        }

        if (collision.gameObject.tag == "PathoreBari")
        {
           
                gameController.GameOver();
                Destroy(gameObject);
                GameObject blast = Instantiate(explosion, transform.position, Quaternion.identity);
                Destroy(blast, 2f);


        }

        if (collision.gameObject.tag == "BossErNani")
        {
            
            {
                gameController.GameOver();
                Destroy(gameObject);
                GameObject blast = Instantiate(explosion, transform.position, Quaternion.identity);
                Destroy(blast, 2f);
            }


        }


    }

    
    void DamagePlayerHealthbar()
    {
        if(health>0)
        {
            health -= 1;
            barFillAmount = barFillAmount - damage;
            playerHealthbar.SetAmount(barFillAmount);

        }
    }

    


}
