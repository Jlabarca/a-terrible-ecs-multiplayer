using System;
using System.Collections.Generic;
using Commands;
using UnityEngine;

[Serializable]
public class CommandsManager
{
    private GameState gameState;
    [SerializeField]
    public List<Command> commands;
    [SerializeField]
    private int currentCommandIndex;

    private bool paused;

    public CommandsManager(GameState gameState)
    {
        this.gameState = gameState;
        commands = new List<Command>();
    }

    public void AddCommand(Command command)
    {
        commands.Add(command);
    }

    public void ExecuteCommand()
    {
        if (!paused && commands.Count > currentCommandIndex)
        {
            var command = commands[currentCommandIndex++];
            Debug.Log("Executing command "+command.GetType().Name);
            command.executionTimeInMillis = gameState.time;
            command.Execute();
        }
    }

    public void BackTrack()
    {
        if(currentCommandIndex < 1) return;
        paused = true;
        currentCommandIndex--;
        var command = commands[currentCommandIndex];
        command.Undo();
        commands.Remove(command);
        gameState.time = command.executionTimeInMillis;
        paused = false;
    }
}
