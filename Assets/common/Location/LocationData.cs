using UnityEngine;
using System.Collections;
using System.Globalization;

public class LocationFeatures
{
    public float minerals = 0.2f;
    public float biomass = 0.2f;
    public float frontier = 0.2f;
    public float liberalValues = 0.2f;
    public float independent = 0.0f;
    public float religious = 0.2f;
    public float techLevel = 0.5f;
    public float psychic = 0.1f;
    public float psyStability = 0.9f;
    public float population = 10.0f;
    public float infrastructure = 1.0f;
    public float orbitalInfra = 0.2f;
    public float legalLevel = 1.0f;

    public LocationFeatures()
    {
    }
    public LocationFeatures(string data)
    {
        string[] dataChunk = data.Split(',');
        minerals = float.Parse(dataChunk[0], CultureInfo.InvariantCulture.NumberFormat);
        biomass = float.Parse(dataChunk[1], CultureInfo.InvariantCulture.NumberFormat);
        frontier = float.Parse(dataChunk[2], CultureInfo.InvariantCulture.NumberFormat);
        liberalValues = float.Parse(dataChunk[3], CultureInfo.InvariantCulture.NumberFormat);
        independent = float.Parse(dataChunk[4], CultureInfo.InvariantCulture.NumberFormat);
        religious = float.Parse(dataChunk[5], CultureInfo.InvariantCulture.NumberFormat);
        techLevel = float.Parse(dataChunk[6], CultureInfo.InvariantCulture.NumberFormat);
        psychic = float.Parse(dataChunk[7], CultureInfo.InvariantCulture.NumberFormat);
        psyStability = float.Parse(dataChunk[8], CultureInfo.InvariantCulture.NumberFormat);
        population = float.Parse(dataChunk[9], CultureInfo.InvariantCulture.NumberFormat);
        infrastructure = float.Parse(dataChunk[10], CultureInfo.InvariantCulture.NumberFormat);
        orbitalInfra = float.Parse(dataChunk[11], CultureInfo.InvariantCulture.NumberFormat);
        legalLevel = float.Parse(dataChunk[12], CultureInfo.InvariantCulture.NumberFormat);
    }
}
