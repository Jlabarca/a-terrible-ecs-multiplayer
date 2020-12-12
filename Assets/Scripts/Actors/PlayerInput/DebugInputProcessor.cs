using Actors.Command.Components;
using Pixeye.Actors;
using UnityEngine;

namespace Actors.PlayerInput
{
    public class DebugInputProcessor : Processor, ITick
    {
        // Group<UnitComponent> objects;
        //
        // public override void HandleEcsEvents()
        // {
        //     foreach (var e in objects.added)
        //     {
        //         Debug.Log($"{e} was added!");
        //     }
        //
        //     foreach (var e in objects.removed)
        //     {
        //         Debug.Log($"{e} was removed!");
        //     }
        // }

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
                var spawnCommand = Entity.Create().Set<SpawnCommand>();
                spawnCommand.position = hit.point;
            }
        }
    }
}
