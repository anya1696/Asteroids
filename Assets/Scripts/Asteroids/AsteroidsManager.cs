using System.Collections.Generic;
using UnityEngine;

public class AsteroidsManager : MonoBehaviour {
    public Asteroid AsteroidHuge;
    public Asteroid AsteroidLarge;
    public Asteroid AsteroidMedium;
    public Asteroid AsteroidSmall;

    Vector2 realCoord;
    Collider2D collider;

    Dictionary<AsteroidType, Asteroid> asteroidPrefabs = new Dictionary<AsteroidType, Asteroid>();


    void Start()
    {
        Init();
    }

    void Init()
    {
        EventManager.EventBus.Subscribe<AsteroidDestroyEvent>(OnAsteroidDestroy);

        asteroidPrefabs.Add(AsteroidType.Huge, AsteroidHuge);
        asteroidPrefabs.Add(AsteroidType.Large, AsteroidLarge);
        asteroidPrefabs.Add(AsteroidType.Medium, AsteroidMedium);
        asteroidPrefabs.Add(AsteroidType.Small, AsteroidSmall);

        collider = GetComponent<Collider2D>();
        realCoord = new Vector2 (collider.transform.position.x - collider.bounds.size.x/2, collider.transform.position.y - collider.bounds.size.y/2);

        SpawnRandomAsteroid();
    }

    void Update()
    {

    }

    public void OnAsteroidDestroy(AsteroidDestroyEvent asteroidDestroyEvent){
        Asteroid asteroid = asteroidDestroyEvent.Asteroid;
//        if (asteroid.Type == AsteroidType.Huge){
//            SpawnAsteroid(AsteroidType.Large, Vector2.up);
//            SpawnAsteroid(AsteroidType.Small, Vector2.right);
//            SpawnAsteroid(AsteroidType.Small, Vector2.left);
//        }
    }

    void SpawnRandomAsteroid(){
        //Asteroid asteroid = SpawnAsteroid(AsteroidType.Huge, Vector2.one);

        SpawnAsteroid(AsteroidType.Huge, GetRandomSpawnPoint());
        SpawnAsteroid(AsteroidType.Large, GetRandomSpawnPoint());
        SpawnAsteroid(AsteroidType.Medium, GetRandomSpawnPoint());
        SpawnAsteroid(AsteroidType.Small, GetRandomSpawnPoint());
    }

    Vector2 GetRandomSpawnPoint(){
        int rand = Random.Range(0,3);
        Vector2 vector;
        if (rand == 0){
            vector = new Vector2(realCoord.x, Random.Range(realCoord.y ,realCoord.y + collider.bounds.size.y));
        }else if (rand == 1){
            vector = new Vector2(Random.Range(realCoord.x ,realCoord.y + collider.bounds.size.x), realCoord.y);
        }else if (rand == 2){
            vector = new Vector2(realCoord.x + collider.bounds.size.x, Random.Range(realCoord.y ,realCoord.y + collider.bounds.size.y));
        }else{
            vector = new Vector2(Random.Range(realCoord.x ,realCoord.y + collider.bounds.size.x), realCoord.y + collider.bounds.size.y);
        }

        return vector;

    }

    Asteroid SpawnAsteroid(AsteroidType asteroidType, Vector2 vector){
        Asteroid asteroid;
        Vector3 vector3 = vector;
        asteroid = Instantiate(asteroidPrefabs[asteroidType], vector3, Quaternion.identity);
        asteroid.gameObject.SetActive(true);
        asteroid.transform.SetParent(transform);
        return asteroid;
    }
}
