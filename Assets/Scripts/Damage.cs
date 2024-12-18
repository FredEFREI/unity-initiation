using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Damage : MonoBehaviour
{

    public int damage;
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

        if(entityHealth == null){

            entityHealth = collision.gameObject.GetComponent<Health>();
            if(entityHealth == null)
                return;
        }

        if(collision.gameObject.tag ==  "Player"){

            entityHealth.takeDamage(damage);
        }
        else if(collision.gameObject.tag == "Enemy" && gameObject.tag == "Weapon"){

            Destroy(gameObject);
            entityHealth.takeDamage(damage);
        }
    }

    void GetDamage(){

    }
}
