using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Circlearound : MonoBehaviour
{
        public float speed = 1;  //Drone travel speed;
        public int damage = 5; //Flat damage
        public float radius = 3;
        private  Vector3 startPosition;
        private GameObject owner;
        private float angle = 0;
        // Start is called before the first frame update
        void Start()
        {
            startPosition = this.transform.position + new Vector3(5,0,0);
        }
    
        // Update is called once per frame
        void Update()
        {
            if (!owner)
            {
                owner = GameObject.FindGameObjectWithTag("Player");
            }
            float x = Mathf.Cos(angle)*radius + owner.transform.position.x;
            float z = Mathf.Sin(angle)*radius + owner.transform.position.z;
            this.transform.position = new Vector3(x, 0, z);
            angle = angle + speed*Time.deltaTime;
            
        }
        
        private void onTriggerEnter(Collider collision){
            
            Health entityHealth = collision.gameObject.GetComponent<Health>();

            if (entityHealth == null)
                return;
            

            if(collision.gameObject.tag == "Enemy"){

                entityHealth.takeDamage(damage);
            }
        }
        

}
