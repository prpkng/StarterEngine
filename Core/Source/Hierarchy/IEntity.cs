using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Core.Source.Hierarchy;

public interface IEntity
{
    /// <summary>
    /// Called at the moment which the entity is initialized, its children are not ready yet
    /// </summary>
    public void Load() {}
    /// <summary>
    /// Called after all entities have been loaded
    /// </summary>
    public void Start();
    /// <summary>
    /// Called every tick, use for logic and physics updates
    /// </summary>
    public void Update();
    /// <summary>
    /// Called every frame, draws the entity (if any rendering is required)
    /// </summary>
    public void Draw(SpriteBatch spriteBatch);
    /// <summary>
    /// Called when the entity is being unloaded (freed)
    /// </summary>
    public void Unload() {}
}