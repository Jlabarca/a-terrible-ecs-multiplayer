using System;
using System.Collections.Generic;
using UnityEngine.AI;

[Serializable]
public class GameState
{
    public long time;
    public List<NavMeshAgent> selectedActors;
    public GameState()
    {
       selectedActors = new List<NavMeshAgent>();
    }
}
