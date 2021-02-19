public class AsteroidData {
    public AsteroidType Type {get; set;}
    public int Score {get; set;}
    public float Speed {get; set;}

    public AsteroidData(AsteroidType type, int score, float speed){
        Type = type;
        Score = score;
        Speed = speed;
    }
}
