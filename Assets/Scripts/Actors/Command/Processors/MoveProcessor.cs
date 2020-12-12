using System.Collections.Generic;
using Actors.Command.Components;
using Pixeye.Actors;
using UnityEngine;
using UnityEngine.AI;

namespace Actors.Command.Processors
{
    internal sealed class MoveProcessor : Processor, ITickFixed
    {

        private readonly Group<MoveCommand> moveCommands = default;

        public void TickFixed(float dt)
        {
            foreach (var entity in moveCommands)
            {
                var moveCommand = entity.Get<MoveCommand>();
                var tempPositions = GetFormationPositions(moveCommand.selectedActors, moveCommand.targetPosition);

                for (int i = 0; i < moveCommand.selectedActors.Count; i++)
                {
                    var navMeshAgent = moveCommand.selectedActors[i];
                    navMeshAgent.isStopped = false;
                    navMeshAgent.radius = 0.1f;
                    navMeshAgent.stoppingDistance = 0.1f;
                    navMeshAgent.angularSpeed = 0;
                    navMeshAgent.SetDestination(tempPositions[i]);
                }

                entity.Remove<MoveCommand>();
            }
        }

        private static Vector3[] GetFormationPositions(List<NavMeshAgent> selectedActors, Vector3 targetPosition)
        {
            //TODO: accomodate bigger numbers
            var originalPositions = new Vector3[selectedActors.Count];
            var tempPositions = new Vector3[selectedActors.Count];

            const float formationOffset = 1f;

            float increment = 360f / selectedActors.Count;
            for(int k = 0; k < selectedActors.Count; k++)
            {
                originalPositions[k] = selectedActors[k].transform.position;
                float angle = increment * k;
                var offset = new Vector3(formationOffset * Mathf.Cos(angle * Mathf.Deg2Rad), 0f, formationOffset * Mathf.Sin(angle * Mathf.Deg2Rad));
                tempPositions[k] = targetPosition + offset;
            }

            return tempPositions;
        }
    }
}
