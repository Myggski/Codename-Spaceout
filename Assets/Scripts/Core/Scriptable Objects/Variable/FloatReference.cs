using System;

[Serializable]
public class FloatReference
{
  public bool UseConstant = false;
  public float ConstantValue;
  public FloatVariable Variable;

  public FloatReference()
  { }

  public FloatReference(float value)
  {
    UseConstant = false;
    ConstantValue = value;
  }

  public float Value
  {
    get
    {
      if (this.UseConstant)
      {
        return this.ConstantValue;
      }

      if (this.Variable != null && !this.Variable.Value.Equals(null))
      {
        return Variable.Value;
      }

      return 0f;
    }
  }

  public static implicit operator float(FloatReference reference)
  {
    return reference.Value;
  }
}