using UnityEngine;
using System.Collections;
using System.Globalization;

public class LocationFeatures
{
	//public float minerals = 1.0f;  // obsolete
	//public float biomass = 1.0f;  // obsolete
	// todo: change local resource multipliers
	// should these be accessable by enum types?
	public float food = 1.0f;
	public float minerals = 1.0f;
	public float blackMarket = 1.0f;
	public float innovation = 1.0f;
	public float culture = 1.0f;
	public float industry = 1.0f;
	public float economy = 1.0f;
	public float military = 1.0f;

	
	// todo: change to baseIdeology stats
    public float frontier = 0.2f;
    public float liberalValues = 0.2f;
    public float independent = 0.0f;
    public float religious = 0.2f;
    public float psychic = 0.1f;
    public float psyStability = 0.9f;
	// /\

	public float imperialist = 0;
	public float nationalist = 0;
	public float navigators = 0;
	public float brotherhood = 0;
	public float liberal = 0;
	public float bureaucracy = 0;
	public float technocrat = 0;
	public float transhumanist = 0;
	public float cult = 0;
	public float mercantile = 0;
	public float aristocrat = 0;


    public float population = 10.0f;
	public float techLevel = 0.5f;
    public float infrastructure = 0.5f;
    public float orbitalInfra = 0.2f;
    public int legalLevel = 1;

    public LocationFeatures()
    {
    }
    public LocationFeatures(string data)
    {
        string[] dataChunk = data.Split(',');

        //minerals = float.Parse(dataChunk[0], CultureInfo.InvariantCulture.NumberFormat);
        //biomass = float.Parse(dataChunk[1], CultureInfo.InvariantCulture.NumberFormat);
        
		/** todo - add
		* food = float.Parse(dataChunk[0], CultureInfo.InvariantCulture.NumberFormat);
		* minerals = float.Parse(dataChunk[0], CultureInfo.InvariantCulture.NumberFormat);
		* blackMarket = float.Parse(dataChunk[0], CultureInfo.InvariantCulture.NumberFormat);
		* innovation = float.Parse(dataChunk[0], CultureInfo.InvariantCulture.NumberFormat);
		* culture = float.Parse(dataChunk[0], CultureInfo.InvariantCulture.NumberFormat);
		* industry = float.Parse(dataChunk[0], CultureInfo.InvariantCulture.NumberFormat);
		* economy = float.Parse(dataChunk[0], CultureInfo.InvariantCulture.NumberFormat);
		* military = float.Parse(dataChunk[0], CultureInfo.InvariantCulture.NumberFormat);
		*/

		// todo: change to baseIdeology stats
		frontier = float.Parse(dataChunk[2], CultureInfo.InvariantCulture.NumberFormat);
        liberalValues = float.Parse(dataChunk[3], CultureInfo.InvariantCulture.NumberFormat);
        independent = float.Parse(dataChunk[4], CultureInfo.InvariantCulture.NumberFormat);
        religious = float.Parse(dataChunk[5], CultureInfo.InvariantCulture.NumberFormat); 
        psychic = float.Parse(dataChunk[7], CultureInfo.InvariantCulture.NumberFormat);
        psyStability = float.Parse(dataChunk[8], CultureInfo.InvariantCulture.NumberFormat);
		// /\
		/*
		imperialist 	= float.Parse(dataChunk[0], CultureInfo.InvariantCulture.NumberFormat);
		nationalist 	= float.Parse(dataChunk[0], CultureInfo.InvariantCulture.NumberFormat);
		navigators 		= float.Parse(dataChunk[0], CultureInfo.InvariantCulture.NumberFormat);
		brotherhood 	= float.Parse(dataChunk[0], CultureInfo.InvariantCulture.NumberFormat);
		liberal 		= float.Parse(dataChunk[0], CultureInfo.InvariantCulture.NumberFormat);
		bureaucracy 	= float.Parse(dataChunk[0], CultureInfo.InvariantCulture.NumberFormat);
		technocrat 		= float.Parse(dataChunk[0], CultureInfo.InvariantCulture.NumberFormat);
		transhumanist 	= float.Parse(dataChunk[0], CultureInfo.InvariantCulture.NumberFormat);
		cult 			= float.Parse(dataChunk[0], CultureInfo.InvariantCulture.NumberFormat);
		mercantile 		= float.Parse(dataChunk[0], CultureInfo.InvariantCulture.NumberFormat);
		aristocrat 		= float.Parse(dataChunk[0], CultureInfo.InvariantCulture.NumberFormat);
		*/

        population = float.Parse(dataChunk[9], CultureInfo.InvariantCulture.NumberFormat);
		techLevel = float.Parse(dataChunk[6], CultureInfo.InvariantCulture.NumberFormat);
		infrastructure = float.Parse(dataChunk[10], CultureInfo.InvariantCulture.NumberFormat);
        orbitalInfra = float.Parse(dataChunk[11], CultureInfo.InvariantCulture.NumberFormat);
        legalLevel = int.Parse(dataChunk[12], CultureInfo.InvariantCulture.NumberFormat);
    }

}
