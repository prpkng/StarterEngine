using Core.Source.Components;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Serilog;

namespace Core.Source.Hierarchy;

public class GameObject : IEntity
{
    public List<GameObject> Children = [];
    public GameObject? Parent;

    public readonly Transform2D Transform;

    public string Name;

    public GameObject(string name = "")
    {
        Name = name;
        Transform = new Transform2D();
    }

    public GameObject(string name, GameObject parent)
    {
        Name = name;
        Parent = parent;
        Transform = new Transform2D(parent.Transform);
    }

    public virtual void Load()
    {
    }

    public virtual void Unload()
    {
    }

    public virtual void Start()
    {
    }

    public virtual void Update()
    {
    }

    public virtual void Draw(SpriteBatch spriteBatch)
    {
    }

    public void SetParent(GameObject parent)
    {
        Parent = parent;
        Transform.SetParent(parent.Transform);
    }

    public void ClearParent()
    {
        Parent = null;
        Transform.ClearParent();
    }

    public void AddChild(GameObject child)
    {
        Children.Add(child);
        child.SetParent(this);
    }

    public void RemoveChild(GameObject child)
    {
        if (Children.Contains(child))
        {
            Children.Remove(child);
            return;
        }

        Log.Warning("Calling RemoveChild with a invalid GameObject instance ({}) on '{Name}'", child.Name, Name);
    }
}