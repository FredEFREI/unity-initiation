using UnityEngine;

public class CharacterControllerZQSD : MonoBehaviour
{
    public float moveSpeed = 5f;  // Speed of the player
    public Camera mainCamera;    // Reference to the main camera
    private CharacterController characterController;
    private Rigidbody rb;        // Reference to the Rigidbody component
    public GameObject bulletPrefab;

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
    }


    void Update()
    {
        // Process movement
        float moveX = Input.GetAxis("Horizontal");
        float moveZ = Input.GetAxis("Vertical");

        Vector3 move = Vector3.right * moveX + Vector3.forward * moveZ;
        characterController.Move(move * moveSpeed * Time.deltaTime);

        if(Input.GetKeyDown(KeyCode.Space)){
            Instantiate(bulletPrefab, this.transform.position + this.transform.forward, this.transform.rotation);
        }

        // Process rotation
        RotatePlayerToMouse();
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
}