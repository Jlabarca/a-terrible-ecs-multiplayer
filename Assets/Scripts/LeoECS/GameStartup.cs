using LeoECS.Command;
using LeoECS.Nav;
using LeoECS.PlayerInput;
using LeoECS.Pooling;
using LeoECS.ScriptableObjects;
using LeoECS.Unit;
using Leopotam.Ecs;
using UnityEngine;

namespace LeoECS
{
    public class GameStartup : MonoBehaviour
    {
        [SerializeField]
        private EcsWorld ecsWorld;
        [SerializeField]
        private EcsSystems systems;
        private System.Random random;
        [SerializeField]
        private GameState gameState;
        public Configuration configuration;
        private UnitsPool unitsPool;

        private void OnEnable()
        {

            ecsWorld = new EcsWorld();
            systems = new EcsSystems(ecsWorld);
            random = new System.Random(1);
            gameState = new GameState();
            unitsPool = gameObject.GetComponent<UnitsPool>();
            unitsPool.Prewarm(1000, configuration.unitView);

#if UNITY_EDITOR
            Leopotam.Ecs.UnityIntegration.EcsWorldObserver.Create(ecsWorld);
            Leopotam.Ecs.UnityIntegration.EcsSystemsObserver.Create(systems);
#endif

            systems
                .Add(new UnitsInitSystem())
                .Add(new UnitSpawnSystem())
                .Add(new CommandSystem())
                .Add(new ClickMoveSystem())
                .Add(new UnitSelectionSystem())
                .Add(new DebugInputsSystem())
                .Add(new DynamicNavSystem())
                .OneFrame<UnitSpawnCommand>()
                .Inject(gameState)
                .Inject(configuration)
                .Inject(unitsPool)
                .Init();
        }

        private void Update() {
            systems.Run();
        }

        private void FixedUpdate()
        {
            gameState.time += (long)(Time.fixedDeltaTime * 1000);
        }

        private void OnDisable() {
            systems.Destroy();
            systems = null;

            ecsWorld.Destroy();
            ecsWorld = null;
        }
    }
}
