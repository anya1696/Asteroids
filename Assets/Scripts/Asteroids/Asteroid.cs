using UnityEngine;

public class Asteroid : MonoBehaviour {
    public AsteroidType type;

    void Start()
    {

    }

    void Update()
    {

    }

    void OnTriggerEnter2D(Collider2D collision){
        if (collision.gameObject.tag == "Missile"){
            SideDestroy();
        }else if (collision.gameObject.tag == "Player"){
            SideDestroy();
        }
    }

    void SideDestroy(){
        EventManager.EventBus.Publish(new AsteroidDestroyEvent(this));
        Destroy(gameObject);
    }

    public AsteroidType Type {
        get{
            return type;
        }
        set {
            type = value;
        }
    }
}
