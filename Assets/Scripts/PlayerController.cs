using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterControllerScript : MonoBehaviour
{
    public float walkSpeed = 5f;  // default Speed of the player
    public float sprintSpeed = 15f; // default sprint speed of the player
    private float moveSpeed;
    public Camera mainCamera;    // Reference to the main camera
    private CharacterController characterController;
    private Rigidbody rb;        // Reference to the Rigidbody component
    public List<GameObject> weapons = new List<GameObject>();
    

    private List<Coroutine> weaponsRoutine = new List<Coroutine>();
    private LevelUP lvlManager;

    public bool isRunning = false;
    public float repulseForce = 10f;    
    public float repulseDuration = 0.5f;
   
    public float xp = 0;
    public float levelUpXp = 5;
    public int lvl = 1;
    

    public float attackSpeedModifier = 1.0f;
    public float attackDamageModifier = 1;
    public float damageReduction = 0.0f;
    public float speedModifier = 1.0f;
    public int additionalProjection = 0;
    public float rangeModifer = 1.0f;
    public float bulletSpeedModifier = 1.0f;
    public bool isInPauseMenu = false;
    public GameObject pauseCanvas;
    public GameObject looseCanvas;

    void Start()
    {
        gameObject.GetComponent<Health>().setIsPlayer();
        // Get the Rigidbody component attached to the player
        rb = GetComponent<Rigidbody>();

        // Automatically assign the main camera if not set
        if (mainCamera == null)
        {
            mainCamera = Camera.main;
        }

        characterController = GetComponent<CharacterController>();
        
        lvlManager = GameObject.FindGameObjectWithTag("LvlManager").GetComponent<LevelUP>();

        StartAllCoroutine();

        levelUpXp = 10;
        lvl =1;
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
        if (Time.timeScale != 0)
        {
            RotatePlayerToMouse();
        }
        if (Input.GetKeyDown(KeyCode.Escape)) 
            TogglePauseMenu();
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
    void OnDestroy()
    {
        ToggleLooseMenu();
    }

    void StartAllCoroutine()
    {
        foreach (var weapon in weapons)
        {
            if (!weapon.GetComponent<Weapon>().owner)
                weapon.GetComponent<Weapon>().owner = this;

            // Start a separate coroutine for each weapon
             weaponsRoutine.Add(StartCoroutine(FireWeaponRoutine(weapon)));
        }
    }

    void ResetAllRoutine()
    {
        foreach (var coroutine in weaponsRoutine)
        {
            StopCoroutine(coroutine);
        }

        weaponsRoutine.Clear();
        
        StartAllCoroutine();
    }

    IEnumerator FireWeaponRoutine(GameObject weapon)
    {
        while (true)
        {
            yield return new WaitForSeconds(GetWeaponStats(weapon)); // Wait for this weapon's cooldown
            Instantiate(weapon, this.transform.position + this.transform.forward, this.transform.rotation); // Fire the weapon
        }
    }

    float GetWeaponStats(GameObject weapon){
        return weapon.GetComponent<Weapon>().attackSpeed;
    }

    public void AddXp(float x){

        xp += x;

        if(xp >= levelUpXp){
            xp -= levelUpXp;
            levelUpXp *= 1.10f;
            lvl++;
            StartCoroutine(UpgradePause());
        }
    }

    IEnumerator UpgradePause()
    {
        lvlManager.initializeLvlUp(lvl);
        Time.timeScale = 0;
        while(Time.timeScale == 0){
                if(Input.GetKey(KeyCode.Space) || Input.GetKey(KeyCode.Return)){
                    Time.timeScale = 1;
                }
                yield return new WaitForEndOfFrame();
        }
        ResetAllRoutine();

    }
    public void TogglePauseMenu()
    {
        if (pauseCanvas)
        {
            if (!pauseCanvas.activeSelf)
            {
                pauseCanvas.SetActive(true);
                Time.timeScale = 0;
            } else
            {
                pauseCanvas.SetActive(false);
                Time.timeScale = 1;
            }  
        }
        else
        {
            Debug.LogError("Canvas is not assigned in the inspector.");
        }
    }
    public void ToggleLooseMenu()
    {
        if (looseCanvas != null)
        {
            if (!looseCanvas.activeSelf)
            {
                looseCanvas.SetActive(true);
                Time.timeScale = 0;
            }
            else
            {
                looseCanvas.SetActive(false);
                Time.timeScale = 1;
            }
        }
        else
        {
            Debug.LogError("looseCanvas is not assigned in the inspector.");
        }
    }
}