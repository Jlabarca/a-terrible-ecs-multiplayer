using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Pixeye.Actors;
using Unity.IL2CPP.CompilerServices;
using UnityEngine;

namespace Actors.Command.Components
 {
   public class SelectionCommand
   {
     public List<GameObject> selectedActors;
   }

   #region HELPERS

   [Il2CppSetOption(Option.NullChecks, false)]
   [Il2CppSetOption(Option.ArrayBoundsChecks, false)]
   [Il2CppSetOption(Option.DivideByZeroChecks, false)]
   static partial class Component
   {
     public const string SelectionCommandNs = "Actors.Command.Components.SelectionCommand";
     [MethodImpl(MethodImplOptions.AggressiveInlining)]
     public static ref SelectionCommand SelectionCommand(in this ent entity) =>
       ref Storage<SelectionCommand>.components[entity.id];
   }

   [Il2CppSetOption(Option.NullChecks, false)]
   [Il2CppSetOption(Option.ArrayBoundsChecks, false)]
   [Il2CppSetOption(Option.DivideByZeroChecks, false)]
   sealed class StorageSelectionCommand : Storage<SelectionCommand>
   {
     public override SelectionCommand Create() => new SelectionCommand();
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

