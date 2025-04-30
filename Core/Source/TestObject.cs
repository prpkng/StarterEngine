using Core.Source.Hierarchy;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Serilog;

namespace Core.Source;

public class TestObject(string name = "") : GameObject(name)
{
    private Texture2D? m_Texture;

    public override void Start()
    {
        m_Texture = new Texture2D(StarterGame.Instance?.GraphicsDevice, 1, 1);
        m_Texture.SetData([Color.Red]);
        base.Start();
    }

    public override void Update()
    {
        var posX = Mouse.GetState().X;
        var posY = Mouse.GetState().Y;


        var deltaX = (posX - Transform.PositionX);
        
        Log.Information("{F}", deltaX);
        
        Transform.RotationDegrees = MathHelper.Lerp(
            Transform.RotationDegrees,
            MathHelper.Clamp(deltaX / 2, -75, 75),
            Time.Delta * 8
        );
        
        Transform.Position = new Vector2(posX, posY);

        base.Update();
    }

    public override void Draw(SpriteBatch spriteBatch)
    {
        spriteBatch.Begin();

        spriteBatch.Draw(m_Texture,
            new Rectangle((int)Transform.WorldPosition.X, (int)Transform.WorldPosition.Y, 64, 64),
            new Rectangle(0, 0, 1, 1), Color.White, Transform.Rotation, Vector2.One*0.5f, SpriteEffects.None, 0);

        spriteBatch.End();
    }
}