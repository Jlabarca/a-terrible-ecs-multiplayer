using Actors.Components;
using Pixeye.Actors;

namespace Actors.Unit
{
    public class UnitActor : Actor
    {
        // you are not forced to use attribute.
        //[FoldoutGroup("Components", true)]
        public HealthComponent healthComponent;
        public NavigationComponent navigationComponent;

        // use instead of the start method to initialize the entity.
        protected override void Setup()
        {
            entity.Set(healthComponent);
            entity.Set(navigationComponent);
        }
    }
}
