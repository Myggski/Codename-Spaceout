#if UNITY_EDITOR
using UnityEditor;
#endif

using UnityEngine;
using UnityEngine.Rendering;

#if UNITY_EDITOR
[InitializeOnLoad]
#endif
public class TransparencySortGraphicsHelper
{
  static TransparencySortGraphicsHelper()
  {
    OnLoad();
  }

  [RuntimeInitializeOnLoadMethod]
  static void OnLoad()
  {
    GraphicsSettings.transparencySortMode = TransparencySortMode.CustomAxis;
    GraphicsSettings.transparencySortAxis = new Vector3(0f, 1f, 0);
  }
}