using Core.Source.Components;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Serilog;

namespace Core.Source.Hierarchy;

public class GameObject(string name = "", GameObject? parent = null) : IEntity
{
    public List<GameObject> Children = [];
    public GameObject? Parent = parent;

    public readonly Transform2D Transform = new(parent?.Transform ?? null);

    public string Name = name;

    public GameObject(string name, List<GameObject> children) : this(name)
    {
        Children = children;
        Children.ForEach(child => child.SetParent(this));
    }
    public GameObject(string name, Transform2D transform, List<GameObject> children) : this(name)
    {
        Children = children;
        Transform = transform;
        Children.ForEach(child => child.SetParent(this));
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

    public void RunRecursively(Action<GameObject> action)
    {
        var queue = new Queue<GameObject>();
        queue.Enqueue(this);

        while (queue.Count != 0)
        {
            var obj = queue.Dequeue();
            action(obj);
            obj.Children.ForEach(queue.Enqueue);
        }
    }
}