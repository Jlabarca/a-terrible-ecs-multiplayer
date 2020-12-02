using LeoECS.Monobehaviours;
using LeoECS.Nav;
using LeoECS.Unit;
using Leopotam.Ecs;

namespace LeoECS.Pooling
{
    public class UnitsPool : GenericObjectPool<UnitView>
    {
        public override void ReturnToPool(UnitView objectToReturn)
        {
            var entity = objectToReturn.Entity;
            entity.Del<UnitComponent>();
            //entity.Del<ActorViewRef>();
            entity.Del<NavigationComponent>();
            base.ReturnToPool(objectToReturn);
        }
    }
}
