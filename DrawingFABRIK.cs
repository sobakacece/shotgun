using Godot;
using System;
using System.Collections.Generic;
using System.Linq;

public partial class DrawingFABRIK : Node2D
{
    [Export] Marker2D startMark, goalMark;
    [Export] float speed;
    Vector2 start, goal;
    List<Vector2> joints = new List<Vector2>();
    [Export] float[] length = new float[2];
    [Export] int[] angles = new int[2];
	Joint joint1, joint2;
    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {

        startMark = GetNode<Marker2D>("Start");
        // goalMark = GetNode<Marker2D>("Goal");
        start = startMark.Position;
        // joints.Insert(0, start);


        // for (int i = 0; i < length.Length; i++)
        // {
        //     joints.Add(GetJointPoint(joints[i], length[i], angles[i]));
        // }
		// joints.Add(goal);
		joint1 = new Joint(start, 0, 5);
		joint2 = new Joint(joint1, 45, 5);
		AddChild(joint1);
		AddChild(joint2);
		Joint currentJoint = joint2;
		for (int i = 0; i < 100; i++)
		{
			Joint jointGen = new Joint(currentJoint, 90, 10);
			AddChild(jointGen);
			currentJoint = jointGen;
		}
    }


    // public Vector2 GetJointPoint(Vector2 start, float length, int angle)
    // {
    //     float radian = angle * MathF.PI / 180;
    //     Vector2 finish = new Vector2();
    //     finish.X = start.X + length * MathF.Cos(radian);
    //     finish.Y = start.Y + length * MathF.Sin(radian);
    //     return finish;
    // }

    // // Called every frame. 'delta' is the elapsed time since the previous frame.
    public override void _Process(double delta)
    {
	// 	joint1.Follow(GetGlobalMousePosition());
	// 	joint2.Follow(Vector2.Zero);
		// joint1.Follow(GetGlobalMousePosition());
		// joint1._Process(delta);
        // Vector2 direction = Input.GetVector("left", "right", "up", "down");
        // goal = joints.Last();
        // goal += direction * speed;
        // joints[joints.Count - 1] = goal;
        // QueueRedraw();
    }
    // public override void _Draw()
    // {
    //     for (int i = 0; i < joints.Count - 1; i++)
    //     {
    //         DrawCircle(joints[i], 5, Colors.Red);
    //         DrawLine(joints[i], joints[i + 1], Colors.Green, 1);
    //     }
    //     DrawCircle(joints.Last(), 5, Colors.Purple);
    // }
}
