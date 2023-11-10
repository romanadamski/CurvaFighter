using UnityEngine;

[CreateAssetMenu(fileName = "Level", menuName = "ScriptableObjects/Level Settings")]
public class LevelSettingsSO : ScriptableObject
{
    [SerializeField]
    private uint levelNumber;
    public uint LevelNumber => levelNumber;
    [SerializeField]
    private Sprite backgroundImage;
    public Sprite BackgroundSprite => backgroundImage;

    [Header("Player settings")]
    [SerializeField]
    private bool activatePlayer = true;
    public bool ActivatePlayer => activatePlayer;

    [Header("Asteroids settings")]
    [SerializeField]
    private SerializableTuple<float, float> asteroidsReleasingFrequency;
    public SerializableTuple<float, float> AsteroidsReleasingFrequency => asteroidsReleasingFrequency;

    [SerializeField]
    private SerializableTuple<float, float> asteroidsSpeedRange;
    public SerializableTuple<float, float> AsteroidsSpeedRange => asteroidsSpeedRange;
}
