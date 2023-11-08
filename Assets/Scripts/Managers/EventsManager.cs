﻿using System;

public class EventsManager : BaseManager<EventsManager>
{
    public event Action<uint> PlayerLoseLife;
    public void OnPlayerLoseLife(uint livesCount)
    {
        PlayerLoseLife?.Invoke(livesCount);
    }

    public event Action<uint> PlayerSpawned;
    public void OnPlayerSpawned(uint livesCount)
    {
        PlayerSpawned?.Invoke(livesCount);
    }
    
    public event Action Score;
    public void OnScore()
    {
        Score?.Invoke();
    }
    
    public event Action<LevelSettingsSO> LevelStarted;
    public void OnLevelStarted(LevelSettingsSO levelNumber)
    {
        LevelStarted?.Invoke(levelNumber);
    }
    
    public event Action GameplayStarted;
    public void OnGameplayStarted()
    {
        GameplayStarted?.Invoke();
    }

    public event Action<uint> ScoreUpdated;
    public void OnScoreUpdated(uint score)
    {
        ScoreUpdated?.Invoke(score);
    }

    public event Action BulletFired;
    public void OnBulletFired()
    {
        BulletFired?.Invoke();
    }
}
