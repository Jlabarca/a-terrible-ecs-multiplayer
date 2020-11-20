using LeoECS.Actor;
using Leopotam.Ecs;
using UnityEngine;

namespace LeoECS.Nav
{
    public class DynamicNavSystem : IEcsRunSystem
    {
        private EcsFilter<ActorComponent, NavigationComponent> _actors;

        public void Run()
        {
            foreach (var index in _actors)
            {
                var navigationComponent = _actors.Get2(index);
                var navMeshAgent = navigationComponent.navMeshAgent;
                if (!navMeshAgent.isStopped)
                {
                    var actorComponent = _actors.Get1(index);
                    if (Vector3.Distance(actorComponent.ActorView.transform.position, navMeshAgent.destination) < 2
                    //&& navMeshAgent.radius < 0.5f
                    && navMeshAgent.stoppingDistance < 4)
                    {
                        navMeshAgent.radius += 0.0035f;
                        navMeshAgent.stoppingDistance += 0.1f;
                    }
                }
            }
        }
    }
}
