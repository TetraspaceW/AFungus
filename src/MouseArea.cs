using Godot;
using System;

public class MouseArea : Area2D
{
    // Declare member variables here. Examples:
    // private int a = 2;
    // private string b = "text";

    // Called when the node enters the scene tree for the first time.
    private AnimatedSprite sprite;
    public enum State {
        Valid,
        Invalid
    }
    public State state;
    public StructureMaterial material;
    public override void _Ready()
    {
        sprite = GetNode<AnimatedSprite>("Sprite");
    }

    public void SetState(State newState, StructureMaterial newMaterial) {
        state = newState;
        material = newMaterial;
        switch (state)
        {
            case State.Valid: sprite.Animation = ValidAnimation(material); break;
            case State.Invalid: sprite.Animation = InvalidAnimation(material); break;
        }
    }

    public string ValidAnimation(StructureMaterial material) {
        switch (material) {
            case StructureMaterial.Wood: return "validWood";
            case StructureMaterial.Stone: return "validStone";
            default: break;
        }
        return "valid";
    }

    public string InvalidAnimation(StructureMaterial material) {
        switch (material) {
            case StructureMaterial.Wood: return "invalidWood";
            case StructureMaterial.Stone: return "invalidStone";
            default: break;
        }
        return "invalid";
    }

    
}
