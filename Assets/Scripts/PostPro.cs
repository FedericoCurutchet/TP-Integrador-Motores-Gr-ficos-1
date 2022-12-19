using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;
using UnityEngine;
public class PostPro : MonoBehaviour
{
    private Bloom bloom = null;
    void Start()
    {
        Volume volume = GetComponent<Volume>();
        volume.sharedProfile.TryGet<Bloom>(out bloom); 
        if (bloom != null)
        {
            bloom.intensity.value = 1.0f;
        }
    }

}