using System.Collections;
using UnityEngine;

public class CharacterControllerScript : MonoBehaviour
{
    public float walkSpeed = 5f;  // default Speed of the player
    public float sprintSpeed = 15f; // default sprint speed of the player
    private float moveSpeed;
    public Camera mainCamera;    // Reference to the main camera
    private CharacterController characterController;
    private Rigidbody rb;        // Reference to the Rigidbody component
    public GameObject weapon;

    public bool isRunning = false;
    
    public float repulseForce = 10f;    
    public float repulseDuration = 0.5f;
    private float attackSpeed;
    public float xp;
    public float levelUpXp;


    void Start()
    {
        // Get the Rigidbody component attached to the player
        rb = GetComponent<Rigidbody>();

        // Automatically assign the main camera if not set
        if (mainCamera == null)
        {
            mainCamera = Camera.main;
        }

        characterController = GetComponent<CharacterController>();
        
        GetWeaponStats();
        StartCoroutine(FireRoutine());
    }


    void Update()
    {
        // Process movement
        float moveX = Input.GetAxis("Horizontal"); // "Horizontal" axis corresponds to A/D or Q/D keys
        float moveZ = Input.GetAxis("Vertical");
        

        Vector3 move = new Vector3(moveX, 0, moveZ).normalized;
        if (Input.GetKey(KeyCode.LeftShift) && !isLookingBehind(move)) {
            moveSpeed = sprintSpeed;
            isRunning = true;
        }
        else {
            moveSpeed = walkSpeed;
            isRunning = false;
        }
        
        characterController.Move(move * moveSpeed * Time.deltaTime);

        // Process rotation
        RotatePlayerToMouse();
        this.transform.position = new Vector3(this.transform.position.x, 1.1f, this.transform.position.z);
    }

    void RotatePlayerToMouse()
    {
        // Get the mouse position in screen space
        Vector3 mouseScreenPosition = Input.mousePosition;

        // Convert mouse position to world space
        Ray ray = mainCamera.ScreenPointToRay(mouseScreenPosition);
        Plane groundPlane = new Plane(Vector3.up, Vector3.zero);

        if (groundPlane.Raycast(ray, out float distance))
        {
            Vector3 mouseWorldPosition = ray.GetPoint(distance);

            // Calculate the direction to look at
            Vector3 lookDirection = (mouseWorldPosition - transform.position).normalized;
            lookDirection.y = 0; // Keep the player upright

            // Rotate the player to face the mouse position
            Quaternion targetRotation = Quaternion.LookRotation(lookDirection);
            transform.rotation = targetRotation;
        }
    }

    bool isLookingBehind(Vector3 move)
    {
        move = move.normalized;

        // Compare the player's forward direction and move direction
        float dotProduct = Vector3.Dot(transform.forward, move);

        // If the dot product is less than a threshold, they are in opposite directions
        return dotProduct < -0.80f; // Threshold close to -1 for "opposite"
    }
    
    void OnCollisionEnter(Collision col)
    {
        if (col.gameObject.CompareTag("Enemy"))
        {
            Rigidbody rbB = col.gameObject.GetComponent<Rigidbody>();
            
            if (rbB != null)
            {
                // Calculate repulsion direction
                Vector3 repulsionDirection = (col.gameObject.transform.position - transform.position).normalized;
                rbB.velocity = Vector3.zero; // Stop current movement
                rbB.AddForce(repulsionDirection * repulseForce, ForceMode.Impulse);

                // Notify Object B to resume moving toward A after repulsion
                col.gameObject.GetComponent<Enemy>()?.StartMoveBackCoroutine(repulseDuration);
            }
        }
    }

    IEnumerator FireRoutine(){

        while(true){

            yield return new WaitForSeconds(attackSpeed);
            Instantiate(weapon, this.transform.position + this.transform.forward, this.transform.rotation);
        }
    }

    void GetWeaponStats(){
        attackSpeed = weapon.GetComponent<Weapon>().attackSpeed;
    }

    public void AddXp(float x){

        xp += x;

        if(xp >= levelUpXp){
            xp = 0;
            levelUpXp *= 1.10f;
            StartCoroutine(UpgradePause());
        }
    }

    IEnumerator UpgradePause(){

        Time.timeScale = 0;
        while(Time.timeScale == 0){
                if(Input.GetKeyUp(KeyCode.Space) || Input.GetKey(KeyCode.Return)){
                    Time.timeScale = 1;
                }
                yield return new WaitForEndOfFrame();
            }
    }


}