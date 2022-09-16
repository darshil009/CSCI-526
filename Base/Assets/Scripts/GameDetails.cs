using UnityEngine;

public struct GemsCount
{
    public int Green;
    public int Red;
    public int Blue;
}

public static class GameDetails {
    public static readonly string BlueGemsTag = "blue_gems";
    public static readonly string RedGemsTag = "red_gems";
    public static readonly string GreenGemsTag = "green_gems";
    public static readonly string SpikeBallTag = "SPIKE_BALL";

    public static GemsCount NumGems;
}