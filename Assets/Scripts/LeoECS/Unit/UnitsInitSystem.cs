using LeoECS.Monobehaviours;
using LeoECS.Nav;
using Leopotam.Ecs;
using UnityEngine;
using UnityEngine.AI;

namespace LeoECS.Unit
{
    public class UnitsInitSystem : IEcsInitSystem
    {
        private EcsWorld _world;

        public void Init()
        {
            Debug.Log("UnitsInitSystem");
            var playerObjects = GameObject.FindGameObjectsWithTag("Locals");
            foreach (var actor in playerObjects) {
                var actorEntity = _world.NewEntity();
                actorEntity
                    .Replace(new UnitComponent
                    {
                        Hp = 100,
                        SpawnPosition = actor.transform.position,
                        unitView = actor.GetComponent<UnitView>()
                    });

                if(actor.TryGetComponent<NavMeshAgent>(out var navMeshAgent))
                    actorEntity.Replace(new NavigationComponent {
                        navMeshAgent = navMeshAgent
                    });
            }

            Debug.Log(playerObjects.Length);
        }
    }
}
