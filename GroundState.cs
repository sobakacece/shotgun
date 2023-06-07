using Godot;
using System;

public partial class GroundState : State
{
    private float jumpVelocity;

    [Export]
    State airState, attackState;

    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
        jumpVelocity = MyCharacter.MyJumpVelocity;
        airState = MyStateMachine.GetNode<State>("Air");
        attackState = MyStateMachine.GetNode<State>("Attack");
    }

    // Called every frame. 'delta' is the elapsed time since the previous frame.
    public override void _Process(double delta)
    {
        // if (Input.IsActionPressed("jump") && jumpVelocity <= jumpThreshold)
        // {
        //     jumpVelocity+= jumpHolding;
        // }
    }

    public override void StateInput(InputEvent @event)
    {
        if (@event.IsActionReleased("jump"))
        {
            Jump();
        }
        if (@event.IsActionPressed("attack"))
        {
            Attack();
        }
    }

    public override void StateProcess(double delta)
    {
        // if (Input.IsActionPressed("jump") && jumpVelocity > jumpThreshold)
        // {
        //     // GD.Print(jumpVelocity);
        //     jumpVelocity -= jumpHolding;
        // }
    }

    public override void OnEnter()
    {
        playback.Travel("walk");
    }
    public void Attack()
    {
        nextState = attackState;
    }

    public void Jump()
    {
        MyCharacter.Velocity = new Vector2(MyCharacter.Velocity.X, jumpVelocity);
        // playback.Travel("jump");
        // MyAudio.Play();
        nextState = airState;
    }
}
