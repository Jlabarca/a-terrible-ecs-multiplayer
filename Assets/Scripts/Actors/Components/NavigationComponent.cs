using System.Runtime.CompilerServices;
using Pixeye.Actors;
using Unity.IL2CPP.CompilerServices;
using UnityEngine.AI;

namespace Actors.Components
{
    public class NavigationComponent
    {
        public NavMeshAgent navMeshAgent;
    }

    #region HELPERS

    [Il2CppSetOption(Option.NullChecks, false)]
    [Il2CppSetOption(Option.ArrayBoundsChecks, false)]
    [Il2CppSetOption(Option.DivideByZeroChecks, false)]
    static partial class Component
    {
        public const string Movement = "Actors.Components.NavigationComponent";
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ref NavigationComponent NavigationComponent(in this ent entity) =>
            ref Storage<NavigationComponent>.components[entity.id];
    }

    [Il2CppSetOption(Option.NullChecks, false)]
    [Il2CppSetOption(Option.ArrayBoundsChecks, false)]
    [Il2CppSetOption(Option.DivideByZeroChecks, false)]
    sealed class StorageMovementComponent : Storage<NavigationComponent>
    {
        public override NavigationComponent Create() => new NavigationComponent();
        // Use for cleaning components that were removed at the current frame.
        public override void Dispose(indexes disposed)
        {
            foreach (var id in disposed)
            {
                ref var component = ref components[id];
            }
        }
    }

    #endregion
}
