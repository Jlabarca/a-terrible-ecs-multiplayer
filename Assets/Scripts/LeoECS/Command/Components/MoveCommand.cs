using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace LeoECS.Command.Components
 {
   struct MoveCommand
   {
     public List<NavMeshAgent> selectedActors;
     public Vector3 targetPosition;
   }
 }

