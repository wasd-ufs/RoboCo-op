public class InputButton
{
    public bool Pressed { get; private set; }
    public bool JustPressed { get; private set; }
    public bool JustReleased { get; private set; }

    public void Update(bool isPressed)
    {
        JustPressed = !Pressed && isPressed;
        JustReleased = Pressed && !isPressed;
        Pressed = isPressed;
    }

    public void ResetFrame()
    {
        JustPressed = false;
        JustReleased = false;
    }
}


