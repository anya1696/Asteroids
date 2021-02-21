using UnityEngine;

public class Missile : MonoBehaviour {
    public Rigidbody2D rb;
    float speed;

    public void SetSpeed(float value){
        speed = value;
    }

    void Start()
    {
        rb.velocity = transform.right * speed;
    }

    void OnTriggerEnter2D(Collider2D collision){
        if (collision.gameObject.tag == "Asteroid") {
            Destroy(gameObject);
        }
    }

    void Update()
    {

    }
}
