using System.Runtime.CompilerServices;
using Lockstep.Core.Logic.Serialization;
using Lockstep.Core.Logic.Serialization.Utils;
using Pixeye.Actors;
using Server.Common;
using Unity.IL2CPP.CompilerServices;
using UnityEngine;

namespace Actors.Command.Components
 {
   public class MoveCommand : ICommand
   {
     public Vector3 position;
     public uint[] units;

     #region Command

     public CommandTag Tag => CommandTag.MoveCommand;

     public void Serialize(Serializer writer)
     {
       writer.Put((int)position.x);
       writer.Put((int)position.y);
       writer.Put((int)position.z);
       writer.PutArray(units);
     }

     void ISerializable.Deserialize(Deserializer reader)
     {
       throw new System.NotImplementedException();
     }

     public static MoveCommand Deserialize(Deserializer reader)
     {
       var spawnCommand = new MoveCommand
       {
         position =  new Vector3(reader.GetInt(), reader.GetInt(), reader.GetInt()),
         units = reader.GetUIntArray()
       };
       return spawnCommand;
     }

     #endregion
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

