using Godot;
using System;

public partial class StatePaused : State
{
    public override void Enter()
    {
        gameController.mainMenu.Show();

        gameController.mainMenu.GetNode<Button>("VBoxContainer/Button_Resume").GrabFocus();
    }

    public override void Exit()
    {
        gameController.mainMenu.Hide();
    }
}
