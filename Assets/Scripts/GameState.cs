using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class GameState
{
    public uint tick;
    public List<uint> selectedActors;
    public List<GameObject> selectedActorsGameObjects;
    public uint unitCount;

    public GameState()
    {
        selectedActors = new List<uint>();
        selectedActorsGameObjects = new List<GameObject>();
    }
}
