using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[Serializable]
public class GameState
{
    public long time;
    public List<GameObject> selectedActors;
    public GameState()
    {
       selectedActors = new List<GameObject>();
    }
}
