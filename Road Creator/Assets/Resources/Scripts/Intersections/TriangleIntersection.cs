﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriangleIntersection : MonoBehaviour {

    public float width = 3;
    public float height = 3;
    public float heightOffset = 0.02f;
    public Material centerMaterial;

    public bool downConnection = true;
    public float downConnectionWidth = 1.5f;
    public float downConnectionHeight = 1;
    public Material downConnectionMaterial;
    public int downConnectionResolution = 2;

    public bool leftConnection = true;
    public float leftConnectionWidth = 1.5f;
    public float leftConnectionHeight = 1;
    public Material leftConnectionMaterial;
    public int leftConnectionResolution = 2;

    public bool rightConnection = true;
    public float rightConnectionWidth = 1.5f;
    public float rightConnectionHeight = 1;
    public Material rightConnectionMaterial;
    public int rightConnectionResolution = 2;

    public GlobalSettings globalSettings;

    public void GenerateMeshes()
    {
        if (centerMaterial == null)
        {
            centerMaterial = Resources.Load("Materials/Intersections/Grid Intersection") as Material;
        }

        if (downConnectionMaterial == null)
        {
            downConnectionMaterial = Resources.Load("Materials/Intersections/Intersection Connections/2L Connection") as Material;
        }

        if (leftConnectionMaterial == null)
        {
            leftConnectionMaterial = Resources.Load("Materials/Intersections/Intersection Connections/2L Connection") as Material;
        }

        if (rightConnectionMaterial == null)
        {
            rightConnectionMaterial = Resources.Load("Materials/Intersections/Intersection Connections/2L Connection") as Material;
        }

        GenerateCenterMesh();

        if (downConnection == true)
        {
            transform.GetChild(0).GetChild(0).localPosition = new Vector3(0, 0, -height);
            transform.GetChild(0).GetChild(0).GetChild(1).localPosition = new Vector3(0, 0, downConnectionHeight);
            Misc.GenerateIntersectionConnection(width, downConnectionWidth, downConnectionResolution * 2, downConnectionHeight, heightOffset, transform.GetChild(0).GetChild(0).GetChild(0), downConnectionMaterial);
        }
        else
        {
            transform.GetChild(0).GetChild(0).GetChild(0).GetComponent<MeshFilter>().sharedMesh = null;
            transform.GetChild(0).GetChild(0).GetChild(0).GetComponent<MeshCollider>().sharedMesh = null;
        }

        if (leftConnection == true)
        {
            transform.GetChild(0).GetChild(1).localPosition = Misc.GetCenter(new Vector3(-width, 0, -height), new Vector3(0, 0, height));
            transform.GetChild(0).GetChild(1).localRotation = Quaternion.FromToRotation(Vector3.right, new Vector3(0, heightOffset, height) - new Vector3(-width, heightOffset, -height));
            transform.GetChild(0).GetChild(1).GetChild(1).localPosition = new Vector3(0, 0, leftConnectionHeight);
            float connectionHeight = Vector3.Distance(new Vector3(-width, heightOffset, -height), new Vector3(0, heightOffset, height)) / 2;
            Misc.GenerateIntersectionConnection(connectionHeight, leftConnectionWidth, leftConnectionResolution * 2, leftConnectionHeight, heightOffset, transform.GetChild(0).GetChild(1).GetChild(0), leftConnectionMaterial);
        }
        else
        {
            transform.GetChild(0).GetChild(1).GetChild(0).GetComponent<MeshFilter>().sharedMesh = null;
            transform.GetChild(0).GetChild(1).GetChild(0).GetComponent<MeshCollider>().sharedMesh = null;
        }

        if (rightConnection == true)
        {
            transform.GetChild(0).GetChild(2).localPosition = Misc.GetCenter(new Vector3(width, 0, -height), new Vector3(0, 0, height));
            transform.GetChild(0).GetChild(2).localRotation = Quaternion.FromToRotation(Vector3.left, new Vector3(0, heightOffset, height) - new Vector3(width, heightOffset, -height));
            transform.GetChild(0).GetChild(2).GetChild(1).localPosition = new Vector3(0, 0, rightConnectionHeight);
            float connectionHeight = Vector3.Distance(new Vector3(-width, heightOffset, -height), new Vector3(0, heightOffset, height)) / 2;
            Misc.GenerateIntersectionConnection(connectionHeight, rightConnectionWidth, rightConnectionResolution * 2, rightConnectionHeight, heightOffset, transform.GetChild(0).GetChild(2).GetChild(0), rightConnectionMaterial);
        }
        else
        {
            transform.GetChild(0).GetChild(2).GetChild(0).GetComponent<MeshFilter>().sharedMesh = null;
            transform.GetChild(0).GetChild(2).GetChild(0).GetComponent<MeshCollider>().sharedMesh = null;
        }
    }

    private void GenerateCenterMesh()
    {
        Vector3[] vertices = new Vector3[3];
        Vector2[] uvs = new Vector2[3];

        vertices[0] = new Vector3(-width, heightOffset, -height);
        vertices[1] = new Vector3(width, heightOffset, -height);
        vertices[2] = new Vector3(0, heightOffset, height);

        uvs[0] = new Vector2(0, 0);
        uvs[1] = new Vector2(1, 0);
        uvs[2] = new Vector2(0.5f, 1);

        Mesh mesh = new Mesh();
        mesh.vertices = vertices;
        mesh.triangles = new int[] { 2, 1, 0 };
        mesh.uv = uvs;

        transform.GetChild(1).GetComponent<MeshFilter>().sharedMesh = mesh;
        Material newMaterial = centerMaterial;
        Texture texture = newMaterial.mainTexture;
        texture.wrapMode = TextureWrapMode.Clamp;
        newMaterial.mainTexture = texture;
        transform.GetChild(1).GetComponent<MeshRenderer>().sharedMaterial = centerMaterial;
        transform.GetChild(1).GetComponent<MeshCollider>().sharedMesh = mesh;
    }

}
