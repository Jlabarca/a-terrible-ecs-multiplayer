using Actors.Command.Processors;
using Actors.PlayerInput;
using Pixeye.Actors;

namespace Actors.Main
{
  public class MainLayer : Layer<MainLayer>
  {

    public GameState gameState;

    // Use to add processors and set up a layer.
    protected override void Setup()
    {
      gameState = Add<GameState>();


      Add<ClickMoveProcessor>();
      Add<UnitSelectionProcessor>();
      Add<SpawnProcessor>();
      Add<MoveProcessor>();
      Add<DebugInputProcessor>();

    }

    // Use to clean up custom stuff before the layer gets destroyed.
    protected override void OnLayerDestroy()
    {
    }
  }
}
