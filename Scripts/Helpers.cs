using Godot;
using System;

public static class Helpers
{
	public static void ExactLerp(ref float currentValue, float targetValue, float acceleration, float precision = 0.01f)
	{
		 if(Mathf.Abs(currentValue - targetValue) > precision)
        {
            currentValue = Mathf.Lerp(currentValue, targetValue, acceleration);
        }
        else
        {
            currentValue = targetValue;
        }
	}
}
