using System.Runtime.CompilerServices;
using Pixeye.Actors;
using Unity.IL2CPP.CompilerServices;
using UnityEngine;

namespace Actors.Command.Components
 {
   public class SpawnCommand
   {
     public Vector3 position;
   }

   #region HELPERS

   [Il2CppSetOption(Option.NullChecks, false)]
   [Il2CppSetOption(Option.ArrayBoundsChecks, false)]
   [Il2CppSetOption(Option.DivideByZeroChecks, false)]
   static partial class Component
   {
     public const string Spawn = "Actors.Command.Components.SpawnCommand";
     [MethodImpl(MethodImplOptions.AggressiveInlining)]
     public static ref SpawnCommand SpawnCommand(in this ent entity) =>
       ref Storage<SpawnCommand>.components[entity.id];
   }

   [Il2CppSetOption(Option.NullChecks, false)]
   [Il2CppSetOption(Option.ArrayBoundsChecks, false)]
   [Il2CppSetOption(Option.DivideByZeroChecks, false)]
   sealed class StorageSpawnCommand : Storage<SpawnCommand>
   {
     public override SpawnCommand Create() => new SpawnCommand();
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

