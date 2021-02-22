using UnityEngine;

/**
 * Отображение астеройда. Трекает столкновения, кидает необходимые для других классов ивенты.
*/

public class Asteroid : MonoBehaviour {
    public AsteroidType type;

    void OnTriggerEnter2D(Collider2D collision){
        string collisionTag = collision.gameObject.tag;
        if (collisionTag == "Missile" || collisionTag == "Player") {
            SideDestroy();
        }
    }

    void SideDestroy(){
        transform.parent = null;
        EventManager.EventBus.Publish(new AsteroidDestroyEvent(this));
        Destroy(this.gameObject);
    }

    public bool CanSplitUp(){
        return Type != AsteroidType.Small;
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
