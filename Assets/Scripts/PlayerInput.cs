using UnityEngine;

public class PlayerInput : MonoBehaviour
{
    private static readonly string AxisVertical = "Vertical";
    private static readonly string AxisHorizontal = "Horizontal";
    private static readonly string FireButton = "Fire1";
    private static readonly string ReloadButton = "Reload";

    public float Move { get; private set; }
    public float Rotate { get; private set; }
    public bool Fire {  get; private set; }
    public bool Reload { get; private set; }

    private void Update()
    {
        Move = Input.GetAxis(AxisVertical);
        Rotate = Input.GetAxis(AxisHorizontal);

        Fire = Input.GetButton(FireButton);
        Reload = Input.GetButton(ReloadButton);
    }
}
