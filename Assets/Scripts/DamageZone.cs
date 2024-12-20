using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class DamageZone : MonoBehaviour
{

    public int damage;
    public float range;
    public Health entityHealth;
    public GameObject player;
    public float cooldown;
    public float currentCooldown;
    
    

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Player");
        damage = Mathf.FloorToInt(player.GetComponent<CharacterControllerScript>().attackDamageModifier);
        this.transform.parent = player.transform;
        this.transform.localScale  = new Vector3(range,0.0001f,range);
        this.transform.position = player.transform.position + new Vector3(0f,-1f,0f);
        currentCooldown = cooldown;
    }

    // Update is called once per frame
    void Update()
    {
        if(currentCooldown < 0f)
            currentCooldown = cooldown;

        currentCooldown -= Time.deltaTime;
        
    }
    
    void OnTriggerStay(Collider collider){

        if(collider.tag == "Enemy" && currentCooldown <= 0f){
            entityHealth = collider.gameObject.GetComponent<Health>();
            if(entityHealth == null){
                print("Coll "+collider.name);
                return;
            }

            entityHealth.takeDamage(damage);
        }
    }
}

//on  trigger stay