using LeoECS.Monobehaviours;
using UnityEngine;

namespace LeoECS.Actor
{
    public struct ActorComponent
    {
        public int Hp;
        public long DeathTime;
        public Vector3 SpawnPosition;
        public ActorView ActorView;
        public ActorState ActorState;
    }
    public enum ActorState
    {
        Idle,
        Guarding,
        Attacking,
        MovingToTarget,
        MovingToSpotIdle,
        MovingToSpotGuard,
        Dead,
    }
}
