using LeoECS.Pooling;
using Leopotam.Ecs;

namespace LeoECS.Actor
{
    public class DeadActorsSystem : IEcsRunSystem
    {
        private GameState gameState;
        private ActorsPool actorsPool;

        private EcsFilter<ActorComponent> _actors;
        private const int  DeathExpirationTime = 1500;

        public void Run()
        {
            foreach (var index in _actors)
            {
                var actorComponent = _actors.Get1(index);
                if (actorComponent.Hp < 1)
                {
                    //Apply death time
                    if (actorComponent.DeathTime == default)
                    {
                        actorComponent.DeathTime = gameState.time;
                        return;
                    }

                    //Destroy on death expiration
                    if (actorComponent.DeathTime > gameState.time + DeathExpirationTime)
                    {
                        var entity = _actors.GetEntity(index);
                        //if(_actors.GetEntity(index).Has<>())
                        if(actorComponent.ActorView != null)
                            actorsPool.ReturnToPool(actorComponent.ActorView);

                        entity.Destroy();
                    }
                }
            }
        }
    }
}
