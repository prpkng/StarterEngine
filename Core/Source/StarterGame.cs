using Microsoft.Xna.Framework;
using Serilog;

namespace Core.Source;

public class StarterGame : Game
{
    public StarterGame(ConfigFile config)
    {
        _ = new GraphicsDeviceManager(this)
        {
            PreferredBackBufferWidth = config.WindowWidth,
            PreferredBackBufferHeight = config.WindowHeight,
            IsFullScreen = false,
            SynchronizeWithVerticalRetrace = true //V-Sync
        };
    }

    protected override void Initialize()
    {
        base.Initialize();
        
        // Initialize logger
        Log.Logger = new LoggerConfiguration()
            .MinimumLevel.Debug()
            .WriteTo.Console()
            .WriteTo.File("logs/latest.log", rollingInterval: RollingInterval.Day)
            .CreateLogger();
        
        SingletonInitializer.InitializeSingletons();
        
        Log.Information("Game initialization complete!");
    }

    protected override void LoadContent()
    {
        // Load textures, sounds, and so on in here...
        base.LoadContent();
    }

    protected override void UnloadContent()
    {
        // Clean up after yourself!
        base.UnloadContent();
    }

    protected override void Update(GameTime gameTime)
    {
        // Run game logic in here. Do NOT render anything here!
        base.Update(gameTime);
    }

    protected override void Draw(GameTime gameTime)
    {
        // Render stuff in here. Do NOT run game logic in here!
        GraphicsDevice.Clear(Color.CornflowerBlue);
        
        base.Draw(gameTime);
    }
}