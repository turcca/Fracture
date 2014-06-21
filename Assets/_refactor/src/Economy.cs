using System;
using System.Collections.Generic;
using System.Linq;

public class CommodityInfo
{
    public string name;
    public string description;
    public string category;
    public float legality;
    public float shelfLife;
    public float value;
    public float marketShareBase;

    public float pTechLevel;
    public float pOrbital;
    public float pBiomass;
    public float pMinerals;

    public float pFoodOutputBonus;
    public float pIndustryOutputBonus;
    public float pInnovationBonus;
    public float pMilitaryBonus;

    public float bFoodOutputBonus;
    public float bIndustryOutputBonus;
    public float bAffluenceBonus;
    public float bMilitaryBonus;

    public float shortagedPriceMult = 1.2f;
}

public class CommodityCategory
{
    public string name;
    public float marketShareBase;

    public float pFoodOutputBonus;
    public float pIndustryOutputBonus;
    public float pMilitaryBonus;

    public float bFoodOutputBonus;
    public float bIndustryOutputBonus;
    public float bMilitaryBonus;
}

static public class Economy
{
    static public Dictionary<string, CommodityInfo> commodityInfo = new Dictionary<string, CommodityInfo>();
    static public Dictionary<string, CommodityCategory> categoryInfo;

    static public string[] getCommodityNames()
    {
        return new string[]
        {
            "grain",
            "grainCelphos",
            "canned",
            "fresh",
            "extravagant",
            "common",
            "scarce",
            "rare",
            "radioactive",
            "limited",
            "bulk",
            "subtle",
            "complex",
            "reactive",
            "restricted",
            "supplies",
            "instruments",
            "advanced",
            "consumer",
            "industrial",
            "military",
            "highTech",
            "cybernetic",
            "artifacts",
            "ordnance",
            "heavy",
            "starship",
            "xeno",
            "drinks",
            "tobacco",
            "drugs",
            "psychic"
        };
    }

    static public string[] getCommodityCategoryNames()
    {
        return new string[]
        {
            "food",
            "minerals",
            "chemicals",
            "medical",
            "products",
            "weapons",
            "narcotic"
        };
    }

    static public Dictionary<string, CommodityInfo> getCommodityInfoOnCategory(string filterCategory)
    {
        return commodityInfo.Where(c => c.Value.category == filterCategory).ToDictionary(c => c.Key, c => c.Value);
    }

    static Economy()
    {
        foreach (string key in getCommodityNames())
        {
            commodityInfo[key] = new CommodityInfo();
        }

        //categoryInfo["food"].name = "food"; categoryInfo["food"].marketShareBase = 40; categoryInfo["food"].pFoodOutputBonus = 2.5f; categoryInfo["food"].pIndustryOutputBonus = 1; categoryInfo["food"].pMilitaryBonus = 1; categoryInfo["food"].bFoodOutputBonus = -2.5f; categoryInfo["food"].bIndustryOutputBonus = 1; categoryInfo["food"].bMilitaryBonus = 1;
        //categoryInfo["minerals"].name = "minerals"; categoryInfo["minerals"].marketShareBase = 20; categoryInfo["minerals"].pFoodOutputBonus = 1; categoryInfo["minerals"].pIndustryOutputBonus = 1.25f; categoryInfo["minerals"].pMilitaryBonus = 1; categoryInfo["minerals"].bFoodOutputBonus = 1; categoryInfo["minerals"].bIndustryOutputBonus = 1.5f; categoryInfo["minerals"].bMilitaryBonus = 1;
        //categoryInfo["chemicals"].name = "chemicals"; categoryInfo["chemicals"].marketShareBase = 10; categoryInfo["chemicals"].pFoodOutputBonus = 1; categoryInfo["chemicals"].pIndustryOutputBonus = 1.5f; categoryInfo["chemicals"].pMilitaryBonus = 1; categoryInfo["chemicals"].bFoodOutputBonus = 1; categoryInfo["chemicals"].bIndustryOutputBonus = 1; categoryInfo["chemicals"].bMilitaryBonus = 1;
        //categoryInfo["medical"].name = "medical"; categoryInfo["medical"].marketShareBase = 6; categoryInfo["medical"].pFoodOutputBonus = 1; categoryInfo["medical"].pIndustryOutputBonus = 1; categoryInfo["medical"].pMilitaryBonus = 1; categoryInfo["medical"].bFoodOutputBonus = 1; categoryInfo["medical"].bIndustryOutputBonus = 1; categoryInfo["medical"].bMilitaryBonus = 1;
        //categoryInfo["products"].name = "products"; categoryInfo["products"].marketShareBase = 17; categoryInfo["products"].pFoodOutputBonus = 1; categoryInfo["products"].pIndustryOutputBonus = 2; categoryInfo["products"].pMilitaryBonus = -2; categoryInfo["products"].bFoodOutputBonus = 1; categoryInfo["products"].bIndustryOutputBonus = -2; categoryInfo["products"].bMilitaryBonus = -1.5f;
        //categoryInfo["weapons"].name = "weapons"; categoryInfo["weapons"].marketShareBase = 4; categoryInfo["weapons"].pFoodOutputBonus = 1; categoryInfo["weapons"].pIndustryOutputBonus = 1.5f; categoryInfo["weapons"].pMilitaryBonus = 2; categoryInfo["weapons"].bFoodOutputBonus = 1; categoryInfo["weapons"].bIndustryOutputBonus = -1.5f; categoryInfo["weapons"].bMilitaryBonus = 1.5f;
        //categoryInfo["narcotic"].name = "narcotic"; categoryInfo["narcotic"].marketShareBase = 3; categoryInfo["narcotic"].pFoodOutputBonus = 1; categoryInfo["narcotic"].pIndustryOutputBonus = 1; categoryInfo["narcotic"].pMilitaryBonus = 1; categoryInfo["narcotic"].bFoodOutputBonus = 1; categoryInfo["narcotic"].bIndustryOutputBonus = 1; categoryInfo["narcotic"].bMilitaryBonus = 1;

        commodityInfo["grain"].name = "grain"; commodityInfo["grain"].category = "food"; commodityInfo["grain"].legality = 0; commodityInfo["grain"].shelfLife = 365; commodityInfo["grain"].value = 1; commodityInfo["grain"].marketShareBase = 45; commodityInfo["grain"].pTechLevel = 0.0f; commodityInfo["grain"].pOrbital = 0; commodityInfo["grain"].pBiomass = 0; commodityInfo["grain"].pMinerals = 0; commodityInfo["grain"].pFoodOutputBonus = 1; commodityInfo["grain"].pIndustryOutputBonus = 1; commodityInfo["grain"].pInnovationBonus = 1; commodityInfo["grain"].pMilitaryBonus = 1; commodityInfo["grain"].bFoodOutputBonus = 1; commodityInfo["grain"].bIndustryOutputBonus = 1; commodityInfo["grain"].bAffluenceBonus = 1; commodityInfo["grain"].bMilitaryBonus = 1;
        commodityInfo["grainCelphos"].name = "celphos"; commodityInfo["grainCelphos"].category = "food"; commodityInfo["grainCelphos"].legality = 0; commodityInfo["grainCelphos"].shelfLife = 2000; commodityInfo["grainCelphos"].value = 1.3f; commodityInfo["grainCelphos"].marketShareBase = 35; commodityInfo["grainCelphos"].pTechLevel = 0.4f; commodityInfo["grainCelphos"].pOrbital = 0; commodityInfo["grainCelphos"].pBiomass = 0; commodityInfo["grainCelphos"].pMinerals = 0; commodityInfo["grainCelphos"].pFoodOutputBonus = 1; commodityInfo["grainCelphos"].pIndustryOutputBonus = 1; commodityInfo["grainCelphos"].pInnovationBonus = 1; commodityInfo["grainCelphos"].pMilitaryBonus = 1; commodityInfo["grainCelphos"].bFoodOutputBonus = 1; commodityInfo["grainCelphos"].bIndustryOutputBonus = 1; commodityInfo["grainCelphos"].bAffluenceBonus = 1; commodityInfo["grainCelphos"].bMilitaryBonus = 1;
        commodityInfo["canned"].name = "canned"; commodityInfo["canned"].category = "food"; commodityInfo["canned"].legality = 0; commodityInfo["canned"].shelfLife = 10000; commodityInfo["canned"].value = 2; commodityInfo["canned"].marketShareBase = 14; commodityInfo["canned"].pTechLevel = 0.5f; commodityInfo["canned"].pOrbital = 0; commodityInfo["canned"].pBiomass = 0; commodityInfo["canned"].pMinerals = 0; commodityInfo["canned"].pFoodOutputBonus = 1; commodityInfo["canned"].pIndustryOutputBonus = 1.5f; commodityInfo["canned"].pInnovationBonus = 1; commodityInfo["canned"].pMilitaryBonus = 1.5f; commodityInfo["canned"].bFoodOutputBonus = 1; commodityInfo["canned"].bIndustryOutputBonus = -1.5f; commodityInfo["canned"].bAffluenceBonus = 1; commodityInfo["canned"].bMilitaryBonus = 1.5f;
        commodityInfo["fresh"].name = "fresh"; commodityInfo["fresh"].category = "food"; commodityInfo["fresh"].legality = 0; commodityInfo["fresh"].shelfLife = 30; commodityInfo["fresh"].value = 5; commodityInfo["fresh"].marketShareBase = 5; commodityInfo["fresh"].pTechLevel = 0.0f; commodityInfo["fresh"].pOrbital = 0.2f; commodityInfo["fresh"].pBiomass = 0; commodityInfo["fresh"].pMinerals = 0; commodityInfo["fresh"].pFoodOutputBonus = 1; commodityInfo["fresh"].pIndustryOutputBonus = 1; commodityInfo["fresh"].pInnovationBonus = 1; commodityInfo["fresh"].pMilitaryBonus = 1; commodityInfo["fresh"].bFoodOutputBonus = 1; commodityInfo["fresh"].bIndustryOutputBonus = 1; commodityInfo["fresh"].bAffluenceBonus = 2; commodityInfo["fresh"].bMilitaryBonus = 1;
        commodityInfo["extravagant"].name = "extravagant"; commodityInfo["extravagant"].category = "food"; commodityInfo["extravagant"].legality = 1; commodityInfo["extravagant"].shelfLife = 20; commodityInfo["extravagant"].value = 20; commodityInfo["extravagant"].marketShareBase = 1; commodityInfo["extravagant"].pTechLevel = 0.0f; commodityInfo["extravagant"].pOrbital = 0.3f; commodityInfo["extravagant"].pBiomass = 0; commodityInfo["extravagant"].pMinerals = 0; commodityInfo["extravagant"].pFoodOutputBonus = 1; commodityInfo["extravagant"].pIndustryOutputBonus = 1; commodityInfo["extravagant"].pInnovationBonus = 1; commodityInfo["extravagant"].pMilitaryBonus = 1; commodityInfo["extravagant"].bFoodOutputBonus = 1; commodityInfo["extravagant"].bIndustryOutputBonus = 1; commodityInfo["extravagant"].bAffluenceBonus = 3; commodityInfo["extravagant"].bMilitaryBonus = 1;
        commodityInfo["common"].name = "common"; commodityInfo["common"].category = "minerals"; commodityInfo["common"].legality = 0; commodityInfo["common"].shelfLife = 99999; commodityInfo["common"].value = 1; commodityInfo["common"].marketShareBase = 60; commodityInfo["common"].pTechLevel = 0.0f; commodityInfo["common"].pOrbital = 0; commodityInfo["common"].pBiomass = 0; commodityInfo["common"].pMinerals = 0; commodityInfo["common"].pFoodOutputBonus = 1; commodityInfo["common"].pIndustryOutputBonus = 1; commodityInfo["common"].pInnovationBonus = 1; commodityInfo["common"].pMilitaryBonus = 1; commodityInfo["common"].bFoodOutputBonus = 1; commodityInfo["common"].bIndustryOutputBonus = 1; commodityInfo["common"].bAffluenceBonus = 1; commodityInfo["common"].bMilitaryBonus = 1;
        commodityInfo["scarce"].name = "scarce"; commodityInfo["scarce"].category = "minerals"; commodityInfo["scarce"].legality = 0; commodityInfo["scarce"].shelfLife = 99999; commodityInfo["scarce"].value = 3; commodityInfo["scarce"].marketShareBase = 20; commodityInfo["scarce"].pTechLevel = 0.0f; commodityInfo["scarce"].pOrbital = 0; commodityInfo["scarce"].pBiomass = 0; commodityInfo["scarce"].pMinerals = 0.3f; commodityInfo["scarce"].pFoodOutputBonus = 1; commodityInfo["scarce"].pIndustryOutputBonus = 1; commodityInfo["scarce"].pInnovationBonus = 1; commodityInfo["scarce"].pMilitaryBonus = 1; commodityInfo["scarce"].bFoodOutputBonus = 1; commodityInfo["scarce"].bIndustryOutputBonus = 1; commodityInfo["scarce"].bAffluenceBonus = 1; commodityInfo["scarce"].bMilitaryBonus = 1;
        commodityInfo["rare"].name = "rare"; commodityInfo["rare"].category = "minerals"; commodityInfo["rare"].legality = 0; commodityInfo["rare"].shelfLife = 99999; commodityInfo["rare"].value = 100; commodityInfo["rare"].marketShareBase = 2; commodityInfo["rare"].pTechLevel = 0.0f; commodityInfo["rare"].pOrbital = 0; commodityInfo["rare"].pBiomass = 0; commodityInfo["rare"].pMinerals = 0.5f; commodityInfo["rare"].pFoodOutputBonus = 1; commodityInfo["rare"].pIndustryOutputBonus = 1; commodityInfo["rare"].pInnovationBonus = 1; commodityInfo["rare"].pMilitaryBonus = 1; commodityInfo["rare"].bFoodOutputBonus = 1; commodityInfo["rare"].bIndustryOutputBonus = 1.5f; commodityInfo["rare"].bAffluenceBonus = 1; commodityInfo["rare"].bMilitaryBonus = 1;
        commodityInfo["radioactive"].name = "radioactive"; commodityInfo["radioactive"].category = "minerals"; commodityInfo["radioactive"].legality = 1; commodityInfo["radioactive"].shelfLife = 99999; commodityInfo["radioactive"].value = 8; commodityInfo["radioactive"].marketShareBase = 10; commodityInfo["radioactive"].pTechLevel = 0.5f; commodityInfo["radioactive"].pOrbital = 0.35f; commodityInfo["radioactive"].pBiomass = 0; commodityInfo["radioactive"].pMinerals = 0.25f; commodityInfo["radioactive"].pFoodOutputBonus = 1; commodityInfo["radioactive"].pIndustryOutputBonus = 1; commodityInfo["radioactive"].pInnovationBonus = 1; commodityInfo["radioactive"].pMilitaryBonus = 1; commodityInfo["radioactive"].bFoodOutputBonus = 1; commodityInfo["radioactive"].bIndustryOutputBonus = 1.5f; commodityInfo["radioactive"].bAffluenceBonus = 1; commodityInfo["radioactive"].bMilitaryBonus = 1;
        commodityInfo["limited"].name = "limited"; commodityInfo["limited"].category = "minerals"; commodityInfo["limited"].legality = 2; commodityInfo["limited"].shelfLife = 99999; commodityInfo["limited"].value = 20; commodityInfo["limited"].marketShareBase = 8; commodityInfo["limited"].pTechLevel = 0.0f; commodityInfo["limited"].pOrbital = 0; commodityInfo["limited"].pBiomass = 0; commodityInfo["limited"].pMinerals = 0.4f; commodityInfo["limited"].pFoodOutputBonus = 1; commodityInfo["limited"].pIndustryOutputBonus = 1; commodityInfo["limited"].pInnovationBonus = 1; commodityInfo["limited"].pMilitaryBonus = 1; commodityInfo["limited"].bFoodOutputBonus = 1; commodityInfo["limited"].bIndustryOutputBonus = 1.5f; commodityInfo["limited"].bAffluenceBonus = 1; commodityInfo["limited"].bMilitaryBonus = 1;
        commodityInfo["bulk"].name = "bulk"; commodityInfo["bulk"].category = "chemicals"; commodityInfo["bulk"].legality = 0; commodityInfo["bulk"].shelfLife = 365; commodityInfo["bulk"].value = 1; commodityInfo["bulk"].marketShareBase = 40; commodityInfo["bulk"].pTechLevel = 0.4f; commodityInfo["bulk"].pOrbital = 0; commodityInfo["bulk"].pBiomass = 0; commodityInfo["bulk"].pMinerals = 0; commodityInfo["bulk"].pFoodOutputBonus = 1; commodityInfo["bulk"].pIndustryOutputBonus = 1; commodityInfo["bulk"].pInnovationBonus = 1; commodityInfo["bulk"].pMilitaryBonus = 1; commodityInfo["bulk"].bFoodOutputBonus = 1; commodityInfo["bulk"].bIndustryOutputBonus = 2; commodityInfo["bulk"].bAffluenceBonus = 1; commodityInfo["bulk"].bMilitaryBonus = 1;
        commodityInfo["subtle"].name = "subtle"; commodityInfo["subtle"].category = "chemicals"; commodityInfo["subtle"].legality = 0; commodityInfo["subtle"].shelfLife = 30; commodityInfo["subtle"].value = 3; commodityInfo["subtle"].marketShareBase = 25; commodityInfo["subtle"].pTechLevel = 0.45f; commodityInfo["subtle"].pOrbital = 0.3f; commodityInfo["subtle"].pBiomass = 0; commodityInfo["subtle"].pMinerals = 0; commodityInfo["subtle"].pFoodOutputBonus = 1; commodityInfo["subtle"].pIndustryOutputBonus = 1; commodityInfo["subtle"].pInnovationBonus = 1; commodityInfo["subtle"].pMilitaryBonus = 1; commodityInfo["subtle"].bFoodOutputBonus = 1; commodityInfo["subtle"].bIndustryOutputBonus = 1; commodityInfo["subtle"].bAffluenceBonus = 1; commodityInfo["subtle"].bMilitaryBonus = 1;
        commodityInfo["complex"].name = "complex"; commodityInfo["complex"].category = "chemicals"; commodityInfo["complex"].legality = 0; commodityInfo["complex"].shelfLife = 120; commodityInfo["complex"].value = 12; commodityInfo["complex"].marketShareBase = 10; commodityInfo["complex"].pTechLevel = 0.55f; commodityInfo["complex"].pOrbital = 0.3f; commodityInfo["complex"].pBiomass = 0; commodityInfo["complex"].pMinerals = 0; commodityInfo["complex"].pFoodOutputBonus = 1; commodityInfo["complex"].pIndustryOutputBonus = 1; commodityInfo["complex"].pInnovationBonus = 2; commodityInfo["complex"].pMilitaryBonus = 1; commodityInfo["complex"].bFoodOutputBonus = 1; commodityInfo["complex"].bIndustryOutputBonus = 1; commodityInfo["complex"].bAffluenceBonus = 1; commodityInfo["complex"].bMilitaryBonus = 1;
        commodityInfo["reactive"].name = "reactive"; commodityInfo["reactive"].category = "chemicals"; commodityInfo["reactive"].legality = 2; commodityInfo["reactive"].shelfLife = 99999; commodityInfo["reactive"].value = 8; commodityInfo["reactive"].marketShareBase = 15; commodityInfo["reactive"].pTechLevel = 0.5f; commodityInfo["reactive"].pOrbital = 0.35f; commodityInfo["reactive"].pBiomass = 0; commodityInfo["reactive"].pMinerals = 0; commodityInfo["reactive"].pFoodOutputBonus = 1; commodityInfo["reactive"].pIndustryOutputBonus = 1; commodityInfo["reactive"].pInnovationBonus = 1; commodityInfo["reactive"].pMilitaryBonus = 1.5f; commodityInfo["reactive"].bFoodOutputBonus = 1; commodityInfo["reactive"].bIndustryOutputBonus = 1; commodityInfo["reactive"].bAffluenceBonus = 1; commodityInfo["reactive"].bMilitaryBonus = 2;
        commodityInfo["restricted"].name = "restricted"; commodityInfo["restricted"].category = "chemicals"; commodityInfo["restricted"].legality = 3; commodityInfo["restricted"].shelfLife = 99999; commodityInfo["restricted"].value = 20; commodityInfo["restricted"].marketShareBase = 10; commodityInfo["restricted"].pTechLevel = 0.5f; commodityInfo["restricted"].pOrbital = 0; commodityInfo["restricted"].pBiomass = 0; commodityInfo["restricted"].pMinerals = 0; commodityInfo["restricted"].pFoodOutputBonus = 1; commodityInfo["restricted"].pIndustryOutputBonus = 1; commodityInfo["restricted"].pInnovationBonus = 1; commodityInfo["restricted"].pMilitaryBonus = 1; commodityInfo["restricted"].bFoodOutputBonus = 1; commodityInfo["restricted"].bIndustryOutputBonus = 2; commodityInfo["restricted"].bAffluenceBonus = 1; commodityInfo["restricted"].bMilitaryBonus = 1;
        commodityInfo["supplies"].name = "supplies"; commodityInfo["supplies"].category = "medical"; commodityInfo["supplies"].legality = 0; commodityInfo["supplies"].shelfLife = 365; commodityInfo["supplies"].value = 3; commodityInfo["supplies"].marketShareBase = 70; commodityInfo["supplies"].pTechLevel = 0.45f; commodityInfo["supplies"].pOrbital = 0; commodityInfo["supplies"].pBiomass = 0; commodityInfo["supplies"].pMinerals = 0; commodityInfo["supplies"].pFoodOutputBonus = 2; commodityInfo["supplies"].pIndustryOutputBonus = 1; commodityInfo["supplies"].pInnovationBonus = 1; commodityInfo["supplies"].pMilitaryBonus = 1; commodityInfo["supplies"].bFoodOutputBonus = -2; commodityInfo["supplies"].bIndustryOutputBonus = 1; commodityInfo["supplies"].bAffluenceBonus = 1; commodityInfo["supplies"].bMilitaryBonus = 1;
        commodityInfo["instruments"].name = "instruments"; commodityInfo["instruments"].category = "medical"; commodityInfo["instruments"].legality = 0; commodityInfo["instruments"].shelfLife = 99999; commodityInfo["instruments"].value = 10; commodityInfo["instruments"].marketShareBase = 20; commodityInfo["instruments"].pTechLevel = 0.5f; commodityInfo["instruments"].pOrbital = 0; commodityInfo["instruments"].pBiomass = 0; commodityInfo["instruments"].pMinerals = 0; commodityInfo["instruments"].pFoodOutputBonus = 1; commodityInfo["instruments"].pIndustryOutputBonus = 2; commodityInfo["instruments"].pInnovationBonus = 1; commodityInfo["instruments"].pMilitaryBonus = 1; commodityInfo["instruments"].bFoodOutputBonus = 1; commodityInfo["instruments"].bIndustryOutputBonus = -2; commodityInfo["instruments"].bAffluenceBonus = 1; commodityInfo["instruments"].bMilitaryBonus = 1;
        commodityInfo["advanced"].name = "advanced"; commodityInfo["advanced"].category = "medical"; commodityInfo["advanced"].legality = 0; commodityInfo["advanced"].shelfLife = 180; commodityInfo["advanced"].value = 20; commodityInfo["advanced"].marketShareBase = 10; commodityInfo["advanced"].pTechLevel = 0.55f; commodityInfo["advanced"].pOrbital = 0.3f; commodityInfo["advanced"].pBiomass = 0; commodityInfo["advanced"].pMinerals = 0; commodityInfo["advanced"].pFoodOutputBonus = 1; commodityInfo["advanced"].pIndustryOutputBonus = 1; commodityInfo["advanced"].pInnovationBonus = 1; commodityInfo["advanced"].pMilitaryBonus = 1; commodityInfo["advanced"].bFoodOutputBonus = 1; commodityInfo["advanced"].bIndustryOutputBonus = 1; commodityInfo["advanced"].bAffluenceBonus = 2; commodityInfo["advanced"].bMilitaryBonus = 1;
        commodityInfo["consumer"].name = "consumer"; commodityInfo["consumer"].category = "products"; commodityInfo["consumer"].legality = 0; commodityInfo["consumer"].shelfLife = 99999; commodityInfo["consumer"].value = 2; commodityInfo["consumer"].marketShareBase = 35; commodityInfo["consumer"].pTechLevel = 0.4f; commodityInfo["consumer"].pOrbital = 0; commodityInfo["consumer"].pBiomass = 0; commodityInfo["consumer"].pMinerals = 0; commodityInfo["consumer"].pFoodOutputBonus = 1; commodityInfo["consumer"].pIndustryOutputBonus = 1; commodityInfo["consumer"].pInnovationBonus = 1; commodityInfo["consumer"].pMilitaryBonus = 1; commodityInfo["consumer"].bFoodOutputBonus = 1; commodityInfo["consumer"].bIndustryOutputBonus = -2; commodityInfo["consumer"].bAffluenceBonus = 1; commodityInfo["consumer"].bMilitaryBonus = 1;
        commodityInfo["industrial"].name = "industrial"; commodityInfo["industrial"].category = "products"; commodityInfo["industrial"].legality = 0; commodityInfo["industrial"].shelfLife = 99999; commodityInfo["industrial"].value = 3; commodityInfo["industrial"].marketShareBase = 45; commodityInfo["industrial"].pTechLevel = 0.45f; commodityInfo["industrial"].pOrbital = 0; commodityInfo["industrial"].pBiomass = 0; commodityInfo["industrial"].pMinerals = 0; commodityInfo["industrial"].pFoodOutputBonus = 1; commodityInfo["industrial"].pIndustryOutputBonus = 1; commodityInfo["industrial"].pInnovationBonus = 1; commodityInfo["industrial"].pMilitaryBonus = 1; commodityInfo["industrial"].bFoodOutputBonus = 1; commodityInfo["industrial"].bIndustryOutputBonus = -2; commodityInfo["industrial"].bAffluenceBonus = 1; commodityInfo["industrial"].bMilitaryBonus = 1;
        commodityInfo["military"].name = "military"; commodityInfo["military"].category = "products"; commodityInfo["military"].legality = 3; commodityInfo["military"].shelfLife = 99999; commodityInfo["military"].value = 4; commodityInfo["military"].marketShareBase = 10; commodityInfo["military"].pTechLevel = 0.45f; commodityInfo["military"].pOrbital = 0; commodityInfo["military"].pBiomass = 0; commodityInfo["military"].pMinerals = 0; commodityInfo["military"].pFoodOutputBonus = 1; commodityInfo["military"].pIndustryOutputBonus = 1; commodityInfo["military"].pInnovationBonus = 1; commodityInfo["military"].pMilitaryBonus = 3; commodityInfo["military"].bFoodOutputBonus = 1; commodityInfo["military"].bIndustryOutputBonus = 1; commodityInfo["military"].bAffluenceBonus = 1; commodityInfo["military"].bMilitaryBonus = 3;
        commodityInfo["highTech"].name = "high Tech"; commodityInfo["highTech"].category = "products"; commodityInfo["highTech"].legality = 1; commodityInfo["highTech"].shelfLife = 99999; commodityInfo["highTech"].value = 12; commodityInfo["highTech"].marketShareBase = 6; commodityInfo["highTech"].pTechLevel = 0.55f; commodityInfo["highTech"].pOrbital = 0; commodityInfo["highTech"].pBiomass = 0; commodityInfo["highTech"].pMinerals = 0; commodityInfo["highTech"].pFoodOutputBonus = 1; commodityInfo["highTech"].pIndustryOutputBonus = 1; commodityInfo["highTech"].pInnovationBonus = 3; commodityInfo["highTech"].pMilitaryBonus = 1.5f; commodityInfo["highTech"].bFoodOutputBonus = 1; commodityInfo["highTech"].bIndustryOutputBonus = 1; commodityInfo["highTech"].bAffluenceBonus = 2; commodityInfo["highTech"].bMilitaryBonus = 1.5f;
        commodityInfo["cybernetic"].name = "cybernetic"; commodityInfo["cybernetic"].category = "products"; commodityInfo["cybernetic"].legality = 2; commodityInfo["cybernetic"].shelfLife = 99999; commodityInfo["cybernetic"].value = 35; commodityInfo["cybernetic"].marketShareBase = 3; commodityInfo["cybernetic"].pTechLevel = 0.58f; commodityInfo["cybernetic"].pOrbital = 0; commodityInfo["cybernetic"].pBiomass = 0; commodityInfo["cybernetic"].pMinerals = 0; commodityInfo["cybernetic"].pFoodOutputBonus = 1; commodityInfo["cybernetic"].pIndustryOutputBonus = 1; commodityInfo["cybernetic"].pInnovationBonus = 3; commodityInfo["cybernetic"].pMilitaryBonus = 1.5f; commodityInfo["cybernetic"].bFoodOutputBonus = 1; commodityInfo["cybernetic"].bIndustryOutputBonus = 1; commodityInfo["cybernetic"].bAffluenceBonus = 2; commodityInfo["cybernetic"].bMilitaryBonus = 1.5f;
        commodityInfo["artifacts"].name = "artifacts"; commodityInfo["artifacts"].category = "products"; commodityInfo["artifacts"].legality = 4; commodityInfo["artifacts"].shelfLife = 99999; commodityInfo["artifacts"].value = 60; commodityInfo["artifacts"].marketShareBase = 1; commodityInfo["artifacts"].pTechLevel = 0.8f; commodityInfo["artifacts"].pOrbital = 0; commodityInfo["artifacts"].pBiomass = 0; commodityInfo["artifacts"].pMinerals = 0; commodityInfo["artifacts"].pFoodOutputBonus = 1; commodityInfo["artifacts"].pIndustryOutputBonus = 1; commodityInfo["artifacts"].pInnovationBonus = 1; commodityInfo["artifacts"].pMilitaryBonus = 1; commodityInfo["artifacts"].bFoodOutputBonus = 1; commodityInfo["artifacts"].bIndustryOutputBonus = 1; commodityInfo["artifacts"].bAffluenceBonus = 3; commodityInfo["artifacts"].bMilitaryBonus = 1.5f;
        commodityInfo["ordnance"].name = "ordnance"; commodityInfo["ordnance"].category = "weapons"; commodityInfo["ordnance"].legality = 2; commodityInfo["ordnance"].shelfLife = 99999; commodityInfo["ordnance"].value = 5; commodityInfo["ordnance"].marketShareBase = 80; commodityInfo["ordnance"].pTechLevel = 0.45f; commodityInfo["ordnance"].pOrbital = 0; commodityInfo["ordnance"].pBiomass = 0; commodityInfo["ordnance"].pMinerals = 0; commodityInfo["ordnance"].pFoodOutputBonus = 1; commodityInfo["ordnance"].pIndustryOutputBonus = 1; commodityInfo["ordnance"].pInnovationBonus = 1; commodityInfo["ordnance"].pMilitaryBonus = 1; commodityInfo["ordnance"].bFoodOutputBonus = 1; commodityInfo["ordnance"].bIndustryOutputBonus = -2; commodityInfo["ordnance"].bAffluenceBonus = 1; commodityInfo["ordnance"].bMilitaryBonus = 1;
        commodityInfo["heavy"].name = "heavy"; commodityInfo["heavy"].category = "weapons"; commodityInfo["heavy"].legality = 3; commodityInfo["heavy"].shelfLife = 99999; commodityInfo["heavy"].value = 10; commodityInfo["heavy"].marketShareBase = 15; commodityInfo["heavy"].pTechLevel = 0.5f; commodityInfo["heavy"].pOrbital = 0; commodityInfo["heavy"].pBiomass = 0; commodityInfo["heavy"].pMinerals = 0; commodityInfo["heavy"].pFoodOutputBonus = 1; commodityInfo["heavy"].pIndustryOutputBonus = 1; commodityInfo["heavy"].pInnovationBonus = 1; commodityInfo["heavy"].pMilitaryBonus = 1.5f; commodityInfo["heavy"].bFoodOutputBonus = 1; commodityInfo["heavy"].bIndustryOutputBonus = 1; commodityInfo["heavy"].bAffluenceBonus = 1; commodityInfo["heavy"].bMilitaryBonus = 1.5f;
        commodityInfo["starship"].name = "starship"; commodityInfo["starship"].category = "weapons"; commodityInfo["starship"].legality = 3; commodityInfo["starship"].shelfLife = 99999; commodityInfo["starship"].value = 20; commodityInfo["starship"].marketShareBase = 4; commodityInfo["starship"].pTechLevel = 0.55f; commodityInfo["starship"].pOrbital = 0.4f; commodityInfo["starship"].pBiomass = 0; commodityInfo["starship"].pMinerals = 0; commodityInfo["starship"].pFoodOutputBonus = 1; commodityInfo["starship"].pIndustryOutputBonus = 1; commodityInfo["starship"].pInnovationBonus = 1; commodityInfo["starship"].pMilitaryBonus = 1.5f; commodityInfo["starship"].bFoodOutputBonus = 1; commodityInfo["starship"].bIndustryOutputBonus = 1; commodityInfo["starship"].bAffluenceBonus = 1; commodityInfo["starship"].bMilitaryBonus = 1;
        commodityInfo["xeno"].name = "xeno"; commodityInfo["xeno"].category = "weapons"; commodityInfo["xeno"].legality = 5; commodityInfo["xeno"].shelfLife = 99999; commodityInfo["xeno"].value = 160; commodityInfo["xeno"].marketShareBase = 1; commodityInfo["xeno"].pTechLevel = 0.9f; commodityInfo["xeno"].pOrbital = 0; commodityInfo["xeno"].pBiomass = 0; commodityInfo["xeno"].pMinerals = 0; commodityInfo["xeno"].pFoodOutputBonus = 1; commodityInfo["xeno"].pIndustryOutputBonus = 1; commodityInfo["xeno"].pInnovationBonus = 1; commodityInfo["xeno"].pMilitaryBonus = 1; commodityInfo["xeno"].bFoodOutputBonus = 1; commodityInfo["xeno"].bIndustryOutputBonus = 1; commodityInfo["xeno"].bAffluenceBonus = 2; commodityInfo["xeno"].bMilitaryBonus = 1;
        commodityInfo["drinks"].name = "drinks"; commodityInfo["drinks"].category = "narcotic"; commodityInfo["drinks"].legality = 2; commodityInfo["drinks"].shelfLife = 120; commodityInfo["drinks"].value = 12; commodityInfo["drinks"].marketShareBase = 50; commodityInfo["drinks"].pTechLevel = 0.0f; commodityInfo["drinks"].pOrbital = 0; commodityInfo["drinks"].pBiomass = 0; commodityInfo["drinks"].pMinerals = 0; commodityInfo["drinks"].pFoodOutputBonus = 1; commodityInfo["drinks"].pIndustryOutputBonus = 1; commodityInfo["drinks"].pInnovationBonus = 1; commodityInfo["drinks"].pMilitaryBonus = 1; commodityInfo["drinks"].bFoodOutputBonus = 1; commodityInfo["drinks"].bIndustryOutputBonus = 1; commodityInfo["drinks"].bAffluenceBonus = 1; commodityInfo["drinks"].bMilitaryBonus = 1;
        commodityInfo["tobacco"].name = "tobacco"; commodityInfo["tobacco"].category = "narcotic"; commodityInfo["tobacco"].legality = 1; commodityInfo["tobacco"].shelfLife = 720; commodityInfo["tobacco"].value = 6; commodityInfo["tobacco"].marketShareBase = 34; commodityInfo["tobacco"].pTechLevel = 0.0f; commodityInfo["tobacco"].pOrbital = 0; commodityInfo["tobacco"].pBiomass = 0.3f; commodityInfo["tobacco"].pMinerals = 0; commodityInfo["tobacco"].pFoodOutputBonus = 1.5f; commodityInfo["tobacco"].pIndustryOutputBonus = 1; commodityInfo["tobacco"].pInnovationBonus = 1; commodityInfo["tobacco"].pMilitaryBonus = 1; commodityInfo["tobacco"].bFoodOutputBonus = -1.5f; commodityInfo["tobacco"].bIndustryOutputBonus = 1; commodityInfo["tobacco"].bAffluenceBonus = 1; commodityInfo["tobacco"].bMilitaryBonus = 1;
        commodityInfo["drugs"].name = "drugs"; commodityInfo["drugs"].category = "narcotic"; commodityInfo["drugs"].legality = 4; commodityInfo["drugs"].shelfLife = 120; commodityInfo["drugs"].value = 20; commodityInfo["drugs"].marketShareBase = 12; commodityInfo["drugs"].pTechLevel = 0.0f; commodityInfo["drugs"].pOrbital = 0; commodityInfo["drugs"].pBiomass = 0; commodityInfo["drugs"].pMinerals = 0; commodityInfo["drugs"].pFoodOutputBonus = 1; commodityInfo["drugs"].pIndustryOutputBonus = 1; commodityInfo["drugs"].pInnovationBonus = 1; commodityInfo["drugs"].pMilitaryBonus = 1; commodityInfo["drugs"].bFoodOutputBonus = 1; commodityInfo["drugs"].bIndustryOutputBonus = 1; commodityInfo["drugs"].bAffluenceBonus = 1; commodityInfo["drugs"].bMilitaryBonus = 1;
        commodityInfo["psychic"].name = "psychic"; commodityInfo["psychic"].category = "narcotic"; commodityInfo["psychic"].legality = 5; commodityInfo["psychic"].shelfLife = 120; commodityInfo["psychic"].value = 80; commodityInfo["psychic"].marketShareBase = 4; commodityInfo["psychic"].pTechLevel = 0.57f; commodityInfo["psychic"].pOrbital = 0; commodityInfo["psychic"].pBiomass = 0; commodityInfo["psychic"].pMinerals = 0; commodityInfo["psychic"].pFoodOutputBonus = 1; commodityInfo["psychic"].pIndustryOutputBonus = 1; commodityInfo["psychic"].pInnovationBonus = 3; commodityInfo["psychic"].pMilitaryBonus = 1; commodityInfo["psychic"].bFoodOutputBonus = 1; commodityInfo["psychic"].bIndustryOutputBonus = 1; commodityInfo["psychic"].bAffluenceBonus = 1; commodityInfo["psychic"].bMilitaryBonus = 1;
    }

    static public int calculateCommodityPrice(string commodity, float localShare, int shortage, bool producable)
    {
        float priceMult = 1.0f;

        if (!producable)
        {
            priceMult += 0.1f;
        }
        if (localShare > commodityInfo[commodity].marketShareBase)
        {
            priceMult += 0.1f;
        }
        if (shortage > 0)
        {
            priceMult += shortage / 10.0f;
            priceMult += commodityInfo[commodity].shortagedPriceMult;
        }

        return (int)(priceMult * commodityInfo[commodity].value);
    }

    static public int calculateCommodityAmountInSale(string commodity, float localShare, int totalAmount,
                                                     float consumptionFactor, int shortage, bool producable)
    {
        if (!producable || localShare > commodityInfo[commodity].marketShareBase)
        {
            return (int)Math.Max(totalAmount - 1 - (consumptionFactor * 3 + shortage), 0.0);
        }
        else
        {
            return (int)Math.Max(totalAmount - 1 - (consumptionFactor + shortage), 0);
        }
    }
}
