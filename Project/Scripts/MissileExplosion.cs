using Godot;
using System;

public class MissileExplosion : Sprite
{
    string source;

    [Signal]
    public delegate void EnemyHit();

    [Signal]
    public delegate void CitieHit();

    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
        GetNode<AnimationPlayer>("AnimationPlayer").Play("Explode");
    }

    void die()
    {
    	QueueFree();
    }

    public void setSource(string sourceString)
    {
        source = sourceString;
    }

    void _on_Area2D_body_entered(PhysicsBody2D body)
    {
        if(source == "player")
        {
            if(body.IsInGroup("Enemy"))
            {
                Connect("EnemyHit", body, "die");
                EmitSignal("EnemyHit");
            }
        }
        if(source == "enemy")
        {
            if(body.IsInGroup("Citie"))
            {
                Connect("CitieHit", body, "pop");
                EmitSignal("CitieHit");
            }
            if(body.IsInGroup("Gun"))
                GD.Print("Add gun");    // TODO
        }
    }

//  // Called every frame. 'delta' is the elapsed time since the previous frame.
//  public override void _Process(float delta)
//  {
//      
//  }
}