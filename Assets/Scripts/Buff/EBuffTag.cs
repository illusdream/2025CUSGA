using System;

[Flags]
public enum EBuffTag
{
      None = 0,
      Control = 1 << 0,
      UnRemoveAbleControl = 1 << 1,
}