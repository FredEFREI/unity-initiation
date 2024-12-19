using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomShot : Weapon
{
    public float speed = 1;  //Bullet travel speed;
    public int damage = 8; //Flat damage
    public float attackSpeed = 10f; //Seconds between each shot
    public float range = 20f;  //Range before destruction;
    private  Vector3 startPosition;
    public Health entityHealth;
    private Vector3 direction;
    public int numberOfCollision = 3;

    // Start is called before the first frame update
    void Start()
    {
        float angle = Random.Range(0.0f, 1.0f) * Mathf.PI * 2;
        direction = new Vector3(Mathf.Cos(angle), 0, Mathf.Sin(angle));
        startPosition = this.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        this.transform.position += direction  * speed * Time.deltaTime;

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
            
            numberOfCollision--;
            if (numberOfCollision == 0){Destroy(gameObject);}
            entityHealth.takeDamage(damage);
            
        }
    }
    
    


    
}