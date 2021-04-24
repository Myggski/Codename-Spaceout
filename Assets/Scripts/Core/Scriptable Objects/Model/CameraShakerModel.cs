using UnityEngine;
using Cinemachine;

/// <summary>
/// Camera shake information
/// </summary>
[CreateAssetMenu(menuName = "Models/CameraShaker")]
public class CameraShakerModel : ScriptableObject
{
  [Tooltip("The amplitude of the camera shake")]
  public float amplitude = 0.6f;
  [Tooltip("The shake frequency")]
  public float frequency = 1;
  [Tooltip("The shake duration in seconds")]
  public float duration = 0.1f;

  public NoiseSettings NoiseSettings;
}
