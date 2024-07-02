using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class enemyShooter : MonoBehaviour
{
    public GameObject bullet;
    public Transform bulletPos;
    private GameObject player;
    public GameObject SnailAlive;
    public GameObject Shell;


    private float timer;
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {

        float distance = Vector2.Distance(transform.position, player.transform.position);
        Debug.Log($"Distance to player: {distance}");
        if (distance < 27.5)
        {
            timer += Time.deltaTime;

            if (timer > 1)
            {
                timer = 0;
                shoot();
            }
        }
        if (distance < 10)
        {
            if (timer > 2)
            {
                timer = 0;
                shoot();
            }
        }
    }
    void shoot()
    {
        Instantiate(bullet, bulletPos.position, Quaternion.identity);
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            ContactPoint2D[] contactPoints = collision.contacts;

            foreach (ContactPoint2D contact in contactPoints)
            {
                if (contact.normal.y >0.5f)
                {
                    Debug.Log("Enemy Die");
                    //Player enemyScript = collision.gameObject.GetComponent<Player>();
                    //if (enemyScript != null)
                    //{
                    //    enemyScript.TakeDamage();
                    //}
                    //return;
                }
                else
                {
                    Debug.Log("Damage from enemy");
                    TakeDamage();
                    return;
                }
            }
        }
    }

    private void TakeDamage()
    {
        // Disable the SnailAlive GameObject
        SnailAlive.SetActive(false);

        // Enable the Shell GameObject at the Snail's position
        Shell.transform.position = SnailAlive.transform.position;
        Shell.SetActive(true);
    }
}
