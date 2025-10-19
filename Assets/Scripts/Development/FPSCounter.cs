using TMPro;
using UnityEngine;
using UnityEngine.Profiling;
public class FPSCounter : MonoBehaviour
{
    public TMP_Text fpsTxt;
    //private float deltaTime;

    // Update is called once per frame
    void Update()
    {
        long usedMemory = Profiler.GetTotalAllocatedMemoryLong();
        float usedMB = usedMemory / (1024f * 1024f);
        fpsTxt.text = $"Memoria usada: {usedMB:F2} MB";
    }
}
