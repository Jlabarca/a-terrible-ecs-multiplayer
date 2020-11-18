using LeoECS.Actor;
using LeoECS.Monobehaviours;
using LeoECS.Nav;
using Leopotam.Ecs;

namespace LeoECS.Pooling
{
    public class ActorsPool : GenericObjectPool<ActorView>
    {
        public override void ReturnToPool(ActorView objectToReturn)
        {
            var entity = objectToReturn.Entity;
            entity.Del<ActorComponent>();
            //entity.Del<ActorViewRef>();
            entity.Del<NavigationComponent>();
            base.ReturnToPool(objectToReturn);
        }
    }
}
