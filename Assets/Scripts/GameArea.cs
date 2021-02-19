using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameArea : MonoBehaviour {
    public GameObject progectileParent;
    public AsteroidsManager asteroidsManager;

    public Text scoreText;
    public LayoutGroup lifes;
    public SpriteRenderer lifeSprite;

    private Player player;

    Dictionary<AsteroidType, AsteroidData> asteroidTypeData = new Dictionary<AsteroidType, AsteroidData>{
        {AsteroidType.Huge, new AsteroidData(AsteroidType.Huge, 5, 0.5f)},
        {AsteroidType.Large,new AsteroidData(AsteroidType.Large, 10, 0.5f)},
        {AsteroidType.Medium,new AsteroidData(AsteroidType.Medium, 20, 0.5f)},
        {AsteroidType.Small,new AsteroidData(AsteroidType.Small, 40, 0.5f)},
    };

    int playerScore = 0;
    int playerLifes = 3;

    void Start()
    {
        Init();
    }

    void Init(){
        EventManager.EventBus.Subscribe<AsteroidDestroyEvent>(OnAsteroidDestroy);
        EventManager.EventBus.Subscribe<ShipCollisionEvent>(OnShipCollision);
        scoreText.text = "0";

        player = FindObjectOfType<Player>();
        player.ProjectileParent = progectileParent;
        InitLifeCounter();
    }

    void InitLifeCounter(){
        for (int i = 0; i < playerLifes; i++) {
            SpriteRenderer sprite = Instantiate(lifeSprite);
            sprite.gameObject.SetActive(true);
            sprite.transform.SetParent(lifes.transform);
        }
    }

    void OnAsteroidDestroy(AsteroidDestroyEvent asteroidDestroyEvent){
        playerScore += asteroidTypeData[asteroidDestroyEvent.Asteroid.Type].Score;
        scoreText.text = playerScore.ToString();
    }

    void OnShipCollision(ShipCollisionEvent shipCollisionEvent){
        Debug.Log("ShipCollisionEvent:" + playerLifes);
        playerLifes--;
        Destroy(lifes.transform.GetChild(lifes.transform.childCount-1).gameObject);
        if (playerLifes < 0) {
            GameOver();
        }
    }

    void GameOver(){

    }

    void OnTriggerExit2D(Collider2D collider){
        Destroy(collider.gameObject);
    }
}
