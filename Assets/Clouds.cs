using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class Clouds : MonoBehaviour
{
    public Shader CloudShader;
    public float MinHeight = 0.0f;
    public float MaxHeight = 5.0f;
    public float FadeDist = 2;
    public float Scale = 5;
    public float Steps = 50;
    public Texture ValueNoiseImage;
    public Transform Sun;
    
    Camera _Cam;

    public Material Material
    {
        get
        {
            if (_Material == null && CloudShader != null)
            {
                _Material = new Material(CloudShader);
            }

            if(_Material != null && CloudShader == null)
            {
                DestroyImmediate(_Material);
            }
            if(_Material != null && CloudShader != null && CloudShader != _Material.shader)
            {
                DestroyImmediate(_Material);
                _Material = new Material(CloudShader);
            }
            return _Material;
        }
    }

    Material _Material;

    // Start is called before the first frame update
    void Start()
    {
        if (_Material)
            DestroyImmediate(_Material);
    }

    Matrix4x4 GetFrustumCorners()
    {
        Matrix4x4 frustumCorners = Matrix4x4.identity;
        Vector3[] fCorners = new Vector3[4];

        _Cam.CalculateFrustumCorners(new Rect(0, 0, 1, 1), _Cam.farClipPlane, Camera.MonoOrStereoscopicEye.Mono, fCorners);

        frustumCorners.SetRow(0, fCorners[1]);
        frustumCorners.SetRow(1, fCorners[2]);
        frustumCorners.SetRow(2, fCorners[3]);
        frustumCorners.SetRow(3, fCorners[0]);
        return frustumCorners;
    }
 
}
