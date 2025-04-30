namespace Core.Source.Components;

using Microsoft.Xna.Framework;

public class Transform2D(Transform2D? parent = null)
{
    public Transform2D? Parent { get; private set; } = parent;

    public Vector2 Position
    {
        get => m_Position;
        set
        {
            m_Position = value;
            UpdateMatrices();
        }
    }

    public float PositionX
    {
        get => m_Position.X;
        set
        {
            m_Position.X = value;
            UpdateMatrices();
        }
    }

    public float PositionY
    {
        get => m_Position.Y;
        set
        {
            m_Position.Y = value;
            UpdateMatrices();
        }
    }

    public Vector2 Scale
    {
        get => m_Scale;
        set
        {
            m_Scale = value;
            UpdateMatrices();
        }
    }

    public Vector2 Offset
    {
        get => m_Offset;
        set
        {
            m_Offset = value;
            UpdateMatrices();
        }
    }

    public float Rotation
    {
        get => m_Rotation;
        set
        {
            m_Rotation = value;
            UpdateMatrices();
        }
    }

    public float RotationDegrees
    {
        get => MathHelper.ToDegrees(Rotation);
        set => Rotation = MathHelper.ToRadians(value);
    }

    public Vector2 WorldPosition => Vector2.Transform(Position, ParentTransformationMatrix);
    public Quaternion WorldRotation => Quaternion.CreateFromRotationMatrix(WorldTransformationMatrix);
    public Vector2 WorldScale => Vector2.Transform(Scale, ParentTransformationMatrix);

    public Matrix LocalTransformationMatrix { get; private set; }
    public Matrix WorldTransformationMatrix { get; private set; }
    public Matrix ParentTransformationMatrix => Parent?.WorldTransformationMatrix ?? Matrix.Identity;

    private Vector2 m_Position = Vector2.Zero;
    private Vector2 m_Scale = Vector2.One;
    private Vector2 m_Offset = Vector2.Zero;
    private float m_Rotation = 0;

    public Transform2D(Vector2? position = null, Vector2? scale = null, float rotation = 0) : this(null)
    {
        Position = position ?? Vector2.Zero;
        Scale = scale ?? Vector2.One;
        RotationDegrees = rotation;
    }
    
    void UpdateMatrices()
    {
        var locationMatrix = Matrix.CreateTranslation(new Vector3(Position, 0));
        var offsetMatrix = Matrix.CreateTranslation(new Vector3(Offset, 0));
        var rotationMatrix = Matrix.CreateRotationZ(Rotation);
        var scaleMatrix = Matrix.CreateScale(new Vector3(Scale, 0));

        LocalTransformationMatrix = scaleMatrix * offsetMatrix * rotationMatrix * locationMatrix;
        WorldTransformationMatrix = LocalTransformationMatrix * ParentTransformationMatrix;
    }

    public void SetParent(Transform2D? parent)
    {
        Parent = parent;
        UpdateMatrices();
    }

    public void ClearParent()
    {
        Parent = null;
        UpdateMatrices();
    }
}