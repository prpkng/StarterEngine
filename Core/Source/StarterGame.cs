using Core.Source.Components;
using Core.Source.Hierarchy;
using Core.Source.Singletons;
using ImGuiNET;
using ImGuiNET.SampleProgram.XNA;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Serilog;

namespace Core.Source;

public class StarterGame : Game
{
    public static StarterGame? Instance { get; private set; }

    private ImGuiRenderer m_ImGuiRenderer;
    private Scene m_CurrentScene;
    
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
        
        // Initialize logger
        Log.Logger = new LoggerConfiguration()
            .MinimumLevel.Debug()
            .WriteTo.Console()
            .WriteTo.File("logs/latest.log", rollingInterval: RollingInterval.Day)
            .CreateLogger();
        
        SingletonInitializer.InitializeSingletons();
        
        Log.Information("Game initialization complete!");


        m_CurrentScene = new Scene("Test Scene", [
            new GameObject("Root", new Transform2D(new Vector2(128, 64)), [
                new TestObject("Test Object")
            ])
        ]);
        m_CurrentScene.Load();

        IsMouseVisible = true;
        m_ImGuiRenderer = new ImGuiRenderer(this);
        m_ImGuiRenderer.RebuildFontAtlas();
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
        
        m_CurrentScene.Update();
    }

    protected override void Draw(GameTime gameTime)
    {
        // Render stuff in here. Do NOT run game logic in here!
        GraphicsDevice.Clear(Color.CornflowerBlue);
        
        m_CurrentScene.Draw();
        
        m_ImGuiRenderer.BeforeLayout(gameTime);
        ImGui.ShowDemoWindow();
        m_ImGuiRenderer.AfterLayout();
        
        base.Draw(gameTime);
    }
}