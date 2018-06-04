using System.Collections;
using UnityEngine;

public class CameraShake : MonoBehaviour
{
    public static float shakeAmount = 0.1f;
    public static float shakeDuration = 2f;
    private static CameraShake instance;

    private Vector3 camOriginalPos;


    private void Start()
    {
        camOriginalPos = gameObject.transform.localPosition;
        instance = this;
    }

    public static void Shake()
    {
        instance.camOriginalPos = instance.gameObject.transform.localPosition;
        instance.StopAllCoroutines();
        instance.StartCoroutine(instance.doShake());
    }


    public IEnumerator doShake()
    {
        var endTime = Time.time + shakeDuration;

        while (Time.time < endTime)
        {
            transform.localPosition = camOriginalPos + Random.insideUnitSphere * shakeAmount;
            //shakeDuration -= Time.deltaTime;

            yield return null;
        }

        transform.localPosition = camOriginalPos;
    }
}