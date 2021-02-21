using Godot;
using System;

public class StructureTiles : TileMap
{
    // Declare member variables here. Examples:
    // private int a = 2;
    // private string b = "text";
    Area2D mouseArea;

    // Called when the node enters the scene tree for the first time.
    public override void _Ready() {
        mouseArea = GetNode<Area2D>("MouseArea");
    }

    public override void _Input(InputEvent @event) {
        if (@event is InputEventMouseButton eventMouseButton)
        {
            HandleMouseInput(eventMouseButton);
        }
    }

    private void HandleMouseInput(InputEventMouseButton eventMouseButton) {
        if (eventMouseButton.Pressed == false) {
            var mouseTile = WorldToMap(GetGlobalMousePosition());
            if (eventMouseButton.ButtonIndex == (int)ButtonList.Left) {
                if (mouseArea.GetOverlappingAreas().Count + mouseArea.GetOverlappingBodies().Count == 0) {
                    SetCell((int)mouseTile.x, (int)mouseTile.y, 0);
                }
            } else if (eventMouseButton.ButtonIndex == (int)ButtonList.Right) {
                SetCell((int)mouseTile.x, (int)mouseTile.y, -1);
            }
        }
    }

    public override void _Process(float delta)
    {
        var mouseTilePosition = MapToWorld(WorldToMap(GetGlobalMousePosition()));
        mouseArea.Position = mouseTilePosition;
    }
}
