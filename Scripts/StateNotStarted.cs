using Godot;
using System;

public partial class StateNotStarted : State
{
    public override void Enter()
    {
		gameController.mainMenu.GetNode<Control>("VBoxContainer/Button_Resume").Hide();
		gameController.mainMenu.GetNode<Control>("VBoxContainer/Button_Start").Show();
        gameController.mainMenu.Show();

        gameController.mainMenu.GetNode<Button>("VBoxContainer/Button_Start").GrabFocus();
    }

    public override void Exit()
    {
		gameController.mainMenu.GetNode<Control>("VBoxContainer/Button_Resume").Show();
		gameController.mainMenu.GetNode<Control>("VBoxContainer/Button_Start").Hide();
        gameController.mainMenu.Hide();
		
    }
}
