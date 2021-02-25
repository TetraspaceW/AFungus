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
    StructureMaterial materialPlaced = StructureMaterial.Wood;

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
                    SetCell((int)mouseTile.x, (int)mouseTile.y, tileOf(materialPlaced));
                    tilePlacer.materialSupply[materialPlaced]--;
                }
            } else if (eventMouseButton.ButtonIndex == (int)ButtonList.Right) {
                var currentCell = GetCell((int)mouseTile.x, (int)mouseTile.y);
                if (currentCell != -1) {
                    SetCell((int)mouseTile.x, (int)mouseTile.y, -1);
                    tilePlacer.materialSupply[materialOf(currentCell)]++;
                }
            } else if (eventMouseButton.ButtonIndex == (int)ButtonList.WheelUp) {
                SwitchMaterial();
            } else if (eventMouseButton.ButtonIndex == (int)ButtonList.WheelDown) {
                SwitchMaterial();
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
        && tilePlacer.materialSupply[materialPlaced] > 0) {
            mouseArea.SetState(MouseArea.State.Valid, materialPlaced);
        } else {
            mouseArea.SetState(MouseArea.State.Invalid, materialPlaced);
        }
    }

    public int tileOf(StructureMaterial material) {
        switch (material) {
            case StructureMaterial.Wood: return 0;
            case StructureMaterial.Stone: return 1;
            default: break;
        }
        return 0;
    }

    public StructureMaterial materialOf(int tileID) {
        switch (tileID) {
            case 0: return StructureMaterial.Wood;
            case 1: return StructureMaterial.Stone;
            default: break;
        }
        return StructureMaterial.Wood;
    }

    public void SwitchMaterial() {
        switch (materialPlaced) {
            case StructureMaterial.Wood: materialPlaced = StructureMaterial.Stone; break;
            case StructureMaterial.Stone: materialPlaced = StructureMaterial.Wood; break;
            default: break;
        }
    }
}
