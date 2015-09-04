using UnityEngine;
using UnityEngine.UI;
using System;
using System.Collections;
using System.Collections.Generic;

public class MarketCommodity : MonoBehaviour
{
    public Data.Resource.SubType trackedCommodity;
    public LocationSceneState state;

    public Text commodityName;
    public Text price;
    public Text cargoAmount;
    public Text storeAmount;

    public Image hilight;
    
    private Text playerCredits;
    private Text playerCargo;
    private Image lineGraph;
    private Image lineGraphHor;
    //private string trackedLocation = "not defined";


    // Use this for initialization
    void Start()
    {
        hilight.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
    }

    internal void trackLocation(LocationSceneState state, Data.Resource.SubType commodity)
    {
        this.state = state;
        //trackedLocation = state.trackedLocation;
        trackedCommodity = commodity;
        updateCommodityInfo();
    }

    private void updateCommodityInfo()
    {
        ///@todo set readable names
        commodityName.text = Enum.GetName(typeof(Data.Resource.SubType), trackedCommodity);
        price.text = "0";
        storeAmount.text = state.tradeList.commodities[trackedCommodity].ToString();
        cargoAmount.text = "0"; //Root.game.player.cargo.commodities[].ToString();
    }

    public void msgMouseEnter()
    {
        hilight.enabled = true;
    }

    public void msgMouseExit()
    {
        hilight.enabled = false;
    }

    public void buy()
    {
        //Dictionary<string, int> store = Game.universe.locations[trackedLocation].stockpile.tradable;
        //Dictionary<string, int> player = Game.universe.player.cargo.commodities;

        //if (store[trackedCommodity] > 0 &&
        //    Game.universe.player.cargo.credits >= Game.universe.locations[trackedLocation].getCommodityPrice(trackedCommodity) &&
        //    Game.universe.player.cargo.getUsedCargoSpace() < Game.universe.player.cargo.maxCargoSpace)
        //{
        //    --store[trackedCommodity];
        //    ++player[trackedCommodity];
        //    Game.universe.player.cargo.credits -= Game.universe.locations[trackedLocation].getCommodityPrice(trackedCommodity);
        //}

        //updateCommodityInfo(trackedLocation, trackedCommodity);
    }

    public void sell()
    {
        //Dictionary<string, int> store = Game.universe.locations[trackedLocation].stockpile.tradable;
        //Dictionary<string, int> player = Game.universe.player.cargo.commodities;

        //if (Game.universe.player.cargo.commodities[trackedCommodity] > 0)
        //{
        //    ++store[trackedCommodity];
        //    --player[trackedCommodity];
        //    Game.universe.player.cargo.credits += Game.universe.locations[trackedLocation].getCommodityPrice(trackedCommodity);
        //}

        //updateCommodityInfo(trackedLocation, trackedCommodity);
    }

    string getCommodityName(Data.Resource.SubType resourceType)
    {
        switch (resourceType)
        {
        case Data.Resource.SubType.FoodT1:
            return "Grain"; // Celphos grain, Fusarium venenatum, soy and other crops that can be stored for transportation.
        case Data.Resource.SubType.FoodT2:
            return "Canned Food"; // Airtight preserving of processed and fresh food ingredients: vegetables, meat, seafood and dairy.
        case Data.Resource.SubType.FoodT3:
            return "Frozen Food"; // More advanced methods of transporting food while preserving all its nutrients.
        case Data.Resource.SubType.FoodT4:
            return "Nutrigrafts"; // Food supplements, balanced meals and genetically modified food made to meet all nutritional requirements.


        case Data.Resource.SubType.MineralT1:
            return "Raw Materials"; // Common materials in minimally processed or unprocessed states: metal ores, raw minerals and extracted chemicals.
        case Data.Resource.SubType.MineralT2:
            return "Processed Materials"; // Processed metals, alloys and chemicals for industrial manufacturing: metals, alloys, plastics and refined chemicals.
        case Data.Resource.SubType.MineralT3:
            return "Advanced Materials"; //  Materials for Hi-tech industrial needs: liquid crystals, semiconductors, superconductors, optics, lasers, sensors, mesoporous materials, shape memory alloys, light-emitting materials, magnetic materials, thin films, and colloids.
        case Data.Resource.SubType.MineralT4:
            return "Nanomaterials"; // Microfabricated materials with structure matrix at the nanoscale are highly sought after due to their extraordinary properties.

        case Data.Resource.SubType.IndustryT1:
            return "Machinery"; // Machinery and tools for upkeeping and building basic low-scale infrastructure and manufacturing.
        case Data.Resource.SubType.IndustryT2:
            return "Industrial Assembly"; // Assembly lines and components for the needs of scalable heavy industry.
        case Data.Resource.SubType.IndustryT3:
            return "Advanced Assembly"; // Advanced manufacturing components for electronics and complex manufacturing industries.
        case Data.Resource.SubType.IndustryT4:
            return "Autofabs"; // Highly automated factory modules capable of autonomous deployment and sophisticated manufacturing.

        case Data.Resource.SubType.InnovationT1:
            return "Education Assets"; // Teachers, training programs, medical supplies, blueprints and other foundational colony assets.
        case Data.Resource.SubType.InnovationT2:
            return "Technical Assets"; // Medical instruments, comm systems and specialists like engineers doctors and lawmakers.
        case Data.Resource.SubType.InnovationT3:
            return "Advanced Assets"; // Computer systems, scientists,  satellites and other information infrastructure.
        case Data.Resource.SubType.InnovationT4:
            return "Hi-tech Assets"; // Robotics, neural cores, med vats, sophisticated software and other cutting edge technology.

        case Data.Resource.SubType.EconomyT1:
            return "Seed Economy"; // Economic instruments, seed investments, agents and prospectors.
        case Data.Resource.SubType.EconomyT2:
            return "Corporate Economy"; // Corporation assets, investors and private funding.
        case Data.Resource.SubType.EconomyT3:
            return "Banking Economy"; // Banking instruments, economists and securities.
        case Data.Resource.SubType.EconomyT4:
            return "Planetary Economy"; // Planetary assets, major investments and treasuries.

        case Data.Resource.SubType.CultureT1:
            return "Consumer Necessities"; // Clothing, furniture and other personal effects.
        case Data.Resource.SubType.CultureT2:
            return "Consumer Goods"; // Appliances, services and entertainment products.
        case Data.Resource.SubType.CultureT3:
            return "Consumer Electronics"; // Personal computers, consumer software, entertainment electronics and digital entertainment.
        case Data.Resource.SubType.CultureT4:
            return "Consumer Luxuries"; // Cybernetics, VR and Virtual Personal Assistants.

        case Data.Resource.SubType.MilitaryT1:
            return "Ordnance"; // Personal assault weapons and infantry armor.
        case Data.Resource.SubType.MilitaryT2:
            return "Heavy Weapons"; // Support weapons to take out larger targets.
        case Data.Resource.SubType.MilitaryT3:
            return "Weapon Systems"; // Rockets, las cannons and combat vehicles.
        case Data.Resource.SubType.MilitaryT4:
            return "Capital Weapons"; // Ship-mounted weapon systems used in space or fracture.

        case Data.Resource.SubType.BlackMarketT1:
            return "Grain"; // 
        case Data.Resource.SubType.BlackMarketT2:
            return "Grain"; // 
        case Data.Resource.SubType.BlackMarketT3:
            return "Grain"; // 
        case Data.Resource.SubType.BlackMarketT4:
            return "Grain"; // 

        case Data.Resource.SubType.Unknown:
            return "unknown";

        default:
            return "default";
        }
    }
}
