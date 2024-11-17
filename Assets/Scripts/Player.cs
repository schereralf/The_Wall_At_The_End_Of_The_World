
using UnityEngine;

public class Player : MonoBehaviour
{
    private readonly float speed = 7.5f;
    private readonly float jumpSpeed = 3.0f;
    private float horizontalInput;
    private float forwardInput;
    public CharacterController controller;
    public Vector3 move;
    private Vector3 velocity;
    public float gravity = -9.81f;
    public Transform groundCheck;
    public float groundDistance = 0.1f;
    public LayerMask groundMask;
    public bool isGrounded;

    public GameObject bow;
    private bool canShoot;
    float arrowReloadTime;

    // Update is called once per frame.  Here we do basic movement including jumps.

    void Update()
    {
        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);

        if (isGrounded && velocity.y < 0)
        {
            velocity.y = -2f;
        }

        horizontalInput = Input.GetAxis("Horizontal");
        forwardInput = Input.GetAxis("Vertical");

        // Move the player forward  

        move = transform.right * horizontalInput + transform.forward * forwardInput;
        controller.Move(speed * Time.deltaTime * move);

        velocity.y += gravity * Time.deltaTime;
        controller.Move(velocity * Time.deltaTime);

        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            velocity.y = Mathf.Sqrt(jumpSpeed * -2f * gravity);
        }

        if (Input.GetMouseButton(0) && canShoot)
        {
            arrowReloadTime = Time.time + 0.3f;
            canShoot = false;
            // get object from the pool
            GameObject pooledProjectile = ObjectPooler.SharedInstance.GetPooledObject();
            if (pooledProjectile != null)
            {
                pooledProjectile.SetActive(true); 
                // activate arrow
                pooledProjectile.transform.SetPositionAndRotation(bow.transform.position, bow.transform.rotation);
                // position the arrow in line with player
            }
        }

        if (!canShoot && Time.time > arrowReloadTime) 
            // wait a while after last arrow was shot
            canShoot = true;
    }
}
