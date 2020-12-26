using System.Collections.Generic;
using UnityEngine;

namespace LeoECS.Command.Components
 {
   struct MoveCommand
   {
     public List<GameObject> selectedActors;
     public Vector3 targetPosition;
   }
 }

