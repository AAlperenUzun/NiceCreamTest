using UnityEngine;

public class NumberFormatter : MonoBehaviour
{
    public static string FormatNumber(double value)
    {
        if (value >= 1000000000)
            return (value / 1000000000.0).ToString("0.##B");
        else if (value >= 1000000)
            return (value / 1000000.0).ToString("0.##M");
        else if (value >= 1000)
            return (value / 1000.0).ToString("0.##K");
        else
            return value.ToString("0.##");
    }
}