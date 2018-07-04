using UnityEngine;
using System.Collections;

[ExecuteInEditMode]
[RequireComponent(typeof(Camera))]
[AddComponentMenu("Image Effects/Cutie 3/AAA - Glass")]
public class AAAGlass : MonoBehaviour
{

    public Material material;

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    // Called by the camera to apply the image effect
    void OnRenderImage(RenderTexture source, RenderTexture destination)
    {

        //mat is the material containing your shader
        Graphics.Blit(source, destination, material);
    }
}
