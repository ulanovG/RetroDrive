using Godot;
using System;

public partial class StatePlayingCar : State
{
    private float speedMult = -0.1f;
    private float obstaclesSpeedMultX = 0.49f;
    private float obstaclesSpeedMultZ = 0.575f;
    private double shiftX = 0;
    private double shiftY = 0;
    private Vector3 rotationVec= new Vector3(0, 0, 0);

    public override void Enter()
    {
        gameController.spawnTimer.Start();
    }

    public override void Update(double delta)
    {

        if(gameController.currentSpeed != gameController.targetSpeed)
        {
            Helpers.ExactLerp(ref gameController.currentSpeed, gameController.targetSpeed, gameController.currentAcceleration);
        }
        
		if (Input.IsActionPressed("left"))
    	{
            if(gameController.currentAngle != gameController.maxAngle)
            {
                rotationVec.Y = gameController.currentAngle;
                Helpers.ExactLerp(ref gameController.currentAngle, gameController.maxAngle, gameController.rotationAcceleration);
                gameController.car.Rotation = rotationVec;
            }
    	}
        else if (Input.IsActionPressed("right"))
    	{
            if(gameController.currentAngle != -gameController.maxAngle)
            {
                rotationVec.Y = gameController.currentAngle;
                Helpers.ExactLerp(ref gameController.currentAngle, -gameController.maxAngle, gameController.rotationAcceleration);
                gameController.car.Rotation = rotationVec;
            }
    	}
        else
        {
            if(gameController.currentAngle != 0)
            {
                rotationVec.Y = gameController.currentAngle;
                Helpers.ExactLerp(ref gameController.currentAngle, 0, gameController.rotationAcceleration);
                gameController.car.Rotation = rotationVec;
            }
        }

        if(gameController.currentSpeed != 0)
        {
            shiftX += gameController.currentSpeed * speedMult * Mathf.Sin(gameController.currentAngle) * delta;
            shiftY += gameController.currentSpeed * speedMult * Mathf.Cos(gameController.currentAngle) * delta;
            gameController.groundShader.SetShaderParameter("shift_x", shiftX);
            gameController.groundShader.SetShaderParameter("shift_y", shiftY);

            float posX;
            float posZ;
            foreach(Node3D obs in gameController.spawnLocation.GetChildren())
            {
                posX = obs.Position.X + gameController.currentSpeed * obstaclesSpeedMultX * Mathf.Sin(gameController.currentAngle) * (float)delta;
                posZ = obs.Position.Z + gameController.currentSpeed * obstaclesSpeedMultZ * Mathf.Cos(gameController.currentAngle) * (float)delta;
                obs.Position = new Vector3(posX, 0, posZ);
            }
        }
    }

    public override void HandleInput(InputEvent @event)
    {
        if (@event.IsActionPressed("pause"))
        {
            gameController.Transition("Paused");
        }
    }

    public void OnSpawnTimerTimeout()
    {
        RandomNumberGenerator rnjesus = new RandomNumberGenerator();

        for (int i = 0; i < gameController.spawnCount; i++)
        {
            rnjesus.Randomize();
            Node3D pillar = gameController.pillar.Instantiate<Node3D>();
            pillar.Position = new Vector3(rnjesus.RandiRange(-215, 215), 0, 0);
            gameController.spawnLocation.AddChild(pillar);
        }
    }

    public override void Exit()
    {
        gameController.spawnTimer.Stop();
    }
}
