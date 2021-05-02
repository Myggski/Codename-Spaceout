/// <summary>
/// Helper-class that helps with pixel calculations
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
