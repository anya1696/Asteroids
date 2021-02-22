using UnityEngine;

/**
 * Игрок. Нужен для указания спавнпоинта снаряда, самого снаряда и реализации логики.
*/

public class Player : MonoBehaviour {
    public GameObject gun;
    public GameObject missile;

    public float shipSpeed = 1f;
    public float missileSpeed = 1f;

    public float rotationSpeed = -5f;
    public float moveForce = 2f;


    public GameObject projectileParent;

    void Update()
    {
        PlayerControl();
    }

    void OnShipCollision(){
        EventManager.EventBus.Publish(new ShipCollisionAsteroidEvent());
    }

    void OnTriggerEnter2D(Collider2D collision){
        if (collision.gameObject.tag == "Asteroid") {
            EventManager.EventBus.Publish(new ShipCollisionAsteroidEvent());
        }
    }

    void Shot(){
        GameObject projectile = Instantiate(missile, gun.transform.position, Quaternion.identity);
        projectile.gameObject.SetActive(true);
        projectile.transform.SetParent(projectileParent.transform);
        projectile.transform.rotation = transform.rotation;
        projectile.GetComponent<Rigidbody2D>().velocity = transform.right * missileSpeed;
    }

    void PlayerControl(){
        float moveHorizontal = Input.GetAxis("Horizontal");
        transform.Rotate(new Vector3(0, 0, -moveHorizontal) * rotationSpeed);

        float moveVertical = Input.GetAxis("Vertical");
        Vector2 v = moveVertical * transform.right;

        GetComponent<Rigidbody2D>().velocity = Vector3.ClampMagnitude
        (GetComponent<Rigidbody2D>().velocity, shipSpeed);
        GetComponent<Rigidbody2D>().AddForce(v.normalized * moveForce);

        if (Input.GetKeyDown("space")) {
            Shot();
        }
    }
}
