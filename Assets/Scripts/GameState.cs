using System;
using System.Collections.Generic;
using UnityEngine.AI;

[Serializable]
public class GameState
{
    public long time;
    public CommandsManager commandsManager;
    public List<NavMeshAgent> selectedActors;
    public GameState()
    {
       selectedActors = new List<NavMeshAgent>();
       commandsManager = new CommandsManager(this);
    }
}
