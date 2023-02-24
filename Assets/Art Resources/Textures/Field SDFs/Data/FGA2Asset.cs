using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[ExecuteInEditMode]
public class FGA2Asset : MonoBehaviour
{
    public bool SaveFile = false;
    public TextAsset fgaFile;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (SaveFile)
        {
            SaveFile = false;
            Texture3D aa = DeserializeVectorField(fgaFile);
            Save3DTexture(aa);
        }
    }

    public static Texture3D DeserializeVectorField(TextAsset fgaFile)
    {
        string FullFile = fgaFile.text;
        string[] AllFloats = FullFile.Split(',');

        float Length = (float)AllFloats.Length - 10;
        int LengthPerSide = Mathf.RoundToInt(Mathf.Pow(Length / 3f, 1f / 3f));

        Texture3D VectorField = new Texture3D(LengthPerSide, LengthPerSide, LengthPerSide, TextureFormat.RGBAFloat, false);
        VectorField.wrapMode = TextureWrapMode.Clamp;

        float[] ConvertedFloats = new float[(int)Length];

        for (int i = 0; i < ConvertedFloats.Length - 1; i++)
        {
            ConvertedFloats[i] = float.Parse(AllFloats[i + 9]);
        }



        Color[] col = new Color[Mathf.RoundToInt(Length / 3f)];

        for (int i = 0; i < col.Length - 1; i++)
        {
            Vector3 v = Vector3.Normalize(new Vector3(ConvertedFloats[i * 3], ConvertedFloats[i * 3 + 1], ConvertedFloats[i * 3 + 2]));
            col[i] = new Color(v.x, v.y, v.z, 1f);
        }

        VectorField.SetPixels(col);
        VectorField.Apply(false);
        return VectorField;
    }

    public void Save3DTexture(Texture3D newTexture)
    {
        AssetDatabase.CreateAsset(newTexture, "Assets/" + fgaFile.name +  ".asset");
    }
}
