using Godot;
using System;
using System.Collections.Generic;

public class Mushrum : KinematicBody2D
{
    // Declare member variables here. Examples:
    // private int a = 2;
    // private string b = "text";
    private Vector2 horizontalVelocity;
    [Export] public int speed = 240;
    private Vector2 verticalVelocity;
    private AnimatedSprite sprite;
    private GraphicsState graphicsState = GraphicsState.Idle;

    public Dictionary<StructureMaterial,int> materialSupply = new Dictionary<StructureMaterial, int>() {
        {StructureMaterial.Wood, 0},
        {StructureMaterial.Stone, 0}
    };

    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
        base._Ready();
        sprite = GetNode<AnimatedSprite>("Sprite");
    }

    //  // Called every frame. 'delta' is the elapsed time since the previous frame.
    //  public override void _Process(float delta)
    //  {
    //      
    //  }

    public void GetInput() {
        horizontalVelocity = new Vector2();
        if (Input.IsActionPressed("ui_left")) {
            horizontalVelocity.x -= 1;
        }
        if (Input.IsActionPressed("ui_right")) {
            horizontalVelocity.x += 1;
        }

        horizontalVelocity = horizontalVelocity.Normalized() * speed;
    }

    public override void _PhysicsProcess(float delta)
    {
        GetInput();
        verticalVelocity.y += PhysicsConstants.gravity*delta;
        if (verticalVelocity.y > 1000) {
            verticalVelocity.y = 1000;
        }

        var velocity = horizontalVelocity + verticalVelocity;
        MoveAndSlide(velocity,PhysicsConstants.upDirection,true);

        if (IsOnCeiling()) {
            verticalVelocity.y = 0;
        }

        if (IsOnFloor()) {
            if (Input.IsActionPressed("ui_up")) {
                verticalVelocity.y = -Mathf.Sqrt(PhysicsConstants.gravity)*Mathf.Sqrt(100);
            } else {
                verticalVelocity.y = 0;
            }
        }

        if (velocity.Length() > 900) {
            horizontalVelocity = new Vector2();
            verticalVelocity = new Vector2();
            Position = new Vector2();
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

    public void CollectMaterial(StructureMaterial material) {
        materialSupply[material]++;
    }
}
