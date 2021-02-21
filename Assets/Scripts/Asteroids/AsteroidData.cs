/**
 * Класс с данными. Нужен для симуляции хранения сторонних настроек астеройдов
*/

public class AsteroidData {
    public AsteroidType Type { get; set; }
    public int Score { get; set; }
    public float Speed { get; set; }
    public AsteroidType DestroyToType { get; set; }
    public int DestroyToCount { get; set; }

    public AsteroidData(AsteroidType type, int score, float speed, AsteroidType destroyToType, int destroyToCount){
        Type = type;
        Score = score;
        Speed = speed;
        DestroyToType = destroyToType;
        DestroyToCount = destroyToCount;
    }
}
