using UnityEngine;

[CreateAssetMenu(menuName = "Variables/StringVariable")]

public class StringVariable : ScriptableObject
{
  [Multiline]
  public string DeveloperDescription = "";
  [SerializeField]
  private string value = "";

  public string Value
  {
    get { return value; }
    set { this.value = value; }
  }
}