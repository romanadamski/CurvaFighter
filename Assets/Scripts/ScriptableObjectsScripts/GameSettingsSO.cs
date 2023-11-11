using UnityEngine;

[CreateAssetMenu(fileName = "GameSettings", menuName = "ScriptableObjects/Game Settings")]
public class GameSettingsSO : ScriptableObject
{
    [Header("Bullet settings")]
    [SerializeField]
    private float baseBulletMovementSpeed;
    public float BaseBulletMovementSpeed => baseBulletMovementSpeed;

    [Header("Asteroid settings")]
    [SerializeField]
    private float baseAsteroidMovementSpeed;
    public float BaseAsteroidMovementSpeed => baseAsteroidMovementSpeed;

    [SerializeField]
    private uint _asteroidShottedPoints;
    public uint AsteroidShottedPoints => _asteroidShottedPoints;

    [Header("Game settings")]
    [SerializeField]
    private float _idleStateTime;
    public float IdleStateTime => _idleStateTime;

    [SerializeField]
    private int _maxHighscoresSaveCount;
    public int MaxHighscoresSaveCount => _maxHighscoresSaveCount;
}
