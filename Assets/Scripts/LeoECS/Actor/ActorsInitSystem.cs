using LeoECS.Monobehaviours;
using LeoECS.Nav;
using Leopotam.Ecs;
using UnityEngine;
using UnityEngine.AI;

namespace LeoECS.Actor
{
    public class ActorsInitSystem : IEcsInitSystem
    {
        private EcsWorld _world;

        public void Init()
        {
            Debug.Log("ActorsInitSystem");
            var playerObjects = GameObject.FindGameObjectsWithTag("Locals");
            foreach (var actor in playerObjects) {
                var actorEntity = _world.NewEntity();
                actorEntity
                    .Replace(new ActorComponent
                    {
                        Hp = 100,
                        SpawnPosition = actor.transform.position,
                        ActorView = actor.GetComponent<ActorView>()
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
