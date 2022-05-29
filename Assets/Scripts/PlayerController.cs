
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Rigidbody rigidBody;
    public GameObject bow;
    public float speed;
    public ForceMode force;
    float forwardMove;
    float sidewaysMove;
    Vector3 velocity;
    public float jumpForce;
    private bool canShoot;
    float arrowReloadTime;

    // Start is called before the first frame update
    void Start()
    {
        rigidBody = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        sidewaysMove = Input.GetAxis("Horizontal");
        forwardMove = Input.GetAxis("Vertical");
        if (Input.GetKeyDown(KeyCode.Space))
        {
            rigidBody.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
        }

        velocity = new Vector3(sidewaysMove, 0, forwardMove);

        // switches from local space to global
        velocity = transform.TransformDirection(velocity);  
        velocity *= speed;
        velocity -= rigidBody.velocity;
        rigidBody.AddForce(velocity, force);
        transform.Rotate(new Vector3(0, sidewaysMove, 0));

        if (Input.GetMouseButton(0) && canShoot)
        {
            arrowReloadTime = Time.time + 0.2f;
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
