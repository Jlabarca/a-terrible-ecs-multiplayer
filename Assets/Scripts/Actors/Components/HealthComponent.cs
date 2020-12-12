using System.Runtime.CompilerServices;
using Pixeye.Actors;
using Unity.IL2CPP.CompilerServices;

namespace Actors.Components
 {
   public class HealthComponent
   {
     public int count;
   }

   #region HELPERS

   [Il2CppSetOption(Option.NullChecks, false)]
   [Il2CppSetOption(Option.ArrayBoundsChecks, false)]
   [Il2CppSetOption(Option.DivideByZeroChecks, false)]
   static partial class Component
   {
     public const string Health = "Game.Source.HealthComponent";
     [MethodImpl(MethodImplOptions.AggressiveInlining)]
     public static ref HealthComponent HealthComponent(in this ent entity) =>
       ref Storage<HealthComponent>.components[entity.id];
   }

   [Il2CppSetOption(Option.NullChecks, false)]
   [Il2CppSetOption(Option.ArrayBoundsChecks, false)]
   [Il2CppSetOption(Option.DivideByZeroChecks, false)]
   sealed class StorageHealthComponent : Storage<HealthComponent>
   {
     public override HealthComponent Create() => new HealthComponent();
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

