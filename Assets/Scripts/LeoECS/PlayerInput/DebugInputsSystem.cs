using LeoECS.Unit;
using Leopotam.Ecs;
using UnityEngine;

namespace LeoECS.PlayerInput
{
    public sealed class DebugInputsSystem : IEcsRunSystem {
        private GameState gameState;
        private EcsWorld _world;

        public void Run() {

            if (Input.GetKeyUp(KeyCode.S))
            {
                gameState.commandsManager.BackTrack();
            }


            if (
                Input.GetKey(KeyCode.LeftShift) &&
                Input.GetMouseButtonDown(1) &&
                Physics.Raycast(Camera.main.ScreenPointToRay(new Vector2(Input.mousePosition.x, Input.mousePosition.y)),
                    out var hit)
            )
            {
                Debug.Log("spawn at "+hit.point);
                for (int i = 0; i < 100; i++)
                {
                    var actorEntity = _world.NewEntity();
                    // ref var actorComponent = ref actorEntity.Get<UnitComponent>();
                    // actorComponent.hp = 101;
                    // actorComponent.spawnPosition = hit.point;
                    actorEntity
                        .Replace( new UnitComponent
                        {
                            Hp = 101,
                            SpawnPosition = hit.point,
                        });
                }
                //actorEntity.Get<UnitSpawnCommand>();
            }


        }
    }
}
