using UnityEngine;

/**
 * Снаряд. Трекает свое столкновение с астероидом и дестроится.
*/

public class Missile : MonoBehaviour {
    public Rigidbody2D rb;

    void OnTriggerEnter2D(Collider2D collision){
        if (collision.gameObject.tag == "Asteroid") {
            Destroy(gameObject);
        }
    }
}
