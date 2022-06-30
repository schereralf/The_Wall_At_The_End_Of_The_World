
using UnityEngine;

public class ArrowFlight : MonoBehaviour
{
    public float speed = 40.0f;
    public GameObject deadEnemy;
    public GameObject deadArrow;
    private readonly float lowerLimit=-10f;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(speed * Time.deltaTime * Vector3.forward);
        if (gameObject.transform.position.y < lowerLimit)
        {
            gameObject.SetActive(false);
        }
    }

    void OnCollisionEnter(Collision col)
    {
        if (col.gameObject.CompareTag("Enemy"))
        {
            Instantiate(deadEnemy, col.gameObject.transform.position, col.gameObject.transform.rotation);
            gameObject.SetActive(false);



            Destroy(col.gameObject);
        }
        if (col.gameObject.CompareTag("Ground"))
        {
            Instantiate(deadArrow, gameObject.transform.position, gameObject.transform.rotation);
            gameObject.SetActive(false);
        }
        else return;
    }
}
