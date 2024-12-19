using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    public float speed;  //Bullet travel speed;
    public int damage = 2; //Flat damage
    public float attackSpeed = 1f; //Seconds between each shot
    public float range = 5f;  //Range before destruction;
    private  Vector3 startPosition;
    public Health entityHealth;

    // Start is called before the first frame update
    void Start()
    {
        startPosition = this.transform.position;
        //GetComponent<Damage>().damage = damage;
    }

    // Update is called once per frame
    void Update()
    {
        this.transform.position += transform.forward  * speed * Time.deltaTime;

        if(Mathf.Abs(Vector3.Distance(startPosition, this.transform.position)) > range){
            Destroy(this.gameObject);
        }
    }

    private void OnCollisionEnter(Collision collision){

        if(entityHealth == null){

            entityHealth = collision.gameObject.GetComponent<Health>();
            if(entityHealth == null)
                return;
        }

        if(collision.gameObject.tag == "Enemy"){

            Destroy(gameObject);
            entityHealth.takeDamage(damage);
        }
    }


    
}
