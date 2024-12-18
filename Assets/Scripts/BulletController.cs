using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletController : MonoBehaviour
{
    public float speed;
    private float range  = 0f;
    private float  attackSpeed;
    private  Vector3 startPosition;
    // Start is called before the first frame update
    void Start()
    {
        startPosition = this.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        this.transform.position += transform.forward  * speed * Time.deltaTime;

        if(Mathf.Abs(Vector3.Distance(startPosition, this.transform.position)) > 5f){
            Destroy(this.gameObject);
        }
    }


    
}
