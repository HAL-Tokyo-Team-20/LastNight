using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;


public class CubeMeshManager : MonoBehaviour
{

    public List<Material> materials;

    private void Awake()
    {
        Mesh mesh = new Mesh();
        mesh = this.gameObject.GetComponent<MeshFilter>().mesh;
        int[] triangles;
        mesh.subMeshCount = 6;

        this.gameObject.GetComponent<MeshRenderer>().materials = materials.ToArray();

        triangles = mesh.triangles;

        // Back
        mesh.SetTriangles(GetRangeArray(triangles, 0, 5), 0);
        // Top
        mesh.SetTriangles(GetRangeArray(triangles, 6, 11), 1);
        // Front
        mesh.SetTriangles(GetRangeArray(triangles, 12, 17), 2);
        // Buttom
        mesh.SetTriangles(GetRangeArray(triangles, 18, 23), 3);
        // Left
        mesh.SetTriangles(GetRangeArray(triangles, 24, 29), 4);
        // Right
        mesh.SetTriangles(GetRangeArray(triangles, 30, 35), 5);


    }

    public int[] GetRangeArray(int[] SourceArray, int StartIndex, int EndIndex)
    {
        try
        {
            int[] result = new int[EndIndex - StartIndex + 1];
            for (int i = 0; i <= EndIndex - StartIndex; i++) result[i] = SourceArray[i + StartIndex];
            return result;
        }
        catch (IndexOutOfRangeException ex)
        {
            throw new Exception(ex.Message);
        }
    }
}
