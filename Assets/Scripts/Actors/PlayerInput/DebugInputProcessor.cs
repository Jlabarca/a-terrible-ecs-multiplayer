using System.Collections.Generic;
using Actors.Command.Components;
using Online;
using Pixeye.Actors;
using UnityEngine;

namespace Actors.PlayerInput
{
    public class DebugInputProcessor : Processor, ITick
    {
        private readonly LiteNetLibNetwork network;

        public DebugInputProcessor()
        {
            network = Layer.Get<LiteNetLibNetwork>();
        }

        public void Tick(float dt)
        {
            if (
                Input.GetKey(KeyCode.LeftShift) &&
                Input.GetMouseButtonDown(1) &&
                Physics.Raycast(Camera.main.ScreenPointToRay(new Vector2(Input.mousePosition.x, Input.mousePosition.y)),
                    out var hit)
            )
            {
                Debug.Log("spawn at "+hit.point);
                var spawnCommand = new SpawnCommand {position = hit.point};
                network.Send(spawnCommand);
            }
        }
    }
}
