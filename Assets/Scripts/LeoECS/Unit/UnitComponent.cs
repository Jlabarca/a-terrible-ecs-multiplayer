using LeoECS.Monobehaviours;
using UnityEngine;

namespace LeoECS.Unit
{
    public struct UnitComponent
    {
        public int Hp;
        public long DeathTime;
        public Vector3 SpawnPosition;
        public UnitView unitView;
        public UnitState unitState;
    }

    public enum UnitState
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
