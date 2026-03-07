using System;
using UnityEngine;

public class CameraBobSystem : MonoBehaviour
{

    [Range(0.0001f, 0.1f)]
    public float Amount = 0.03f;
    
    [Range(1f, 30f)]
    public float Frequency = 15.0f;

    [Range(10f, 100f)]
    public float Smooth = 10.0f;

    [Range(0.01f, 1f)]
    public float bobbingPlayerSpeedSoftening = 0.25f;

    Vector3 StartPos;

    void Start()
    {
        StartPos = this.transform.localPosition;
    }

    void Update()
    {
        StartCameraBob();
        StopCameraBob();
    }

    private Vector3 StartCameraBob()
    {
        Debug.Log("start camera bob");
        Vector3 pos = Vector3.zero;
        pos.y += Mathf.Lerp(pos.y, Mathf.Sin(Time.time * Frequency) * Amount * 1.4f, Smooth * Time.deltaTime * (PlayerScript.instance.playerMovementScript.currentPlayerMoveSpeed * bobbingPlayerSpeedSoftening));
        pos.x += Mathf.Lerp(pos.x, Mathf.Cos(Time.time * Frequency / 2f) * Amount * 1.6f, Smooth * Time.deltaTime * (PlayerScript.instance.playerMovementScript.currentPlayerMoveSpeed * bobbingPlayerSpeedSoftening));
        this.transform.localPosition += pos;

        return pos;
    }

    private void StopCameraBob()
    {
        if (this.transform.localPosition == StartPos) return;
        this.transform.localPosition = Vector3.Lerp(this.transform.localPosition, StartPos, 1 * Time.deltaTime);
    }
}
