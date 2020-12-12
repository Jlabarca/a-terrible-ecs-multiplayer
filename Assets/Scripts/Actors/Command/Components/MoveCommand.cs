using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Pixeye.Actors;
using Unity.IL2CPP.CompilerServices;
using UnityEngine;
using UnityEngine.AI;

namespace Actors.Command.Components
 {
   public class MoveCommand
   {
     public List<NavMeshAgent> selectedActors;
     public Vector3 targetPosition;
   }

   #region HELPERS

   [Il2CppSetOption(Option.NullChecks, false)]
   [Il2CppSetOption(Option.ArrayBoundsChecks, false)]
   [Il2CppSetOption(Option.DivideByZeroChecks, false)]
   static partial class Component
   {
     public const string Move = "Game.Source.MoveCommand";
     [MethodImpl(MethodImplOptions.AggressiveInlining)]
     public static ref MoveCommand MoveCommand(in this ent entity) =>
       ref Storage<MoveCommand>.components[entity.id];
   }

   [Il2CppSetOption(Option.NullChecks, false)]
   [Il2CppSetOption(Option.ArrayBoundsChecks, false)]
   [Il2CppSetOption(Option.DivideByZeroChecks, false)]
   sealed class StorageMoveCommand : Storage<MoveCommand>
   {
     public override MoveCommand Create() => new MoveCommand();
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

