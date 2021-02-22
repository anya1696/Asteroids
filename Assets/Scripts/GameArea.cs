using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/**
 * Основное управление игрой. Считает очки, жизни игрока, заканчивает игру.
*/

public class GameArea : MonoBehaviour {
    public AsteroidsManager asteroidsManager;

    public Text scoreText;
    public LayoutGroup lifes;
    public SpriteRenderer lifeSprite;

    public GameObject gameOverScreen;

    public float respawnTime = 5f;
    public int playerScore = 0;
    public int playerLifes = 3;

    public GameObject myPlayer;
    bool isPlayerReapawning = false;
    float timeToRespawn;
    bool isGameOver = false;
    int scoreToLifeRestoreCounter = 0;
    int scoreToLifeRestoreStep = 150;

    //как бы данные астероидов взятые из настроек
    static Dictionary<AsteroidType, AsteroidData> asteroidTypeData = new Dictionary<AsteroidType, AsteroidData>{
        {AsteroidType.Huge, new AsteroidData(AsteroidType.Huge, 5, 0.5f, AsteroidType.Large, 2)},
        {AsteroidType.Large,new AsteroidData(AsteroidType.Large, 10, 1f, AsteroidType.Small, 2)},
        {AsteroidType.Medium,new AsteroidData(AsteroidType.Medium, 20, 1.5f, AsteroidType.Small, 2)},
        {AsteroidType.Small,new AsteroidData(AsteroidType.Small, 40, 2f, AsteroidType.Small, 0)},
    };

    void Start()
    {
        Init();
    }

    void Update()
    {
        if (isPlayerReapawning) {
            timeToRespawn -= Time.deltaTime;
            if (timeToRespawn <= 0.0f) {
                RespawnPlayer();
            }
        }
    }

    void Init(){
        timeToRespawn = respawnTime;

        EventManager.EventBus.Subscribe<AsteroidDestroyEvent>(OnAsteroidDestroy);
        EventManager.EventBus.Subscribe<ShipCollisionAsteroidEvent>(OnShipCollisionAsteroid);
        scoreText.text = "0";

        InitLifeCounter();
    }

    void InitLifeCounter(){
        for (int i = 0; i < playerLifes; i++) {
            CreateLifeSprite();
        }
    }

    void OnAsteroidDestroy(AsteroidDestroyEvent asteroidDestroyEvent){
        int asteroidScore = asteroidTypeData[asteroidDestroyEvent.Asteroid.Type].Score;
        playerScore += asteroidScore;
        scoreToLifeRestoreCounter += asteroidScore;
        if (scoreToLifeRestoreCounter >= scoreToLifeRestoreStep) {
            CreateLifeSprite();
            playerLifes++;
            scoreToLifeRestoreCounter = scoreToLifeRestoreCounter - scoreToLifeRestoreStep;
        }
        scoreText.text = playerScore.ToString();
    }

    void OnShipCollisionAsteroid(ShipCollisionAsteroidEvent shipCollisionEvent){
        playerLifes--;
        MoveToRespawnPlayer();
        if (playerLifes < 0) {
            GameOver();
        }
    }

    void GameOver(){
        isGameOver = true;
        gameOverScreen.SetActive(true);
        this.enabled = false;
    }

    void OnTriggerExit2D(Collider2D collider){
        if (collider.tag == "Missile") {
            Destroy(collider.gameObject);
        }
    }

    public static Dictionary<AsteroidType, AsteroidData> AsteroidTypeData {
        get {
            return asteroidTypeData;
        }
    }

    void RespawnPlayer(){
        if (isGameOver) {
            return;
        }
        myPlayer.SetActive(true);
        myPlayer.transform.position = Vector3.zero;
        isPlayerReapawning = false;
    }

    void MoveToRespawnPlayer(){
        Destroy(lifes.transform.GetChild(lifes.transform.childCount - 1).gameObject);
        myPlayer.SetActive(false);
        isPlayerReapawning = true;
        timeToRespawn = respawnTime;
    }

    void CreateLifeSprite(){
        SpriteRenderer sprite = Instantiate(lifeSprite);
        sprite.gameObject.SetActive(true);
        sprite.transform.SetParent(lifes.transform);
    }
}
