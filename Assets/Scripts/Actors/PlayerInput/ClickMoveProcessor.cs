using System.Linq;
using Online;
using Pixeye.Actors;
using UnityEngine;
using MoveCommand = Actors.Command.Components.MoveCommand;

namespace Actors.PlayerInput
{
    public sealed class ClickMoveProcessor : Processor, ITick
    {
        private readonly GameState gameState;
        private readonly LiteNetLibNetwork network;

        public ClickMoveProcessor()
        {
            gameState = Layer.Get<GameState>();
            network = Layer.Get<LiteNetLibNetwork>();
        }

        public void Tick(float dt)
        {
            var mousePosition = Input.mousePosition;

            if (
                !Input.GetMouseButtonDown(1) ||
                gameState.selectedActors.Count == 0 ||
                !Physics.Raycast(Camera.main.ScreenPointToRay(new Vector2(mousePosition.x, mousePosition.y)) , out var hit)
            ) return;

            var moveCommand = new MoveCommand {position = hit.point, units = gameState.selectedActors.ToArray()};
            network.Send(moveCommand);
            //Entity.Create().Set(moveCommand);
        }
    }
}
