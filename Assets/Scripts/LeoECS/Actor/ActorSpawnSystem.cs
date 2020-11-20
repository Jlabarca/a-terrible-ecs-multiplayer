using LeoECS.Nav;
using LeoECS.Pooling;
using Leopotam.Ecs;
using UnityEngine.AI;

namespace LeoECS.Actor
{
    public class ActorSpawnSystem : IEcsRunSystem
    {

        private EcsWorld world;
        private ActorsPool actorsPool;

        private EcsFilter<ActorSpawnEvent, ActorComponent> _filter;
        private EcsFilter<ActorComponent>.Exclude<NavigationComponent> _filter0;

        public void Run()
        {
            foreach (var index in _filter0)
            {
                _filter0.GetEntity(index).Get<ActorSpawnEvent>();
            }

            foreach (var index in _filter)
            {
                ref var actorComponent = ref _filter.Get2(index);
                var actorView = actorsPool.Get();
                actorView.transform.position = actorComponent.SpawnPosition;
                actorComponent.ActorView = actorView;

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
