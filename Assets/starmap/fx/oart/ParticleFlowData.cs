﻿using UnityEngine;
using System.Collections;

public static class ParticleFlowData
{
    public static Texture2D flowTexture { private set; get; }
    private static Vector2[,] flowMap;

    // Use this for initialization
    public static Vector2[,] getFlowMap()
    {
        if (flowMap == null) buildFlowMap();
        return flowMap;
    }

    public static void buildFlowMap()
    {
        if (flowMap == null)
        {
            flowTexture = Resources.Load<Texture2D>("starmap/weather_flow_2x_a");

            if (flowTexture == null) Debug.LogError("texture map not loaded");
            else
            {
                flowMap = new Vector2[flowTexture.width, flowTexture.height];

                Debug.Log("building flowMap");
                for (int i = 0; i < flowTexture.width; ++i)
                {
                    for (int j = 0; j < flowTexture.height; ++j)
                    {
                        flowMap[i, j] = new Vector2(flowTexture.GetPixel(i, flowTexture.height - j).r - 0.5f, flowTexture.GetPixel(i, flowTexture.height - j).g * (1.0f) - 0.5f);
                        flowMap[i, j] *= 1.8f;
                    }
                }
            }
        }
	}

}
