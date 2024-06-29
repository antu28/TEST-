using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class Shooting : MonoBehaviour
{
    public GameObject playerBullet;
    public GameObject Missile;
    public Transform spawnPoint1;
    public Transform spawnPoint2;
    public int missileCount;
    public GameObject missileCountUI;





    // Start is called before the first frame update
    void Start()
    {
        missileCount = 10;
        missileCountUI.GetComponent<Text>().text = missileCount.ToString();
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetButtonDown("Fire1"))
        {
            Instantiate(playerBullet, spawnPoint1.position, Quaternion.identity);
            Instantiate(playerBullet, spawnPoint2.position, Quaternion.identity);

        }

        if (Input.GetButtonDown("Fire2") && missileCount > 0  )
        {
            Instantiate(Missile, gameObject.transform.position, Quaternion.identity);
            missileCount--;
            missileCountUI.GetComponent<Text>().text = missileCount.ToString();



        }





    }
}
