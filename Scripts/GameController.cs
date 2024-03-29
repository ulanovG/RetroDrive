using Godot;
using System;
using System.Collections.Generic;

public partial class GameController : Node
{
	private Dictionary<string, State> states;
	private State currentState;
	[Export] public Node initialState;


	[Export] public Control mainMenu;
	[Export] public Node3D car;
	[Export] public MeshInstance3D ground;
	public ShaderMaterial groundShader;

	[Export] public float currentAngle = 0;
	[Export] public float maxAngle = 0.785398f;
	[Export] public float rotationAcceleration = 0.2f;

	[Export] public float currentSpeed = 0;
	[Export] public float targetSpeed = 0;
	[Export] public float currentAcceleration = 0.2f;
	[Export] public float targetAcceleration = 0.2f;

	[Export] public Timer spawnTimer;
	[Export] public int spawnCount;
	[Export] public Node3D spawnLocation;
	[Export] public PackedScene pillar;

	public override void _Ready()
	{
		groundShader = ground.GetSurfaceOverrideMaterial(0) as ShaderMaterial;

		states = new Dictionary<string, State>();
		foreach(var node in GetChild(0).GetChildren())
		{
			if(node is State s)
			{
				states[node.Name] = s;
				s.gameController = this;
				s.Ready();
				s.Exit();
			}
		}

		currentState = initialState as State;
		currentState.Enter();
	}

	public override void _Process(double delta)
	{
		currentState.Update(delta);
	}

    public override void _UnhandledInput(InputEvent @event)
    {
        currentState.HandleInput(@event);
    }

	public void Transition (string key)
	{
		if(states.ContainsKey(key) && currentState != states[key])
		{
			currentState.Exit();
			currentState = states[key];
			currentState.Enter();
		}
		else
		{
			GD.Print("No state: " + key);
		}
	}
}
