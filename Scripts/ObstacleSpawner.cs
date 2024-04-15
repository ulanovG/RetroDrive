using Godot;
using System;
using System.Runtime.InteropServices;

public partial class ObstacleSpawner
{
	private float range = 180;
	public int spawnCount;
	private Node3D spawnLocation;
	public PackedScene obstacle;

	public ObstacleSpawner(int SpawnCount, Node3D SpawnLocation, PackedScene Obstacle)
	{
		spawnCount = SpawnCount;
		spawnLocation = SpawnLocation;
		obstacle = Obstacle;
	}

	public void Spawn()
	{
		RandomNumberGenerator rnjesus = new RandomNumberGenerator();
		var intervalLength = (range * 2) / spawnCount;
		var median = intervalLength / 2;


        for (int i = 0; i < spawnCount; i++)
        {
            rnjesus.Randomize();
			//var coord = Mathf.Clamp(rnjesus.Randfn(median + (i * intervalLength), 5f), (i * intervalLength), ((i + 1) * intervalLength)) - range;
			var coord = rnjesus.RandfRange((i * intervalLength), ((i + 1) * intervalLength)) - range;
            Node3D pillar = obstacle.Instantiate<Node3D>();
            pillar.Position = new Vector3(coord, 0, 0);
            spawnLocation.AddChild(pillar);
            (pillar.FindChild("AnimationPlayer") as AnimationPlayer).Play("pillar_appear");
        }
	}
}
