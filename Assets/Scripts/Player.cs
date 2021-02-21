using UnityEngine;

public class Player : MonoBehaviour {
    public GameObject gun;
    public GameObject missile;

    public float shipSpeed = 1f;
    public float missileSpeed = 1f;

    public float rotationSpeed = -5f;

    public GameObject ProjectileParent { get; set; }

    void Start()
    {

    }

    void Update()
    {
        PlayerControl();
    }

    void OnShipCollision(){
        EventManager.EventBus.Publish(new ShipCollisionEvent());
    }

    void OnTriggerEnter2D(Collider2D collision){
        if (collision.gameObject.tag == "Asteroid") {
            EventManager.EventBus.Publish(new ShipCollisionEvent());
        }
    }

    void Shot(){
        GameObject projectile = Instantiate(missile, gun.transform.position, Quaternion.identity);
        projectile.GetComponent<Missile>().SetSpeed(missileSpeed);
        projectile.gameObject.SetActive(true);
        projectile.transform.SetParent(ProjectileParent.transform);
        projectile.transform.rotation = transform.rotation;

    }

    void PlayerControl(){
        float moveHorizontal = Input.GetAxis("Horizontal");
        transform.Rotate(new Vector3(0,0, -moveHorizontal) * rotationSpeed);

        //TODO добавить энерцию
        float moveVertical = Input.GetAxis("Vertical");
        GetComponent<Rigidbody2D>().velocity = moveVertical*shipSpeed * transform.right;

        if (Input.GetKeyDown("space")) {
            Debug.Log("Space pressed");
            Shot();
        }




    }
}
