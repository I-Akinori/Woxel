using UnityEngine;

public class LightController : MonoBehaviour
{
    private Light light;
    private float time = 0;

    [SerializeField, Range(0, 1)] private float colorSpeed = 0.1f;
    [SerializeField, Range(0, 10)] private float rotSpeed = 0.5f;
    [SerializeField, Range(0, 1)] private float colorOffset = 0f;
    [SerializeField, Range(0, 360)] private float angleOffset = 0f;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        this.light = GetComponent<Light>();
        time = 0f;
    }

    // Update is called once per frame
    void Update()
    {
        time += Time.deltaTime;
        this.light.color = Color.HSVToRGB(Mathf.PingPong(time * colorSpeed + colorOffset, 1), 1, 1);
        this.transform.rotation = Quaternion.Euler(50, time * rotSpeed + angleOffset, 0);
    }
}
