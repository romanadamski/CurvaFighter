using UnityEngine;

public class GameplayManager : BaseManager<GameplayManager>
{
    #region States

    private StateMachine _gameplayStateMachine;

    public GameplayState GameplayState { get; private set; }
    public WinState WinState { get; private set; }
    public LoseState LoseState { get; private set; }
    public IdleState IdleState { get; private set; }
    public EndGameplayState EndGameplayState { get; private set; }

    #endregion
    public bool IsPaused => Time.deltaTime == 0;

    private void Awake()
    {
        InitStates();
    }

    private void Start()
    {
        SubscribeToEvents();
    }

    private void InitStates()
    {
        _gameplayStateMachine = gameObject.AddComponent<StateMachine>();

        GameplayState = new GameplayState(_gameplayStateMachine);
        WinState = new WinState(_gameplayStateMachine);
        LoseState = new LoseState(_gameplayStateMachine);
        IdleState = new IdleState(_gameplayStateMachine);
        EndGameplayState = new EndGameplayState(_gameplayStateMachine);
    }

    private void SubscribeToEvents()
    {
        EventsManager.Instance.PlayerLoseLife += PlayerLoseLife;
    }

    private void PlayerLoseLife(uint lives)
    {
        if (lives == 0)
        {
            OnPlayerLose();
        }
        else
        {
            _gameplayStateMachine.PushState(IdleState);
        }
    }

    private void OnPlayerLose()
    {
        ClearGameplay();

        _gameplayStateMachine.SetState(LoseState);
    }

    public void ClearGameplay()
    {
        ObjectPoolingManager.Instance.ReturnAllToPools();
        AsteroidReleasingManager.Instance.StopReleasingAsteroidsCoroutine();
    }

    public void StartCurrentLevel()
    {
        LevelSettingsManager.Instance.SetCurrentLevel();

        EventsManager.Instance.OnLevelStarted(LevelSettingsManager.Instance.CurrentLevel);
    }

    public void StartGameplay()
    {
        ResumeGameplay();

        EventsManager.Instance.OnGameplayStarted();
    }

    public void SetGameplayState()
    {
        _gameplayStateMachine.SetState(GameplayState);
    }

    public void SetEndGameplayState()
    {
        _gameplayStateMachine.SetState(EndGameplayState);
    }

    public void ClearGameplayStateMachine()
    {
        ClearGameplay();
        _gameplayStateMachine.Clear();
    }

    public void PauseGameplay()
    {
        Time.timeScale = 0;
    }

    public void ResumeGameplay()
    {
        Time.timeScale = 1;
    }
}
