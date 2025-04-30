using Microsoft.Xna.Framework.Graphics;
using Serilog;

namespace Core.Source.Hierarchy;

public class Scene(string name)
{
    public readonly string Name = name;
    public readonly List<GameObject> GameObjects = [];
    
    private readonly SpriteBatch m_SpriteBatch = new(StarterGame.Instance?.GraphicsDevice);

    public Scene(string name, List<GameObject> gameObjects) : this(name)
    {
        GameObjects = gameObjects;
    }

    public void Add(GameObject gameObject)
    {
        GameObjects.Add(gameObject);
    }

    public void Remove(GameObject gameObject)
    {
        if (GameObjects.Contains(gameObject))
        {
            GameObjects.Remove(gameObject);
            return;
        }
        Log.Warning("GameObject '{}' not found in scene '{}'", gameObject.Name, Name);
    }

    public void Load()
    {
        GameObjects.ForEach(rootObject => rootObject.RunRecursively(child => child.Load()));
        GameObjects.ForEach(rootObject => rootObject.RunRecursively(child => child.Start()));
    }

    public void Update()
    {
        GameObjects.ForEach(rootObject => rootObject.RunRecursively(child => child.Update()));
    }

    public void Draw()
    {
        GameObjects.ForEach(rootObject => rootObject.RunRecursively(child => child.Draw(m_SpriteBatch)));
    }
}