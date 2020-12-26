using System.Collections.Generic;
using LeoECS.Command.Components;
using Leopotam.Ecs;
using UnityEngine;
using UnityEngine.AI;

namespace LeoECS.Command.Systems
{
    internal sealed class MoveCommandSystem : IEcsRunSystem
    {
        private EcsFilter<MoveCommand> filter;

        public void Run()
        {
            foreach (var index in filter)
            {
                var moveCommand = filter.Get1(index);
                var tempPositions = GetFormationPositions(moveCommand.selectedActors, moveCommand.targetPosition);

                for (int i = 0; i < moveCommand.selectedActors.Count; i++)
                {
                    var navMeshAgent = moveCommand.selectedActors[i].GetComponent<NavMeshAgent>();
                    navMeshAgent.isStopped = false;
                    navMeshAgent.radius = 0.1f;
                    navMeshAgent.stoppingDistance = 0.1f;
                    navMeshAgent.angularSpeed = 0;
                    navMeshAgent.SetDestination(tempPositions[i]);
                }

                filter.GetEntity(index).Destroy();
            }
        }

        private static Vector3[] GetFormationPositions(List<GameObject> selectedActors, Vector3 targetPosition)
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
