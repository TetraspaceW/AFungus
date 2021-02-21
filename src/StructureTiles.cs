using Godot;
using System;

public class StructureTiles : TileMap
{
    // Declare member variables here. Examples:
    // private int a = 2;
    // private string b = "text";

    // Called when the node enters the scene tree for the first time.
    public override void _Ready() {
        
    }

    public override void _Input(InputEvent @event) {
        if (@event is InputEventMouseButton eventMouseButton) {
            if (eventMouseButton.Pressed == false) {
                var mouseTile = WorldToMap(GetGlobalMousePosition());
                if (eventMouseButton.ButtonIndex == (int)ButtonList.Left) {  
                    SetCell((int)mouseTile.x, (int)mouseTile.y, 0);
                } else if (eventMouseButton.ButtonIndex == (int)ButtonList.Right) {
                    SetCell((int)mouseTile.x, (int)mouseTile.y, -1);
                }
            }
        }
    }

    //  // Called every frame. 'delta' is the elapsed time since the previous frame.
    //  public override void _Process(float delta)
    //  {
    //      
    

//  // Called every frame. 'delta' is the elapsed time since the previous frame.
//  public override void _Process(float delta)
//  {
//      
//  }
}
