/*
#pragma strict
import System.Collections.Generic;

public static var spawningOn : boolean;

var debugging : boolean;

var particlesSpawned : int = 20000;	// particles spawned to reach this cap for this weather calculation

var sizeVariationMul : float = 1;

var multiSpawn : boolean;
var spawns : int = 1;
var range : float;

//var rate : System.Int32;	// rate is calculated
private var effectiveRate : System.Int32;

private var nodes : GameObject;
private var weather : NodeMesh;
//private var particlefollow : GameObject;
private var PF : particleFollow;

private var x : int;
private var z : int;
private var i : int;

private var pos : Vector3;


// particles of this object's particleSystem
private var particles : ParticleSystem.Particle[];

private var particlesInSecond : float;

private var spawnR : float;
private var ltLow : float;

private var random : System.Random = new System.Random();

// *****************************************************************************

function Start () {
	if (!GameTime.Instance.realTimeEconomy)
	{
		initialize();
	}
	else this.gameObject.SetActive(false);
}


function initialize() {

	// GRAPHICS QUALITY
	particlesSpawned *= 1;	// graphics settings

	nodes = GameObject.Find("nodes");
		weather 	= nodes.gameObject.GetComponent(NodeMesh);
	
	//particlefollow = this.gameObject;
	PF 	= this.gameObject.GetComponent(particleFollow);
	
	particles = new ParticleSystem.Particle[particlesSpawned];

	// particles spawned per second
	particlesInSecond = particlesSpawned / this.particleSystem.startLifetime;

	// lower bound multiplier for startLifeTime
	ltLow = this.particleSystem.startLifetime *0.66;

	// range of spawns
	spawnR  = PF.boxSizeX/2;

	// prewarm particles
	prewarmParticles();

	// debug particle count
	if (debugging) InvokeRepeating("debugCount", 1, 1);	// debug
}

// ***************************************************************************

function Update () {		// OPTIMIZE TO THE MAX!
	
	if (spawningOn)
	{
		if (particleSystem.particleCount == 0) prewarmParticles();

		// straight spawn
		else if (!multiSpawn) {
			// calculate effective rate (particlesSpawned by lifetime in a frame update)
			effectiveRate = Mathf.Min(Mathf.Max(particlesInSecond * Time.deltaTime, 1), particlesSpawned - particleSystem.particleCount); 

			spawnParticles();
		}
		// multispawn
		else {
			// calculate effective rate (particlesSpawned by lifetime in a frame update)
			effectiveRate = Mathf.Min(Mathf.Max(particlesInSecond / (spawns+1) * Time.deltaTime, 1), particlesSpawned * (spawns+1) - particleSystem.particleCount); 

			spawnMultiParticles();
		}

		
		// spawn effectiveRate particles

		// garbage collection - no use
		//System.GC.Collect();
	}
}

// *************************************************************************** // Debug.Log("x:"+x+"  z:"+z);



function spawnParticles() {


	for (i = 0; i < effectiveRate; i++) {
    	
		// spawn pos		x																y											z
		
		pos = Vector3( this.transform.position.x + randomRange(), this.transform.position.y + random.NextDouble(), this.transform.position.z + randomRange() ); 	// pos = Vector3( this.transform.position.x + Random.Range(-spawnR, spawnR), this.transform.position.y + Random.Range(0, 1), this.transform.position.z + Random.Range(-spawnR, spawnR) );


    	// translate position coordinates to weather grid
		x = (pos.x - weather.xUnitHalf) / weather.xUnit;
		z = (pos.z - weather.zUnitHalf) / weather.zUnit;
    	
    	// Emit
    	if (sizeVariationMul == 1) {
			// Emit constant size particle
			this.particleSystem.Emit(
				// pos
				pos, 
				// velocity
				weather.weather[x,z], 
				// size
				this.particleSystem.startSize, 
				//energy (lifetime/speed: faster particles have faster animation and shorter life) 
				Random.Range(ltLow, this.particleSystem.startLifetime) / (weather.weather[x,z].magnitude*0.6), 
				// colour
				this.particleSystem.startColor
				);//, Random.value*360, Random.value*200);
		}
		else {
			// Emit variable -sized particle
			this.particleSystem.Emit(
				// pos
				pos, 
				// velocity
				weather.weather[x,z], 
				// size
				this.particleSystem.startSize * (sizeVariationMul + (sizeVariationMul-1)*Random.value), 
				//energy (lifetime/speed: faster particles have faster animation and shorter life) 
				Random.Range(ltLow, this.particleSystem.startLifetime) / (weather.weather[x,z].magnitude*0.6), 
				// colour
				this.particleSystem.startColor
				);//, Random.value*360, Random.value*200);
		}
		
	}
}

function prewarmParticles() {

	effectiveRate = particlesSpawned - particleSystem.particleCount;


	for (i = 0; i < effectiveRate; i++) {
    	
		// spawn pos		x																y											z
		
		pos = Vector3( this.transform.position.x + randomRange(), this.transform.position.y + random.NextDouble(), this.transform.position.z + randomRange() ); 	// pos = Vector3( this.transform.position.x + Random.Range(-spawnR, spawnR), this.transform.position.y + Random.Range(0, 1), this.transform.position.z + Random.Range(-spawnR, spawnR) );


    	// translate position coordinates to weather grid
		x = (pos.x - weather.xUnitHalf) / weather.xUnit;
		z = (pos.z - weather.zUnitHalf) / weather.zUnit;
    	
    	// Emit
    	if (sizeVariationMul == 1) {
			// Emit constant size particle
			this.particleSystem.Emit(
				// pos
				pos, 
				// velocity
				weather.weather[x,z], 
				// size
				this.particleSystem.startSize, 
				//energy (lifetime/speed: faster particles have faster animation and shorter life) 
				Random.Range(0, this.particleSystem.startLifetime) / (weather.weather[x,z].magnitude*0.6),
				// colour
				this.particleSystem.startColor
				);//, Random.value*360, Random.value*200);
		}
		else {
			// Emit variable -sized particle
			this.particleSystem.Emit(
				// pos
				pos, 
				// velocity
				weather.weather[x,z], 
				// size
				this.particleSystem.startSize * (sizeVariationMul + (sizeVariationMul-1)*Random.value), 
				//energy (lifetime/speed: faster particles have faster animation and shorter life) 
				Random.Range(ltLow, this.particleSystem.startLifetime) / (weather.weather[x,z].magnitude*0.6), 
				// colour
				this.particleSystem.startColor
				);//, Random.value*360, Random.value*200);
		}
		
	}
}


function spawnMultiParticles() {

	for (i = 0; i < effectiveRate; i++) {
    	
		// spawn pos		x																y											z
		
		pos = Vector3( this.transform.position.x + randomRange(), this.transform.position.y + random.NextDouble(), this.transform.position.z + randomRange() ); 	// pos = Vector3( this.transform.position.x + Random.Range(-spawnR, spawnR), this.transform.position.y + Random.Range(0, 1), this.transform.position.z + Random.Range(-spawnR, spawnR) );


    	// translate position coordinates to weather grid
		x = (pos.x - weather.xUnitHalf) / weather.xUnit;
		z = (pos.z - weather.zUnitHalf) / weather.zUnit;
    	
    	// Emit
    	if (sizeVariationMul == 1) {

				// Emit constant size particle
				this.particleSystem.Emit(
					// pos
					pos, 
					// velocity
					weather.weather[x,z], 
					// size
					this.particleSystem.startSize, 
					//energy (lifetime/speed: faster particles have faster animation and shorter life) 
					Random.Range(ltLow, this.particleSystem.startLifetime) / (weather.weather[x,z].magnitude*0.6), 
					// colour
					this.particleSystem.startColor
					);//, Random.value*360, Random.value*200);

			spawnMultis(pos, false);
		}
		else {
			// Emit variable -sized particle
			this.particleSystem.Emit(
				// pos
				pos, 
				// velocity
				weather.weather[x,z], 
				// size
				this.particleSystem.startSize * (sizeVariationMul + (sizeVariationMul-1)*Random.value), 
				//energy (lifetime/speed: faster particles have faster animation and shorter life) 
				Random.Range(ltLow, this.particleSystem.startLifetime) / (weather.weather[x,z].magnitude*0.6), 
				// colour
				this.particleSystem.startColor
				);//, Random.value*360, Random.value*200);

			spawnMultis(pos, true);
		}
		
	}
}

function spawnMultis(position : Vector3, sizeVariation : boolean) {

	if (sizeVariation) 
	{
		for (var i: int = 0; i < spawns; i++) {
			// Emit constant size particle
			this.particleSystem.Emit(
			// pos
			Vector3( pos.x + Random.value*range, pos.y + Random.value, pos.z + Random.value*range ),
			// velocity
			weather.weather[x,z], 
			// size
			this.particleSystem.startSize, 
			//energy (lifetime/speed: faster particles have faster animation and shorter life) 
			Random.Range(ltLow, this.particleSystem.startLifetime) / (weather.weather[x,z].magnitude*0.6), 
			// colour
			this.particleSystem.startColor
			);//, Random.value*360, Random.value*200);	
		}
	}
	else 
	{
		for (var j: int = 0; j < spawns; j++) {
			// Emit constant size particle
			this.particleSystem.Emit(
			// pos
			Vector3( pos.x + Random.value*range, pos.y + Random.value, pos.z + Random.value*range ), 
			// velocity
			weather.weather[x,z], 
			// size
			this.particleSystem.startSize * (sizeVariationMul + (sizeVariationMul-1)*Random.value), 
			//energy (lifetime/speed: faster particles have faster animation and shorter life) 
			Random.Range(ltLow, this.particleSystem.startLifetime) / (weather.weather[x,z].magnitude*0.6), 
			// colour
			this.particleSystem.startColor
			);//, Random.value*360, Random.value*200);	
		}
	}
}

function debugCount() {
	Debug.Log("Baseline particles: "+particleSystem.particleCount );
}

function randomRange() : float {
	return random.NextDouble() * 2.0 * spawnR - spawnR;
}
*/
