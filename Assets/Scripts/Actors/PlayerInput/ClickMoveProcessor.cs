using Pixeye.Actors;
using UnityEngine;
using MoveCommand = Actors.Command.Components.MoveCommand;

namespace Actors.PlayerInput
{
    public sealed class ClickMoveProcessor : Processor, ITick
    {
        public void Tick(float dt)
        {
            var mousePosition = Input.mousePosition;

            if (
                !Input.GetMouseButtonDown(1) ||
                !Physics.Raycast(Camera.main.ScreenPointToRay(new Vector2(mousePosition.x, mousePosition.y)), out var hit)
            ) return;

            Debug.Log("Move to "+hit.point);
            var moveCommand = Entity.Create().Set<MoveCommand>();
            moveCommand.targetPosition = hit.point;
            moveCommand.selectedActors = Layer.Get<GameState>().selectedActors;
        }
    }
}
