using LeoECS.Nav;
using LeoECS.Pooling;
using Leopotam.Ecs;
using UnityEngine.AI;

namespace LeoECS.Unit
{
    public class UnitSpawnSystem : IEcsRunSystem
    {

        private EcsWorld world;
        private UnitsPool unitsPool;

        private EcsFilter<UnitSpawnCommand, UnitComponent> _filter;
        private EcsFilter<UnitComponent>.Exclude<NavigationComponent> _filter0;

        public void Run()
        {
            foreach (var index in _filter0)
            {
                _filter0.GetEntity(index).Get<UnitSpawnCommand>();
            }

            foreach (var index in _filter)
            {
                ref var actorComponent = ref _filter.Get2(index);
                var actorView = unitsPool.Get();
                actorView.transform.position = actorComponent.SpawnPosition;
                actorComponent.unitView = actorView;

                //if (entity.Has<NavigationComponent>())
                var entity = _filter.GetEntity(index);
                entity.Replace(new NavigationComponent
                {
                    navMeshAgent = actorView.GetComponent<NavMeshAgent>()
                });

                actorView.Entity = entity;
                actorView.gameObject.SetActive(true);
            }
        }
    }
}
