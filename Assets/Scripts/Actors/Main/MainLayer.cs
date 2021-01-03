using System;
using Actors.Command.Processors;
using Actors.Online;
using Actors.PlayerInput;
using NLog;
using Online;
using Pixeye.Actors;
using UnityEngine;
using UnityEngine.UI;

namespace Actors.Main
{
  public class MainLayer : Layer<MainLayer>, ITickFixed
  {

    public GameState gameState;

    public LiteNetLibNetwork onlineManager;

    public Text tickText;

    protected override void Setup()
    {
      SetupLogger();
      gameState = Add<GameState>();

      onlineManager = Add<LiteNetLibNetwork>();
      onlineManager.Start(gameState);
      Add<OnlineProcessor>();

      Add<ClickMoveProcessor>();
      Add<UnitSelectionProcessor>();
      Add<SpawnProcessor>();
      Add<MoveProcessor>();
      Add<SelectionProcessor>();
      Add<DebugInputProcessor>();
      //Add<DynamicNavProcessor>();

    }

    private void SetupLogger()
    {
      var defaultFormatter = new DefaultLogMessageFormatter();
      var timestampFormatter = new TimestampFormatter();
      LoggerFactory.AddAppender((logger, logLevel, message) => {
        message = defaultFormatter.FormatMessage(logger, logLevel, message);
        message = timestampFormatter.FormatMessage(logger, logLevel, message);
        switch (logLevel)
        {
          case LogLevel.On:
          case LogLevel.Trace:
          case LogLevel.Debug:
          case LogLevel.Info:
            Debug.Log(message);
            break;
          case LogLevel.Warn:
            Debug.LogWarning(message);
            break;
          case LogLevel.Error:
          case LogLevel.Fatal:
            Debug.LogError(message);
            break;
          case LogLevel.Off:
            break;
          default:
            throw new ArgumentOutOfRangeException(nameof(logLevel), logLevel, null);
        }
      });
    }

    // Use to clean up custom stuff before the layer gets destroyed.
    protected override void OnLayerDestroy()
    {
      onlineManager.Stop();
    }

    public void TickFixed(float dt)
    {
        tickText.text = gameState.tick.ToString();
    }
  }
}
