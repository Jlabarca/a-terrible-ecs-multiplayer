using LeoECS.Command;
using LeoECS.Command.Components;
using LeoECS.Command.Systems;
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
        [SerializeField]
        private EcsSystems fixedSystems;
        [SerializeField]
        private GameState gameState;
        public Configuration configuration;
        private UnitsPool unitsPool;

        private void OnEnable()
        {

            ecsWorld = new EcsWorld();
            systems = new EcsSystems(ecsWorld);
            fixedSystems = new EcsSystems(ecsWorld);
            gameState = new GameState();
            unitsPool = gameObject.GetComponent<UnitsPool>();
            unitsPool.Prewarm(1000, configuration.unitView);

#if UNITY_EDITOR
            Leopotam.Ecs.UnityIntegration.EcsWorldObserver.Create(ecsWorld);
            Leopotam.Ecs.UnityIntegration.EcsSystemsObserver.Create(systems);
#endif

            systems
                .Add(new UnitsInitSystem())
                .Add(new ClickMoveSystem())
                .Add(new UnitSelectionSystem())
                .Add(new DebugInputsSystem())
                .Add(new DynamicNavSystem())
                .Inject(gameState)
                .Inject(configuration)
                .Inject(unitsPool)
                .Init();

            fixedSystems
                .Add(new SpawnCommandSystem())
                .Add(new MoveCommandSystem())
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
            fixedSystems.Run();
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
