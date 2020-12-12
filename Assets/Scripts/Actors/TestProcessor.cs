using System.Collections;
using Pixeye.Actors;
using UnityEngine;

namespace Actors
{
    public class TestProcessor : Processor, ITick
    {
        private RoutineCall routine;
        private GameState gameState;
        public TestProcessor()
        {
            routine = Layer.Run(CoHangWithAlpaca()); // run routine and get routine call.
            Debug.Log("get");
            gameState = Layer.Get<GameState>();
        }

        IEnumerator CoHangWithAlpaca()
        {
            yield return Layer.Wait(1.0f);
            Debug.Log("Stop hanging around "+gameState.selectedActors.Count);
        }

        public void Tick(float dt)
        {
            if (Input.GetKeyDown(KeyCode.S))
            {
                Debug.Log("STOP");
                routine.Stop(); // stop the routine
            }
        }
    }
}
