using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    private float speed = 20f;
    private Vector3 shootDirection;

    public void Setup(Vector3 shootDir)
    {
        shootDirection = shootDir;
        Destroy(gameObject, 3.0f);
    }

    private void Update()
    {
        transform.position += shootDirection * speed * Time.deltaTime;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            Destroy(gameObject);
            Destroy(collision.gameObject);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(this.tag == "RedBullet")
        {
            if(other.CompareTag("Blue"))
            {
                Destroy(gameObject);
                Destroy(other.gameObject);
            }
            else
            {
                Destroy(gameObject);
            }
        }
        else if(this.tag == "BlueBullet")
        {
            if(other.CompareTag("Red"))
            {
                Destroy(gameObject);
                Destroy(other.gameObject);
            }
            else
            {
                Destroy(gameObject);
            }
        }
    }
}
