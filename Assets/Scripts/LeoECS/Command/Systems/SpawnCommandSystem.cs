using LeoECS.Command.Components;
using LeoECS.Monobehaviours;
using LeoECS.Nav;
using LeoECS.Pooling;
using LeoECS.Unit;
using Leopotam.Ecs;
using UnityEngine.AI;

namespace LeoECS.Command.Systems
{
    public class SpawnCommandSystem : IEcsRunSystem
    {
        private EcsWorld world;
        private UnitsPool unitsPool;

        private EcsFilter<SpawnCommand> filter;

        public void Run()
        {
            foreach (var index in filter)
            {
                ref var spawnCommand = ref filter.Get1(index);
                var actorView = unitsPool.Get();
                actorView.transform.position = spawnCommand.position;

                var actorEntity = world.NewEntity();
                actorEntity
                    .Replace(new UnitComponent
                    {
                        Hp = 100,
                        SpawnPosition = spawnCommand.position,
                        unitView = actorView.GetComponent<UnitView>(),
                    });

                if(actorView.TryGetComponent<NavMeshAgent>(out var navMeshAgent))
                    actorEntity.Replace(new NavigationComponent {
                        navMeshAgent = navMeshAgent
                    });

                actorView.Entity = actorEntity;
                actorView.gameObject.SetActive(true);

                filter.GetEntity(index).Destroy();
            }
        }
    }
}
