using System;

public static class GameInfo
{
    public static GameType currentGameType;
    
    public enum GameType
    {
        Tutorial,
        Mission01,
        Mission02,
    }
}