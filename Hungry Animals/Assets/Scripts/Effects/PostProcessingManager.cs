using UnityEngine;
using UnityEngine.Rendering.PostProcessing;

public class PostProcessingManager : MonoBehaviour
{ 
    [SerializeField] PostProcessVolume _postProcessVolume;
    ColorGrading _colorGrading;
  
    void Start()
    {
        _postProcessVolume.profile.TryGetSettings(out _colorGrading);
    }

    public void SaturationHighLow(float value){
        _colorGrading.saturation.value = value;
    }
}
