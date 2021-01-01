using System.Runtime.CompilerServices;
using Lockstep.Core.Logic.Serialization;
using Lockstep.Core.Logic.Serialization.Utils;
using Pixeye.Actors;
using Server.Common;
using Unity.IL2CPP.CompilerServices;
using UnityEngine;

namespace Actors.Command.Components
 {
   public class SpawnCommand : ICommand
   {
     public byte actorId;
     public Vector3 position;

     #region Command
     public CommandTag Tag => CommandTag.SpawnCommand;

     public void Serialize(Serializer writer)
     {
       writer.Put(actorId);
       writer.Put((int)position.x);
       writer.Put((int)position.y);
       writer.Put((int)position.z);
     }

     void ISerializable.Deserialize(Deserializer reader)
     {
       throw new System.NotImplementedException();
     }

     public static SpawnCommand Deserialize (Deserializer reader)
     {
       var spawnCommand = new SpawnCommand
       {
         actorId = reader.GetByte(),
         position =  new Vector3(reader.GetInt(), reader.GetInt(), reader.GetInt())
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

