using UnityEngine;

public class Singleton<T> : MonoBehaviour where T : MonoBehaviour
{
  // Check to see if we're about to be destroyed.
  private static bool shuttingDown = false;
  /// <summary>
  /// Locker variable, to ensures that Instance will be instantiated once
  /// </summary>
  /// <returns></returns>
  private static readonly object locker = new object();
  /// <summary>
  /// Instance of the Singleton
  /// </summary>
  private static T instance;

  /// <summary>
  /// Access singleton instance through this propriety.
  /// </summary>
  public static T Instance
  {
    get
    {
      if (Singleton<T>.shuttingDown)
      {
        return null;
      }

      lock (Singleton<T>.locker)
      {
        if (Singleton<T>.instance == null)
        {
          // Search for existing instance.
          Singleton<T>.instance = (T)FindObjectOfType(typeof(T));

          // Create new instance if one doesn't already exist.
          if (Singleton<T>.instance == null)
          {
            // Need to create a new GameObject to attach the singleton to.
            var singletonObject = new GameObject();
            Singleton<T>.instance = singletonObject.AddComponent<T>();
            singletonObject.name = $"{typeof(T).ToString()} (Singleton)";

            // Make instance persistent.
            DontDestroyOnLoad(singletonObject);
          }
        }

        return Singleton<T>.instance;
      }
    }
  }

  private void ShutDown()
  {
    Singleton<T>.shuttingDown = true;
  }

  private void OnApplicationQuit()
  {
    this.ShutDown();
  }

  private void OnDestroy()
  {
    this.ShutDown();
  }
}