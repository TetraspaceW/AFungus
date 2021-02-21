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
    public override void _Ready()
    {
        sprite = GetNode<AnimatedSprite>("Sprite");
    }

    public void SetState(State newState) {
        state = newState;
        switch (state)
        {
            case State.Valid: sprite.Animation = "valid"; break;
            case State.Invalid: sprite.Animation = "invalid"; break;
        }
    }
}
