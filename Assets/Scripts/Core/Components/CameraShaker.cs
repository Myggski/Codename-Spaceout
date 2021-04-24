using UnityEngine;
using Cinemachine;

/// <summary>
/// Shakes camera depending on the values in CameraShakerModel
/// For example when FiredProjectile-event is triggered, and a projectile from player has spawned.
/// </summary>
public class CameraShaker : MonoBehaviour
{
  public CameraShakerModel Model;
  private CinemachineBasicMultiChannelPerlin virtualCameraNoise;
  private float shakeTimer = 0f;

  private void Awake()
  {
    this.Setup();
  }

  private void Update()
  {
    this.Shake();
  }

  /// <summary>
  /// Getting the virtual camera to apply the shake effect, and resets any leftover effects
  /// </summary>
  private void Setup()
  {
    this.virtualCameraNoise = Camera.main.GetComponentInChildren<CinemachineVirtualCamera>()?.GetCinemachineComponent<Cinemachine.CinemachineBasicMultiChannelPerlin>();

    if (this.virtualCameraNoise != null)
    {
      this.ResetShake();
    }
  }

  /// <summary>
  /// Resets the shake properties on the camera
  /// </summary>
  private void ResetShake()
  {
    this.virtualCameraNoise.m_AmplitudeGain = 0f;
    this.virtualCameraNoise.m_FrequencyGain = 0f;
    this.virtualCameraNoise.m_PivotOffset = Vector3.zero;
    Camera.main.transform.rotation = Quaternion.Euler(0f, 0f, 0f);
  }

  /// <summary>
  /// Sets the duration of the shake, and finds the virtual camera
  /// </summary>
  public void StartShake()
  {
    if (this.virtualCameraNoise != null && this.shakeTimer.Equals(0f))
    {
      this.shakeTimer = this.Model.duration;
      //this.virtualCameraNoise.m_PivotOffset = (Vector2)Camera.main.ScreenToWorldPoint(Input.mousePosition).normalized;
    }
  }

  /// <summary>
  /// Shakes the camera until the duration time has passed
  /// </summary>
  private void Shake()
  {
    if (this.shakeTimer > 0)
    {
      this.shakeTimer -= Time.deltaTime;

      if (this.shakeTimer <= 0)
      {
        this.shakeTimer = 0f;
        this.ResetShake();
      }
      else
      {

        this.virtualCameraNoise.m_AmplitudeGain = 1f;
        this.virtualCameraNoise.m_FrequencyGain = 1f;

        Cinemachine.NoiseSettings.TransformNoiseParams transformNoiseParams = new NoiseSettings.TransformNoiseParams();
        transformNoiseParams.X = new NoiseSettings.NoiseParams();
        transformNoiseParams.X.Frequency = Mathf.Lerp(0f, this.Model.frequency, this.shakeTimer / this.Model.duration);
        transformNoiseParams.X.Amplitude = Mathf.Lerp(0f, this.Model.amplitude, this.shakeTimer / this.Model.duration);

        transformNoiseParams.Y = new NoiseSettings.NoiseParams();
        transformNoiseParams.Y.Frequency = Mathf.Lerp(0f, this.Model.frequency, this.shakeTimer / this.Model.duration);
        transformNoiseParams.Y.Amplitude = Mathf.Lerp(0f, this.Model.amplitude, this.shakeTimer / this.Model.duration);

        this.Model.NoiseSettings.OrientationNoise = new Cinemachine.NoiseSettings.TransformNoiseParams[] { transformNoiseParams };
        this.virtualCameraNoise.m_NoiseProfile = this.Model.NoiseSettings;
      }
    }
  }
}