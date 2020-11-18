using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace LeoECS.Command
{
    [Serializable]
    public class MoveCommand : Commands.Command
    {
        public readonly List<NavMeshAgent> selectedActors;
        public readonly Vector3 targetPosition;
        private Vector3[] originalPositions;
        private Vector3[] tempPositions;

        public MoveCommand(IEnumerable<NavMeshAgent> selectedActors, Vector3 targetPosition)
        {
            this.selectedActors = new List<NavMeshAgent>(selectedActors);
            this.targetPosition = targetPosition;
            GetFormationPositions();
        }

        internal override void Execute()
        {
            for (int i = 0; i < selectedActors.Count; i++)
            {
                var navMeshAgent = selectedActors[i];
                navMeshAgent.isStopped = false;
                navMeshAgent.radius = 0.1f;
                navMeshAgent.stoppingDistance = 0.1f;
                navMeshAgent.angularSpeed = 0;
                navMeshAgent.SetDestination(tempPositions[i]);
            }
        }

        internal override void Undo()
        {
            for (int i = 0; i < selectedActors.Count; i++)
            {
                var navMeshAgent = selectedActors[i];
                navMeshAgent.transform.position = originalPositions[i];
                navMeshAgent.isStopped = true;
                //navMeshAgent.SetDestination(tempPositions[i]);
            }
        }

        public void GetFormationPositions()
        {
            //TODO: accomodate bigger numbers
            originalPositions = new Vector3[selectedActors.Count];
            tempPositions = new Vector3[selectedActors.Count];

            const float formationOffset = 1f;

            float increment = 360f / selectedActors.Count;
            for(int k = 0; k < selectedActors.Count; k++)
            {
                originalPositions[k] = selectedActors[k].transform.position;
                float angle = increment * k;
                var offset = new Vector3(formationOffset * Mathf.Cos(angle * Mathf.Deg2Rad), 0f, formationOffset * Mathf.Sin(angle * Mathf.Deg2Rad));
                tempPositions[k] = targetPosition + offset;
            }
        }
    }
}
