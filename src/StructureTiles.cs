using Godot;
using System;

public class StructureTiles : TileMap
{
    // Declare member variables here. Examples:
    // private int a = 2;
    // private string b = "text";
    MouseArea mouseArea;
    Mushrum tilePlacer;
    Vector2 mouseTile;

    // Called when the node enters the scene tree for the first time.
    public override void _Ready() {
        mouseArea = GetNode<MouseArea>("MouseArea");
        tilePlacer = GetParent<Level>().GetNode<Mushrum>("Mushrum");
    }

    public override void _Input(InputEvent @event) {
        if (@event is InputEventMouseButton eventMouseButton)
        {
            HandleMouseInput(eventMouseButton);
        }
    }

    private void HandleMouseInput(InputEventMouseButton eventMouseButton) {
        if (eventMouseButton.Pressed == false) {
            if (eventMouseButton.ButtonIndex == (int)ButtonList.Left) {
                if (mouseArea.state == MouseArea.State.Valid) {
                    SetCell((int)mouseTile.x, (int)mouseTile.y, 0);
                    tilePlacer.woodSupply--;
                }
            } else if (eventMouseButton.ButtonIndex == (int)ButtonList.Right) {
                SetCell((int)mouseTile.x, (int)mouseTile.y, -1);
                tilePlacer.woodSupply++;
            }
        }
    }

    public override void _Process(float delta)
    {
        
    }

    public override void _PhysicsProcess(float delta)
    {
        mouseTile = WorldToMap(GetGlobalMousePosition());
        var mouseTilePosition = MapToWorld(mouseTile);
        mouseArea.Position = mouseTilePosition;

        if (mouseArea.GetOverlappingAreas().Count + mouseArea.GetOverlappingBodies().Count == 0 
        && GetCell((int)mouseTile.x, (int)mouseTile.y) == -1
        && tilePlacer.woodSupply > 0) {
            mouseArea.SetState(MouseArea.State.Valid);
        } else {
            mouseArea.SetState(MouseArea.State.Invalid);
        }
    }
}
