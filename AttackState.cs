using Godot;
using System;

public partial class AttackState : State
{
    [Export]
    private Vector2 knockbackSpeed = new Vector2(600, 300);
    private Vector2 knockbackDirection;
    private string attackAnimationNode = "attack";
    State returnState, groundState, airState;
    PackedScene bulletScene;
    [Export]
    int bulletsInShot;
    Random rnd;
    [Export]
    int minSpreadDegree, maxSpreadDegree;
    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
        rnd = new Random();
        MyAnimationTree.AnimationFinished += OnAnimationFinished;
        groundState = MyStateMachine.GetNode<State>("Ground");
        airState = MyStateMachine.GetNode<State>("Air");
        bulletScene = (PackedScene)ResourceLoader.Load("res://Prop/bullet.tscn");
    }
    public override void OnEnter()
    {
        knockbackDirection = MyCharacter.GetGlobalMousePosition() - MyCharacter.GlobalPosition;
        playback.Travel(attackAnimationNode);
        MyCharacter.Velocity += -knockbackDirection.Normalized() * knockbackSpeed;
        MyCharacter.MyGun.LookAt(MyCharacter.GetGlobalMousePosition());

        CreateNewBullet(bulletsInShot);
        // MyCharacter.Velocity += -knockbackDirection.Normalized() * new Vector2(500, 500);
    }
    // Called every frame. 'delta' is the elapsed time since the previous frame.
    public override void StateProcess(double delta)
    {
        if (MyCharacter.IsOnFloor())
        returnState = groundState;
        else
        returnState = airState;
        base.StateProcess(delta);
    }
    public void OnAnimationFinished(StringName anim_name)
    {
        if (anim_name == attackAnimationNode)
        {
            nextState = returnState;
        }
    }
    public void CreateNewBullet(int amount)
    {
        for (int i = 0; i < amount; i++)
        {
            Bullet bullet = (Bullet)bulletScene.Instantiate();
            this.AddChild(bullet);
            bullet.MyTarget = knockbackDirection.Normalized().Rotated(rnd.Next(minSpreadDegree, maxSpreadDegree) / 57f); //57 is the amount of degrees in one radian
            bullet.GlobalPosition = MyCharacter.MyGun.GlobalPosition;
        }
    }
}
