using Godot;
using System;

public partial class Player : CharacterBody2D
{
    [Export]
    public float Speed = 300.0f;

    [Export]
    public float MyJumpVelocity = -400.0f;
    [Export]
    CharacterStateMachine MyStateMachine;
    public Node2D MyGun { get => GetNode<Node2D>("Gun"); }
    // Get the gravity from the project settings to be synced with RigidBody nodes.
    public float gravity = ProjectSettings.GetSetting("physics/2d/default_gravity").AsSingle();
    public AnimationTree MyAnimationTree;
    public override void _Ready()
    {
        MyAnimationTree = GetNode<AnimationTree>("AnimationTree");
        MyAnimationTree.Active = true;
    }

    public override void _PhysicsProcess(double delta)
    {
        Vector2 velocity = Velocity;

        if (!IsOnFloor())
            velocity.Y += gravity * (float)delta;


        Vector2 direction = Input.GetVector("left", "right", "up", "down");
        if (direction != Vector2.Zero && MyStateMachine.IsMoveable())
        {
            velocity.X = direction.X * Speed;
        }
        else if (MyStateMachine.CurrentState.ToString() != "Attack")
        {
            velocity.X = Mathf.MoveToward(Velocity.X, 0, Speed);
        }

        Velocity = velocity;
        MoveAndSlide();
    }
}
