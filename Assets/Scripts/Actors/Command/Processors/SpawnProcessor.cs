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
    private readonly GameObject prefab;
    private readonly GameState gameState;

    public SpawnProcessor()
    {
      gameState = Layer.Get<GameState>();
      prefab = Box.Load<GameObject>("WhiteBall");
      Layer.Pool[Pool.Entities].PopulateWith(prefab, 1000, 1000);
    }

    public void TickFixed(float dt)
    {
      foreach (var spawnCommandEntity in spawnCommands)
      {

        //var entity = Actor.Create(prefab);  //?? difference
        var spawnCommand = spawnCommandEntity.SpawnCommand();
        var spawnPosition = spawnCommand.position;

        var newEntity = Layer.Entity.Create(prefab, spawnPosition, true);
        var unitComponent = newEntity.Set<UnitComponent>();
        unitComponent.health = 100;
        unitComponent.playerId = spawnCommand.actorId;
        unitComponent.unitId = gameState.unitCount++;

        if (newEntity.transform.TryGetComponent<NavMeshAgent>(out var navMeshAgent)){
          var navigation = newEntity.Set<NavigationComponent>();
          navigation.navMeshAgent = navMeshAgent;
        }

        spawnCommandEntity.Remove<SpawnCommand>();
      }
    }
  }
}
