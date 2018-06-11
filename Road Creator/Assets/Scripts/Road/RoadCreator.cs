﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoadCreator : MonoBehaviour {

    public List<GameObject> points = new List<GameObject>();
    public Material defaultRoadMaterial;
    public Material defaultShoulderMaterial;
    public RoadSegment currentSegment;
    public float heightOffset = 0.02f;
    public int smoothnessAmount = 3;

    public GlobalSettings globalSettings;

}
