using UnityEngine;

public class DemoControll : MonoBehaviour
{
    public DemonicAltarController altarScript;
    private bool altarStarted, fadeInLight, altarStopped;

    public Transform altarTF;
    public Material demonSky;
    private float lightMaxIntens, lightTargetIntens, timer;
    public Light mainLight;
    public GameObject privacyWall;
    public Material standardSky;

    // Use this for initialization
    private void Start()
    {
        lightMaxIntens = mainLight.intensity;
        //mainLight.intensity = 0;
        lightTargetIntens = lightMaxIntens;
    }

    // Update is called once per frame
    private void Update()
    {
        altarTF.Rotate(Vector3.up, 10 * Time.deltaTime);

        mainLight.intensity = Mathf.MoveTowards(mainLight.intensity, lightTargetIntens, Time.deltaTime * 0.15f);
    }

    public void OnTriggerEnter()
    {
        if (privacyWall.activeInHierarchy) privacyWall.SetActive(false);
        if (RenderSettings.skybox.name == "standardSky")
        {
            RenderSettings.skybox = demonSky;
            DynamicGI.UpdateEnvironment();
        }
        else if (RenderSettings.skybox.name == "demonSky")
        {
            RenderSettings.skybox = standardSky;
            DynamicGI.UpdateEnvironment();
        }

        altarScript.ActivateDeactivateAltar();
        if (lightTargetIntens != 0)
            lightTargetIntens = 0;
        else lightTargetIntens = lightMaxIntens;
    }

    public void ButtonToggleAltar()
    {
        altarScript.ActivateDeactivateAltar();
    }

    public void ButtonToggleLight()
    {
        if (lightTargetIntens != 0)
            lightTargetIntens = 0;
        else lightTargetIntens = lightMaxIntens;
    }
}