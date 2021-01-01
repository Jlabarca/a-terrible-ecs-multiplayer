using System.Collections.Generic;
using System.Linq;
using Actors.Command.Components;
using Actors.Components;
using Pixeye.Actors;
using UnityEngine;
using UnityEngine.AI;

namespace Actors.Command.Processors
{
    internal sealed class MoveProcessor : Processor, ITickFixed
    {
        private readonly Group<MoveCommand> moveCommands = default;
        private readonly Group<UnitComponent, NavigationComponent> units = default;
        private readonly GameState gameState;

        public MoveProcessor()
        {
            gameState = Layer.Get<GameState>();
        }
        public void TickFixed(float dt)
        {
            foreach (var entity in moveCommands)
            {
                var moveCommand = entity.Get<MoveCommand>();
                var navMeshAgents = GetNavMeshAgents(units, moveCommand.units);
                var tempPositions = GetFormationPositions(navMeshAgents, moveCommand.position);

                for (int i = 0; i < navMeshAgents.Length; i++)
                {
                    var navMeshAgent = navMeshAgents[i];
                    navMeshAgent.isStopped = false;
                    navMeshAgent.radius = 0.1f;
                    navMeshAgent.stoppingDistance = 0.1f;
                    navMeshAgent.angularSpeed = 0;
                    navMeshAgent.SetDestination(tempPositions[i]);
                }

                entity.Remove<MoveCommand>();
            }
        }

        private static NavMeshAgent[] GetNavMeshAgents(GroupCore units, uint[] moveCommandUnits)
        {
            var result = new List<NavMeshAgent>();
            foreach (var entity in units)
            {
                if(moveCommandUnits.Contains(entity.Get<UnitComponent>().unitId))
                    result.Add(entity.transform.GetComponent<NavMeshAgent>());
            }

            return result.ToArray();
        }

        private static Vector3[] GetFormationPositions(IReadOnlyList<NavMeshAgent> selectedActors, Vector3 targetPosition)
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
