using System.Collections.Generic;
using UnityEngine;

public class AsteroidsManager : MonoBehaviour {
    public Asteroid AsteroidHuge;
    public Asteroid AsteroidLarge;
    public Asteroid AsteroidMedium;
    public Asteroid AsteroidSmall;

    Vector2 realCoord;
    Collider2D spawnAreaCollider;

    Dictionary<AsteroidType, Asteroid> asteroidPrefabs = new Dictionary<AsteroidType, Asteroid>();

    float nextactionTime = 0f;
    float timePeriod = 1f;

    int difficulty = 2;
    int AsteroidsCout = 0;

    void Start()
    {
        Init();
    }

    void Init()
    {
        EventManager.EventBus.Subscribe<AsteroidDestroyEvent>(OnAsteroidDestroy);

        asteroidPrefabs.Add(AsteroidType.Huge, AsteroidHuge);
        asteroidPrefabs.Add(AsteroidType.Large, AsteroidLarge);
        //asteroidPrefabs.Add(AsteroidType.Medium, AsteroidMedium);
        asteroidPrefabs.Add(AsteroidType.Small, AsteroidSmall);

        spawnAreaCollider = GetComponent<Collider2D>();
        realCoord = new Vector2(spawnAreaCollider.transform.position.x - spawnAreaCollider.bounds.size.x / 2, spawnAreaCollider.transform.position.y - spawnAreaCollider.bounds.size.y / 2);

        StartRound();
    }

    public void OnAsteroidDestroy(AsteroidDestroyEvent asteroidDestroyEvent){
        Asteroid asteroid = asteroidDestroyEvent.Asteroid;

        if (asteroid.Type == AsteroidType.Huge) {
            SpawnAsteroid(AsteroidType.Large, asteroid.transform.position, true);
            SpawnAsteroid(AsteroidType.Large, asteroid.transform.position, true);
        } else if (asteroid.Type == AsteroidType.Large) {
            SpawnAsteroid(AsteroidType.Small, asteroid.transform.position, true);
            SpawnAsteroid(AsteroidType.Small, asteroid.transform.position, true);
        } else if (asteroid.Type == AsteroidType.Small) {
            if (transform.childCount == 0) {
                EndRound();
            }
        }
    }

    Vector2 GetRandomSpawnPoint(){
        int rand = Random.Range(0, 3);
        Vector2 vector;
        if (rand == 0) {
            vector = new Vector2(realCoord.x, Random.Range(realCoord.y, realCoord.y + spawnAreaCollider.bounds.size.y));
        } else if (rand == 1) {
            vector = new Vector2(Random.Range(realCoord.x, realCoord.x + spawnAreaCollider.bounds.size.x), realCoord.y);
        } else if (rand == 2) {
            vector = new Vector2(realCoord.x + spawnAreaCollider.bounds.size.x, Random.Range(realCoord.y, realCoord.y + spawnAreaCollider.bounds.size.y));
        } else {
            vector = new Vector2(Random.Range(realCoord.x, realCoord.x + spawnAreaCollider.bounds.size.x), realCoord.y + spawnAreaCollider.bounds.size.y);
        }

        return vector;
    }

    void EndRound(){
        difficulty++;
        StartRound();
    }

    void StartRound(){
        for (int i = 0; i <= difficulty; i++) {
            SpawnAsteroid(AsteroidType.Huge, GetRandomSpawnPoint(), false);
        }
    }

    Asteroid SpawnAsteroid(AsteroidType asteroidType, Vector2 vector, bool randomVelocity){
        Asteroid asteroid;
        Vector3 vector3 = vector;
        Vector2 velocity;
        asteroid = Instantiate(asteroidPrefabs[asteroidType], vector3, Quaternion.identity);
        asteroid.gameObject.SetActive(true);
        asteroid.transform.SetParent(transform);

        if (randomVelocity) {
            velocity = new Vector2(Random.Range(-1, 1), Random.Range(-1, 1)) * GameArea.AsteroidTypeData[asteroidType].Speed;
        } else {
            velocity = Vector2.one * GameArea.AsteroidTypeData[asteroidType].Speed;
        }
        asteroid.GetComponent<Rigidbody2D>().velocity = velocity;
        return asteroid;
    }

    void OnTriggerExit2D(Collider2D collider){
        if (collider.tag == "Missile") {
            return;
        }
        Vector2 pos = collider.transform.position;
        /*Debug.Log("Asteroids coord: [" + pos.x + ", " + pos.y + "] " + "[ " + realCoord.x + ", " + realCoord.y + ", "
        + (realCoord.x + spawnAreaCollider.bounds.size.x) + ", " + (realCoord.y + spawnAreaCollider.bounds.size.y) + "]");*/
        float newX = pos.x;
        float newY = pos.y;
        if (pos.x >= realCoord.x + spawnAreaCollider.bounds.size.x) {
            newX = realCoord.x;
        }
        if (pos.x <= realCoord.x) {
            newX = realCoord.x + spawnAreaCollider.bounds.size.x;
        }
        if (pos.y >= realCoord.y + spawnAreaCollider.bounds.size.y) {
            newY = realCoord.y;
        }
        if (pos.y <= realCoord.y) {
            newY = realCoord.y + spawnAreaCollider.bounds.size.y;
        }
        collider.transform.position = new Vector2(newX, newY);
    }
}
