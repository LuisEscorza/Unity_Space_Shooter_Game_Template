using UnityEngine;

public static class ScreenUtils
{
    public static float ScreenLimitLeft
    {
        get { return Camera.main.ScreenToWorldPoint(new Vector3(0, 0, 0)).x; }
    }

    public static float ScreenLimitRight
    {
        get { return Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, 0, 0)).x; }
    }

    public static float ScreenLimitTop
    {
        get { return Camera.main.ScreenToWorldPoint(new Vector3(0, Screen.height, 0)).y; }
    }

    public static float ScreenLimitBottom
    {
        get { return Camera.main.ScreenToWorldPoint(new Vector3(0, 0, 0)).y; }
    }
}
