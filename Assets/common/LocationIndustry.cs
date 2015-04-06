using System;
using System.Collections.Generic;

//public class Production
//{
//    public Info info = new Info();
//    public Market market = new Market();

//    public class Info
//    {
//        public float foodProduced = 0;
//        public float foodConsumed = 0;
//        public float foodOutput = 0;
//        public float foodOutputRelative = 0;
//        public float foodOutputRelativeWithStockpile = 0;
//        public float industryOutput = 0;
//        public float industryConsumed = 0;
//        public float industrySurplus = 0;
//        public float industryRelative = 0;
//        public float industryRelativeWithStockpile = 0;
//        public float economyOutput = 0;
//        public float economyConsumed = 0;
//        public float economySurplus = 0;
//        public float economyRelative = 0;
//        public float techFactor = 0;
//        public float techOutput = 0;

//        public void calculate(IdeologyData ideology, LocationData location)
//        {
//            foodProduced = ((ideology.effects.pgrowth / 100 + 5) / 5) * location.population * (location.infrastructure + 0.5f) * (location.biomass + 0.5f) * (location.biomass + 0.5f) * (location.techLevel + 0.5f) / 100000; // Food output per million
//            foodConsumed = location.population * ((ideology.effects.affluence / 100 + 5) / 5) / 100000 / 1.762f;
//            foodOutput = foodProduced - foodConsumed;
//            foodOutputRelative = foodProduced / foodConsumed;
//            //foodOutputRelativeWithStockpile = Mathf.Floor((foodProduced + foodStockpile()) / foodConsumed * 100) / 100;

//            industryOutput = ((ideology.effects.industry / 100 + 3) / 3) * location.population * (location.infrastructure + 0.5f) * (location.minerals + 0.5f) * (location.minerals + 0.5f) * (location.techLevel + 0.5f) / 100000; // Industry output per million
//            industryConsumed = location.population * ((ideology.effects.affluence / 100 + 4) / 4) * (location.techLevel + 0.5f) / 100000 / 1.302f;
//            industrySurplus = industryOutput - industryConsumed;
//            industryRelative = industryOutput / industryConsumed;
//            //industryRelativeWithStockpile = Mathf.Floor((industryOutput + industryStockpile()) / industryConsumed * 100) / 100;

//            economyOutput = (((ideology.effects.economy / 100 + 3) / 3) * location.population * (location.techLevel + 0.5f) / 100000) + location.orbitalInfra; // Industry output per million
//            economyConsumed = location.population / 100000 / 0.99f;
//            if (economyConsumed < 1)
//            {
//                economyConsumed = 1;
//                economyOutput += 1;
//            };
//            economySurplus = economyOutput - economyConsumed;
//            economyRelative = economyOutput / economyConsumed;

//            techFactor = ((ideology.effects.innovation + 100) / 200) * location.techLevel;
//            techOutput = (techFactor * (float)Math.Sqrt(location.population * (location.infrastructure * location.infrastructure)) / 10) / 10;
//        }
//    }

//    public class Market
//    {
//        public Dictionary<string, CommodityWeight> shares = new Dictionary<string, CommodityWeight>();

//        public class CommodityWeight
//        {
//            public float buy;
//            public float produce;
//            public float consumption = 1.0f;
//            public bool producable;
//        }

//        public Market()
//        {
//            foreach (string key in Economy.getCommodityNames())
//            {
//                shares[key] = new CommodityWeight();
//            }
//        }

//        public void normalize()
//        {
//            foreach (string category in Economy.getCommodityCategoryNames())
//            {
//                float totalProduce = 0;
//                float totalBuy = 0;
//                foreach (string c in Economy.getCommodityInfoOnCategory(category).Keys)
//                {
//                    totalProduce += shares[c].produce;
//                    totalBuy += shares[c].buy;
//                }
//                foreach (string c in Economy.getCommodityInfoOnCategory(category).Keys)
//                {
//                    if (totalProduce != 0)
//                    {
//                        shares[c].produce /= totalProduce;
//                    }
//                    if (totalBuy != 0)
//                    {
//                        shares[c].buy /= totalBuy;
//                    }
//                }
//            }
//        }
//    }

//    public void calculate(IdeologyData ideology, LocationData location)
//    {
//        info.calculate(ideology, location);
//    }

//    public string toDebugString()
//    {
//        string rv = "";
//        foreach (KeyValuePair<string, Market.CommodityWeight> entry in market.shares)
//        {
//            rv += entry.Key + " " + (entry.Value.producable ? entry.Value.produce.ToString() : "--") + " / " + entry.Value.buy.ToString() + " \n";
//        }
//        return rv;
//    }

//}

//public class LocationIndustry
//{
//    //Dictionary<string, int> stockpile = new Dictionary<string,int>();
//    Production production = new Production();

//    Location location;
//    float economyReserves = 0;


//    public LocationIndustry(Location loc)
//    {
//        location = loc;

//        production.calculate(location.ideology, location.info);

//        calculateCommodityShares(3.0f);
//        calcultateStartingStockpile();
//    }

//    void produceFood(float amount)
//    {
//        Random rng = new Random();
//        float foodToProduce = amount + foodFraction;
//        for (; foodToProduce >= 1.0; foodToProduce -= 1.0f)
//        {
//            float roll = (float)rng.NextDouble();
//            float productionProbability = 0.0f;
//            foreach (string key in Economy.getCommodityInfoOnCategory("food").Keys)
//            {
//                productionProbability += production.market.shares[key].produce;
//                if (roll < productionProbability)
//                {
//                    location.stockpile.commodities[key]++;
//                    break;
//                }
//            }
//        }
//        foodFraction = foodToProduce;
//    }

//    void consumeFood(float amount)
//    {
//        Random rng = new Random();
//        float foodToConsume = amount;
//        for (; foodToConsume >= 1.0; foodToConsume -= 1.0f)
//        {
//            float roll = (float)rng.NextDouble();
//            float consumeProbability = 0.0f;
//            string want = "";
//            foreach (string key in Economy.getCommodityInfoOnCategory("food").Keys)
//            {
//                consumeProbability += production.market.shares[key].buy;
//                if (roll < consumeProbability)
//                {
//                    want = key;
//                    break;
//                }
//            }

//            if (location.stockpile.commodities[want] > 0)
//            {
//                location.stockpile.commodities[want]--;
//                if (location.stockpile.lacking[want] > production.market.shares[want].consumption * 2)
//                {
//                    location.stockpile.lacking[want]--;
//                }
//                if (location.stockpile.lacking[want] > 0)
//                {
//                    location.stockpile.lacking[want]--;
//                }
//            }
//            else
//            {
//                location.stockpile.lacking[want]++;
//                bool eaten = false;
//                foreach (string substitute in Economy.getCommodityInfoOnCategory("food").Keys)
//                {
//                    if (location.stockpile.commodities[substitute] > 0)
//                    {
//                        location.stockpile.commodities[substitute]--;
//                        if (location.stockpile.lacking[substitute] > 0)
//                        {
//                            location.stockpile.lacking[substitute]--;
//                            eaten = true;
//                            break;
//                        }
//                    }
//                }
//                if (!eaten)
//                {
//                    // add hunger
//                }
//            }
//        }
//    }

//    void produceIndustry(float amount)
//    {
//        Random rng = new Random();
//        float industryToProduce = amount + industryFraction;
//        for (; industryToProduce >= 1.0; industryToProduce -= 1.0f)
//        {
//            float roll = (float)rng.NextDouble();
//            float productionProbability = 0.0f;
//            foreach (string key in Economy.getIndustryCommodityInfo().Keys)
//            {
//                productionProbability += production.market.shares[key].produce;
//                if (roll < productionProbability)
//                {
//                    location.stockpile.commodities[key]++;
//                    break;
//                }
//            }
//        }
//        industryFraction = industryToProduce;
//    }
//    void consumeIndustry(float amount)
//    {
//        Random rng = new Random();
//        float indToConsume = amount;
//        for (; indToConsume >= 1.0; indToConsume -= 1.0f)
//        {
//            float roll = (float)rng.NextDouble();
//            float consumeProbability = 0.0f;
//            string want = "";
//            foreach (string key in Economy.getIndustryCommodityInfo().Keys)
//            {
//                consumeProbability += production.market.shares[key].buy;
//                if (roll < consumeProbability)
//                {
//                    want = key;
//                    break;
//                }
//            }

//            if (location.stockpile.commodities[want] > 0)
//            {
//                location.stockpile.commodities[want]--;
//                if (location.stockpile.lacking[want] > production.market.shares[want].consumption * 2)
//                {
//                    location.stockpile.lacking[want]--;
//                }
//                if (location.stockpile.lacking[want] > 0)
//                {
//                    location.stockpile.lacking[want]--;
//                }
//            }
//            else
//            {
//                location.stockpile.lacking[want]++;
//                bool eaten = false;
//                foreach (string substitute in Economy.getIndustryCommodityInfo().Keys)
//                {
//                    //@todo make substitution order
//                    if (location.stockpile.commodities[substitute] > 0)
//                    {
//                        location.stockpile.commodities[substitute]--;
//                        if (location.stockpile.lacking[substitute] > 0)
//                        {
//                            location.stockpile.lacking[substitute]--;
//                            eaten = true;
//                            break;
//                        }
//                    }
//                }
//                if (!eaten)
//                {
//                    // add hunger
//                }
//            }
//        }
//    }

//    void getFoodReserves()
//    {
//        // just add all food items
//    }
//    void getIndustryReserves()
//    {
//        // just add all industry items
//    }

//    void calcultateStartingStockpile()
//    {
//        float foodItems = production.info.foodOutputRelative;
//        float industryItems = production.info.industryRelative;

//        produceFood(foodItems);
//        produceIndustry(industryItems);

//        economyReserves = (float)Math.Max(production.info.economySurplus * 3, 0);

//        //@todo hunger, foodReserves
//    }

//    void calculateCommodityShares(float currentLegality)
//    {
//        float legality = currentLegality + location.info.legalLevel;

//        // check production
//        foreach (KeyValuePair<string, CommodityInfo> entry in Economy.commodityInfo)
//        {
//            if (location.info.techLevel < entry.Value.pTechLevel ||
//                location.info.orbitalInfra < entry.Value.pOrbital ||
//                location.info.biomass < entry.Value.pBiomass ||
//                location.info.minerals < entry.Value.pMinerals ||
//                legality < entry.Value.legality)
//            {
//                production.market.shares[entry.Key].producable = false;
//            }
//            else
//            {
//                production.market.shares[entry.Key].producable = true;
//            }
//        }
//        //@todo check artifacts

//        float food = 1;
//        float industry = 1;
//        float military = location.ideology.effects.military / 2 + 1;
//        float innovation = location.ideology.effects.innovation / 2 + 1;
//        float affluence = location.ideology.effects.affluence / 2 + 1;

//        foreach (KeyValuePair<string, Production.Market.CommodityWeight> entry in production.market.shares)
//        {
//            if (entry.Value.producable)
//            {
//                entry.Value.produce = Economy.commodityInfo[entry.Key].marketShareBase *
//                    //Economy.sectorInfo.commodity[entry.Key].lackingWeight * //@todo
//                                      (float)Math.Pow(food, Economy.commodityInfo[entry.Key].pFoodOutputBonus) *
//                                      (float)Math.Pow(industry, Economy.commodityInfo[entry.Key].pIndustryOutputBonus) *
//                                      (float)Math.Pow(military, Economy.commodityInfo[entry.Key].pMilitaryBonus) *
//                                      (float)Math.Pow(innovation, Economy.commodityInfo[entry.Key].pInnovationBonus);
//            }
//            else
//            {
//                entry.Value.produce = 0;
//            }

//            entry.Value.buy = Economy.commodityInfo[entry.Key].marketShareBase *
//                              (float)Math.Pow(food, Economy.commodityInfo[entry.Key].bFoodOutputBonus) *
//                              (float)Math.Pow(industry, Economy.commodityInfo[entry.Key].bIndustryOutputBonus) *
//                              (float)Math.Pow(military, Economy.commodityInfo[entry.Key].bMilitaryBonus) *
//                              (float)Math.Pow(affluence, Economy.commodityInfo[entry.Key].bAffluenceBonus);
//            entry.Value.buy *= (float)Math.Min(legality / Economy.commodityInfo[entry.Key].legality, 1.0);
//        }

//        production.market.normalize();
//    }

//    float foodFraction = 0;
//    float industryFraction = 0;
//    public void tick(float days)
//    {
//        float delta = days * 1 / (30 * 12);
//        float foodProduced = delta * production.info.foodOutput;
//        float industryProduced = delta * production.info.industryOutput;

//        float foodRequired = delta * production.info.foodConsumed;
//        float industryRequired = delta * production.info.industryConsumed;

//        produceFood(foodProduced);
//        produceIndustry(industryProduced);

//        economyReserves += production.info.economySurplus * delta;
//        if (production.info.economySurplus > 0 && economyReserves > 0)
//        {
//            //@todo imperialFunds
//            //var tax = Math.Max(economySurplus / 10.0f * delta, (economyReserves - economySurplus * 2) / 1.03f * delta);
//            //economyReserves -= tax;
//        }

//        consumeFood(foodRequired);
//        //@todo hunger

//        consumeIndustry(industryRequired);
//        //@todo shortage

//        //@todo aid from imperial if enough shortage and not separatist
//    }

//    public int getCommodityPrice(string commodity)
//    {
//        return Economy.calculateCommodityPrice(commodity, production.market.shares[commodity].buy,
//            location.stockpile.lacking[commodity], production.market.shares[commodity].producable);
//    }

//    public string toDebugString()
//    {
//        string rv = production.toDebugString();
//        return rv;
//    }
//}
