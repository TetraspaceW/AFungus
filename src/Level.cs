using Godot;
using System;

public class Level : Node2D
{
    // Declare member variables here. Examples:
    // private int a = 2;
    // private string b = "text";

    // Called when the node enters the scene tree for the first time.
    [Export] PackedScene LittleGuyScene;
    AudioStreamPlayer audioStreamPlayer;
    float time = 0;
    [Export] int numberOfLittleGuys;
    [Export] float intervalBetweenLittleGuys;
    int littleGuysOnMap = 0;
    Mushrum mushrum;
    public override void _Ready() {
        audioStreamPlayer = GetNode<AudioStreamPlayer>("AudioStreamPlayer");
        // audioStreamPlayer.Playing = true;

        mushrum = GetNode<Mushrum>("Mushrum");
        mushrum.Position = new Vector2();
    }

//  // Called every frame. 'delta' is the elapsed time since the previous frame.
    public override void _Process(float delta) {
        time += delta;
        if (time > intervalBetweenLittleGuys && littleGuysOnMap < numberOfLittleGuys) {
            time -= intervalBetweenLittleGuys;
            var newLittleGuy = (LittleGuy)LittleGuyScene.Instance();
            newLittleGuy.Position = new Vector2(0,0);
            AddChild(newLittleGuy);
            littleGuysOnMap++;
        }

        if (Input.IsActionJustReleased("mute")) {
            audioStreamPlayer.Playing = !audioStreamPlayer.Playing;
        }
    }
}
