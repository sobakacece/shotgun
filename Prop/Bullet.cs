using Godot;
using System;
using System.Linq;

public partial class Bullet : Area2D
{
    public VisibleOnScreenNotifier2D notifier { get; set; }
    public Vector2 MyTarget;
    // public override int MyDamage { get; set; }
    public Vector2 MyGlobalPosition { get; set; }
    [Export] public float MySpeed;
    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
        // MyDamage = 1;
        ConnectToArea();
        ConnectToNotifier();
    }

    // Called every frame. 'delta' is the elapsed time since the previous frame.
    public override void _Process(double delta)
    {
        // GD.Print($"Bullet Position is {this.Position}");
        this.GlobalPosition += MyTarget.Normalized() * MySpeed;
    }
    protected void ConnectToArea()
    {
        this.Connect("body_entered", new Callable(this, "Body_Collided"));
        this.Connect("area_entered", new Callable(this, "Collided")); //Bee's are area2D, so we are checking for are2D objects

    }
    public void Body_Collided(Node2D body)
    {
        GD.Print("Body_Collided");
        Despawn();
    }
    public void Collided(Node2D body)
    {
        // GD.Print("Direct Hit");
        // foreach (Damageable child in body.GetChildren().OfType<Damageable>())
        // {
        //     child.Hit(MyDamage, Vector2.Zero);
        // }
        GD.Print("Area_Collided");
        if (body is not Bullet)
        Despawn();

    }
    public void ConnectToNotifier()
    {
        notifier = GetNode<VisibleOnScreenNotifier2D>("Notifier");
        notifier.Connect("screen_exited", new Callable(this, "Despawn"));
    }
    public void Despawn()
    {
        // GD.Print("Bullet despawned");
        this.QueueFree();
    }

}
