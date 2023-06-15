using Godot;
using System;

public partial class Joint : Node2D
{
    Vector2 start, finish;
    float angle, length;
	Joint parent;

    public Joint(Vector2 pos, float _angle, float _length)
    {
		parent = null;
        start = pos;
        length = _length;
        angle = _angle;
    }
	 public Joint(Joint _parent, float _angle, float _length)
    {
        parent = _parent;
        length = _length;
        angle = _angle;
		start = _parent.finish;
		// Follow(_parent.start);
    }
    public void CalculateFinish()
    {
        float radian = angle * MathF.PI / 180;
        finish.X = start.X + length * MathF.Cos(radian);
        finish.Y = start.Y + length * MathF.Sin(radian);
    }
    public void Follow(Vector2 _target)
    {
        // finish = finish.Lerp(GetGlobalMousePosition(), 30);
        // GD.Print("Follows");
		if (parent != null)
		{
			_target = parent.start;
			// GD.Print(parent.ToString());
		}
        Vector2 dir = _target - start;
        angle = dir.Angle() * 180 / MathF.PI;
        dir = -(dir.Normalized() * length);
		start = _target + dir;
    }

    public override void _Draw()
    {
        // DrawCircle(start, 8, Colors.Red);
        DrawLine(start, finish, Colors.Green, 3);
        // DrawCircle(finish, 8, Colors.Blue);
    }
    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
    }

    // Called every frame. 'delta' is the elapsed time since the previous frame.
    public override void _Process(double delta)
    {
        CalculateFinish();
        QueueRedraw();
		Follow(GetGlobalMousePosition());
		// Follow(GetGlobalMousePosition());

        // GD.Print("Processing");
    }
}
