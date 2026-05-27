using System;

public interface IFormSubmitable
{
    public void EnableSubmit(bool enable);
    public Action OnPass { get; set; }
}