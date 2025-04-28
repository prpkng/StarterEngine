using Microsoft.Xna.Framework;

namespace Game.Source;

public class StarterGame : Microsoft.Xna.Framework.Game
{
    public StarterGame()
    {
        _ = new GraphicsDeviceManager(this)
        {
            PreferredBackBufferWidth = 640,
            PreferredBackBufferHeight = 360,
            IsFullScreen = false,
            SynchronizeWithVerticalRetrace = true //V-Sync
        };
    }

    protected override void Initialize()
    {
        base.Initialize();
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