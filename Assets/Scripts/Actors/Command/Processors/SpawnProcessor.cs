using Actors.Command.Components;
using Actors.Components;
using Pixeye.Actors;
using UnityEngine;
using UnityEngine.AI;

namespace Actors.Command.Processors
{
  internal sealed class SpawnProcessor : Processor, ITickFixed
  {
    private readonly Group<SpawnCommand> spawnCommands = default;
    private GameObject prefab;

    public SpawnProcessor()
    {
      prefab = Box.Load<GameObject>("WhiteBall");
      Layer.Pool[Pool.Entities].PopulateWith(prefab, 1000, 1000);
    }
    // public override void HandleEcsEvents()
    // {
    //   foreach (var e in objects.added)
    //   {
    //     Debug.Log($"{e} was added!");
    //   }
    //
    //   foreach (var e in objects.removed)
    //   {
    //     Debug.Log($"{e} was removed!");
    //   }
    // }

    public void TickFixed(float dt)
    {
      foreach (var spawnCommandEntity in spawnCommands)
      {

        //var entity = Actor.Create(prefab);  //?? difference
        var spawnCommand = spawnCommandEntity.SpawnCommand();
        var spawnPosition = spawnCommand.position;

        for (int i = 0; i < 100; i++)
        {
          var newEntity = Layer.Entity.Create(prefab, spawnPosition, true);
          newEntity.Set<UnitComponent>();
          if (newEntity.transform.TryGetComponent<NavMeshAgent>(out var navMeshAgent)){
            var navigation = newEntity.Set<NavigationComponent>();
            navigation.navMeshAgent = navMeshAgent;
          }
        }

        spawnCommandEntity.Remove<SpawnCommand>();
      }
    }
  }
}
