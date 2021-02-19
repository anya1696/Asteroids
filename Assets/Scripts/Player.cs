using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public GameObject gun;
    public GameObject missile;

    public float shipSpeed = 0.5f;
    public float missileSpeed = 1f;

    public GameObject ProjectileParent {get;set;}

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
        if (collision.gameObject.tag == "Asteroid"){
            EventManager.EventBus.Publish(new ShipCollisionEvent());
        }
    }

    void Shot(){
        GameObject projectile = Instantiate(missile, gun.transform.position, Quaternion.identity);
        projectile.GetComponent<Missile>().SetSpeed(missileSpeed);
        projectile.gameObject.SetActive(true);
        projectile.transform.SetParent(ProjectileParent.transform);

    }

    void PlayerControl(){
        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");
        Vector2 currentPosotion = transform.position;
        gameObject.transform.position = new Vector2(
                currentPosotion.x + (moveHorizontal * shipSpeed) ,
                currentPosotion.y + (moveVertical * shipSpeed));

        if (Input.GetKeyDown("space")){
            Debug.Log("Space pressed");
            Shot();
        }

    }
}
