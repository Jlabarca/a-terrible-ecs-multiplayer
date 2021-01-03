using Actors.Components;
using Pixeye.Actors;
using UnityEngine;

namespace Actors.Nav
{
    public class DynamicNavProcessor : Processor, ITick
    {
        private readonly Group<UnitComponent, NavigationComponent> units = default;
        private readonly GameState gameState;
        public DynamicNavProcessor()
        {
            gameState = Layer.Get<GameState>();
        }

        /*
         * Trying to add a better looking navigation
         * by adding radius while moving and stoppingDistance depending on mob size
         */
        public void Tick(float dt)
        {
            var sizeFactor = Mathf.Log(gameState.selectedActors.Count) + 4;

            foreach (var unitEntity in units)
            {
                var navigationComponent = unitEntity.Get<NavigationComponent>();
                var navMeshAgent = navigationComponent.navMeshAgent;
                if (!navMeshAgent.isStopped)
                {
                    var distance = Vector3.Distance(unitEntity.transform.position, navMeshAgent.destination);
                    if (distance < sizeFactor
                        && navMeshAgent.stoppingDistance < sizeFactor)
                    {
                        navMeshAgent.stoppingDistance += 0.1f;
                    }

                    if (distance > sizeFactor)
                    {
                        if (navMeshAgent.radius < 0.5f)
                            navMeshAgent.radius += 0.002f;
                    }
                    else
                    {
                        navMeshAgent.radius  = 0.1f;
                    }
                }
            }
        }
    }
}
