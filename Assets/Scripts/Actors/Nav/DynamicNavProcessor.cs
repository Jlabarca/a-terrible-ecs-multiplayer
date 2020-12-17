using Actors.Components;
using Pixeye.Actors;
using UnityEngine;

namespace Actors.Nav
{
    public class DynamicNavProcessor : Processor, ITick
    {
        private readonly Group<UnitComponent, NavigationComponent> units = default;

        public void Tick(float dt)
        {
            foreach (var unitEntity in units)
            {
                var navigationComponent = unitEntity.Get<NavigationComponent>();
                var navMeshAgent = navigationComponent.navMeshAgent;
                if (!navMeshAgent.isStopped)
                {
                    //var unitComponent = unitEntity.Get<UnitComponent>();
                    if (Vector3.Distance(unitEntity.transform.position, navMeshAgent.destination) < 6
                        //&& navMeshAgent.radius < 0.5f
                        && navMeshAgent.stoppingDistance < 6)
                    {
                        navMeshAgent.radius += 0.0055f;
                        navMeshAgent.stoppingDistance += 0.1f;
                    }
                }
            }
        }
    }
}
