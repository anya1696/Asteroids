using Redbus.Events;

/**
 * Ивент на уничтожение астероида
*/

public class AsteroidDestroyEvent : EventBase {
    public Asteroid Asteroid { get; set; }

    public AsteroidDestroyEvent(Asteroid asteroid){
        Asteroid = asteroid;
    }
}
