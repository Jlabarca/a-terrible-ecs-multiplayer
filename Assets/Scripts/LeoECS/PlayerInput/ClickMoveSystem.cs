using LeoECS.Command;
using Leopotam.Ecs;
using UnityEngine;

namespace LeoECS.PlayerInput
{
    public sealed class ClickMoveSystem : IEcsRunSystem {
        private readonly EcsWorld ecsWorld = null;
        private GameState gameState;

        public void Run() {
            var mousePosition = Input.mousePosition;

            if (
                !Input.GetMouseButtonDown(1) ||
                !Physics.Raycast(Camera.main.ScreenPointToRay(new Vector2(mousePosition.x, mousePosition.y)), out var hit)
            ) return;

            // Creates new entity in world context.
            var entity = ecsWorld.NewEntity ();
            entity.Get<CommandComponent>().command = new MoveCommand(gameState.selectedActors, hit.point);
        }
    }
}
