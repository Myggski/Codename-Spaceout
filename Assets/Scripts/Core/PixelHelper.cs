using UnityEngine;

/// <summary>
/// This class has helping methods when working with pixels
/// </summary>
public static class PixelHelper
{
  private static float unit = 16f;
  /// <summary>
  /// Converts pixels to units
  /// </summary>
  /// <param name="pixels">16x16 pixels = (1,1)</param>
  /// <returns></returns>
  public static float CovertPixelsToUnits(float pixels)
  {
    return pixels / unit;
  }
}
