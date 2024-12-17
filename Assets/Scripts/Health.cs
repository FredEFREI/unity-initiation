using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{

    public int health;
    public  int maxHealth = 10;
    private float cooldown;
    private float cooldownTime = 3;
    // Start is called before the first frame update
    void Start()
    {
        health = maxHealth;
        if(gameObject.tag != "Player"){
            cooldownTime = 0;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(cooldown > 0){
        cooldown -= Time.deltaTime;
        }
    }

    public void takeDamage(int amount){

        if(cooldown <= 0){

        health -= amount;

        if(health <= 0){
            Destroy(gameObject);
        } 
        
        cooldown = cooldownTime;
        }
       
    }
}
