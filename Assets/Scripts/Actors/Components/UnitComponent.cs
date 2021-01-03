using System.Runtime.CompilerServices;
using Pixeye.Actors;
using Unity.IL2CPP.CompilerServices;

namespace Actors.Components
 {
   public class UnitComponent
   {
     public byte playerId;
     public uint unitId;
     public int health = 10;
   }

   #region HELPERS

   [Il2CppSetOption(Option.NullChecks, false)]
   [Il2CppSetOption(Option.ArrayBoundsChecks, false)]
   [Il2CppSetOption(Option.DivideByZeroChecks, false)]
   static partial class Component
   {
     public const string Unit = "Game.Source.UnitComponent";
     [MethodImpl(MethodImplOptions.AggressiveInlining)]
     public static ref UnitComponent UnitComponent(in this ent entity) =>
       ref Storage<UnitComponent>.components[entity.id];
   }

   [Il2CppSetOption(Option.NullChecks, false)]
   [Il2CppSetOption(Option.ArrayBoundsChecks, false)]
   [Il2CppSetOption(Option.DivideByZeroChecks, false)]
   sealed class StorageUnitComponent : Storage<UnitComponent>
   {
     public override UnitComponent Create() => new UnitComponent();
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

