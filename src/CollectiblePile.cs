using Godot;
using System;

public class CollectiblePile : Area2D
{
    // Declare member variables here. Examples:
    // private int a = 2;
    // private string b = "text";
    [Export] StructureMaterial material = StructureMaterial.Wood;
    AnimatedSprite sprite;

    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
        sprite = GetNode<AnimatedSprite>("Sprite");
        switch (material)
        {
            case StructureMaterial.Wood:
                sprite.Animation = "Wood"; break;
            case StructureMaterial.Stone:
                sprite.Animation = "Stone"; break;
            default: break;
        }
    }

    //  // Called every frame. 'delta' is the elapsed time since the previous frame.
    //  public override void _Process(float delta)
    //  {
    //      
    //  }

    public override void _PhysicsProcess(float delta)
    {
        foreach (Mushrum mushrum in GetOverlappingBodies())
        {
            mushrum.CollectMaterial(material);
            QueueFree();
        }
        
    }
}
