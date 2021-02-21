using Redbus.Events;

public class AsteroidDestroyEvent : EventBase {
    public Asteroid Asteroid { get; set; }

    public AsteroidDestroyEvent(Asteroid asteroid){
        Asteroid = asteroid;
    }
}
