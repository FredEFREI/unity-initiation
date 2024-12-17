using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Damage : MonoBehaviour
{

    public int damage = 2;
    public Health entityHealth;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter(Collision collision){

        print(collision.gameObject.name + "  " + this.gameObject.name);

        if(entityHealth == null){

            entityHealth = collision.gameObject.GetComponent<Health>();
            if(entityHealth == null)
                return;

                print(entityHealth.gameObject.name);
        }

        if(collision.gameObject.tag ==  "Player"){

            entityHealth.takeDamage(damage);
        }
        else if(collision.gameObject.tag == "Enemy" && gameObject.tag == "Bullet"){

            Destroy(gameObject);
            entityHealth.takeDamage(damage);
        }
    }
}
