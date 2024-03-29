using Godot;
using System;

public partial class MainMenu : Control
{
	[Export]
    public GameController gameController;

	private void OnButtonStartPressed()
	{
		gameController.Transition("PlayingCar");
	}

	private void OnButtonResumePressed()
	{
		gameController.Transition("PlayingCar");
	}

	private void OnButtonExitPressed()
	{
		GetTree().Quit();
	}
}
