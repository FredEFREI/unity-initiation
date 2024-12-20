using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{

    public int health = 10;
    public  int maxHealth = 10;
    private float cooldown;
    private float cooldownTime = 3;
    private bool isPlayer = false;

    public Canvas UI;
    private uiController uiScript;
    // Start is called before the first frame update
    void Start()
    {
        health = maxHealth;
        if(gameObject.tag != "Player"){
            cooldownTime = 0;
        }

        if (isPlayer)
        {
            uiScript = UI.GetComponent<uiController>();
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
        if (isPlayer)
            UpdatePlayerUI();
    }

    public void setIsPlayer()
    {
        isPlayer = true;
    }

    void UpdatePlayerUI()
    {
        uiScript.playerHealth = health;
        uiScript.playerMaxHealth = maxHealth;
        uiScript.UpdateHealthUI();
    }
}
