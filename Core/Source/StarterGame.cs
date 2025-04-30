using Core.Source.Singletons;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Serilog;

namespace Core.Source;

public class StarterGame : Game
{
    public static StarterGame? Instance { get; private set; }
    
    
    private TestObject m_TestObject;
    private SpriteBatch m_SpriteBatch;
    
    public StarterGame(ConfigFile config)
    {
        _ = new GraphicsDeviceManager(this)
        {
            PreferredBackBufferWidth = config.WindowWidth,
            PreferredBackBufferHeight = config.WindowHeight,
            IsFullScreen = false,
            SynchronizeWithVerticalRetrace = true //V-Sync
        };
        Instance = this;
    }


    protected override void Initialize()
    {
        base.Initialize();
        
        m_SpriteBatch = new SpriteBatch(GraphicsDevice);
        
        // Initialize logger
        Log.Logger = new LoggerConfiguration()
            .MinimumLevel.Debug()
            .WriteTo.Console()
            .WriteTo.File("logs/latest.log", rollingInterval: RollingInterval.Day)
            .CreateLogger();
        
        SingletonInitializer.InitializeSingletons();
        
        Log.Information("Game initialization complete!");

        
        m_TestObject = new TestObject("Test Object");
        m_TestObject.Start();
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

        Time.Delta = (float)gameTime.ElapsedGameTime.TotalSeconds;
        Time.DeltaDouble = gameTime.ElapsedGameTime.TotalSeconds;
        
        m_TestObject.Update();
    }

    protected override void Draw(GameTime gameTime)
    {
        // Render stuff in here. Do NOT run game logic in here!
        GraphicsDevice.Clear(Color.CornflowerBlue);
        
        m_TestObject.Draw(m_SpriteBatch);
        base.Draw(gameTime);
    }
}