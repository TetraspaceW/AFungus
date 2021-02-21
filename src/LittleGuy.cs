using Godot;
using System;

public class LittleGuy : KinematicBody2D
{
    // Declare member variables here. Examples:
    // private int a = 2;
    // private string b = "text";
    private Vector2 horizontalVelocity;
    [Export] private float speed = 30;
    private Vector2 verticalVelocity;
    private AnimatedSprite sprite;
    private GraphicsState graphicsState = GraphicsState.Idle;
    

    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
        horizontalVelocity = new Vector2(speed,0);
        sprite = GetNode<AnimatedSprite>("Sprite");
    }

//  // Called every frame. 'delta' is the elapsed time since the previous frame.
//  public override void _Process(float delta)
//  {
//      
//  }

    public override void _PhysicsProcess(float delta) {
        verticalVelocity.y += PhysicsConstants.gravity*delta;
        if (verticalVelocity.y > 1000) {
            verticalVelocity.y = 1000;
        }

        var velocity = horizontalVelocity + verticalVelocity;
        MoveAndSlide(velocity,PhysicsConstants.upDirection,false);

        if (IsOnFloor()) {
            verticalVelocity.y = 0;
        }

        if (IsOnWall()) {
            horizontalVelocity.x = -horizontalVelocity.x;
        }

        SetAnimation();
    }

    public void SetAnimation() {
        if (horizontalVelocity.x != 0) {
            sprite.FlipH = horizontalVelocity.x < 0;
        }

        if (!IsOnFloor()) {
            graphicsState = GraphicsState.Jumping;
        } else if (horizontalVelocity.x != 0) {
            graphicsState = GraphicsState.Walking;
        } else {
            graphicsState = GraphicsState.Idle;
        }

        switch (graphicsState) {
            case GraphicsState.Idle: sprite.Animation = "idle"; break;
            case GraphicsState.Walking: sprite.Animation = "walk"; break;
            case GraphicsState.Jumping: sprite.Animation = "jump"; break;
        }
    }
}
