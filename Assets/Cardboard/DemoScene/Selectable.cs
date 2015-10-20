using UnityEngine;
using System.Collections;

public class Selectable : MonoBehaviour
{
	public Color selectedColor;
	public float selectedLineWidth = 0.01f;
	public Shader outlineShader;
	// Use this for initialization
	Shader originalShader;
	bool selected = false;

    private MeshRenderer MyRenderer;
	void Start ()
	{
        MyRenderer = GetComponent<MeshRenderer> ();
        originalShader = MyRenderer.material.shader;

        //		Mesh mymesh = GetComponentInChildren<MeshFilter> ().mesh;
        //		int maxVerNum = mymesh.vertices.Length;
        //		Vector3[] vertexColor = new Vector3[maxVerNum];
        //		for (int i = 0; i < maxVerNum; ++i) 
        //		{
        //			Vector3 avgNormal = mymesh.normals[i];
        //			int avgNormalCount = 0;
        //			for(int j = 0; j < maxVerNum; j++)
        //			{
        //
        //				if((mymesh.vertices[i] - mymesh.vertices[j]).sqrMagnitude < 0.001f)
        //				{
        //					avgNormal += mymesh.normals[j];
        //					++avgNormalCount;
        //				}
        //			}
        //			vertexColor[i] = avgNormal/avgNormalCount;
        //
        //		}
        //
        //		for (int i = 0; i < maxVerNum; ++i) {
        //			Debug.Log(mymesh.normals[i] + "-> "+vertexColor[i]);
        //			mymesh.normals[i] =vertexColor[i];
        //		}

    }
	
	// Update is called once per frame
	void Update ()
	{
		if (selected) {
			
			MyRenderer.material.SetFloat ("_CubeScale", GetComponent<Transform>().localScale.magnitude);
		}
	}

	public bool selectBy(GameObject selector)
	{
		if (selector) {
			var MyRenderer = GetComponent<MeshRenderer> ();
			MyRenderer.material.shader = outlineShader;
			MyRenderer.material.SetColor ("_OutlineColor", selectedColor);
			MyRenderer.material.SetFloat ("_Outline", selectedLineWidth);
			MyRenderer.material.SetFloat ("_CubeScale", GetComponent<Transform>().localScale.magnitude);
			selected = true;
			return true;
		}
		return false;
	}

	public bool deSelect()
	{
        GetComponent<MeshRenderer> ().material.shader = originalShader;
		selected = false;
		return true;
	}
}

