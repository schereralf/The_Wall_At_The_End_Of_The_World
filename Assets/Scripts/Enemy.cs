
using UnityEngine;

public class Enemy : MonoBehaviour

{
    public float speed = 2.0f;
    private bool inMotion;
    private GameManager gameManager;

    // Start is called before the first frame update
    void Start()
    {
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        inMotion = true;
    }

    // Update is called once per frame
    void Update()
    {

        if (inMotion) transform.Translate(speed * Time.deltaTime * Vector3.back);
        if (gameObject.transform.position.y < -10) Destroy(gameObject);
    }

    void OnCollisionEnter(Collision col)
    {
        if (col.gameObject.CompareTag("Wall"))
        {
            inMotion = false;                   
            gameManager.GameOver();
        }
    }
}