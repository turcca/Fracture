using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Simulation
{
    public static class Trade
    {
        public static void tradeResources(NPCShip ship)
        {
            foreach (Data.TradeItem item in ship.tradeList)
            {
                if (item.amount > 0.0f)
                {
                    if (item.isExported)
                    {
                        ship.home.economy.export(item.type, item.amount);
                        ship.destination.economy.import (item.type, item.amount);
                    }
                    else
                    {
                        ship.home.economy.import(item.type, item.amount);
                        ship.destination.economy.export(item.type, item.amount);
                    }
                }
            }
            ship.home.economy.updateTradeItems();
            ship.destination.economy.updateTradeItems();
        }
        // if ship is lost or loses cargo in transit, reverse retroactively trades
        public static void reverseTradeResources(NPCShip ship)
        {
            foreach (Data.TradeItem item in ship.tradeList)
            {
                if (item.amount > 0.0f)
                {
                    if (item.isExported)
                    {
                        if (ship.isGoingToDestination) // ship is still en route to destination and hasn't delivered exports
                        {
                            ship.home.economy.import(item.type, item.amount);
                            ship.destination.economy.export (item.type, item.amount);
                        }
                    }
                    else
                    {
                        ship.home.economy.export(item.type, item.amount);
                        ship.destination.economy.import(item.type, item.amount);
                    }
                }
            }
            ship.home.economy.updateTradeItems();
            ship.destination.economy.updateTradeItems();
        }


        // market commodity names
        public static string getCommodityName(Data.Resource.SubType resourceType)
        {
            switch (resourceType)
            {
            case Data.Resource.SubType.FoodT1:
                return "Grain";
            case Data.Resource.SubType.FoodT2:
                return "Canned Food";
            case Data.Resource.SubType.FoodT3:
                return "Frozen Food";
            case Data.Resource.SubType.FoodT4:
                return "Nutrigrafts"; 
                
                
            case Data.Resource.SubType.MineralT1:
                return "Raw Materials";
            case Data.Resource.SubType.MineralT2:
                return "Processed Materials";
            case Data.Resource.SubType.MineralT3:
                return "Advanced Materials";
            case Data.Resource.SubType.MineralT4:
                return "Nanomaterials";
                
            case Data.Resource.SubType.IndustryT1:
                return "Machinery";
            case Data.Resource.SubType.IndustryT2:
                return "Industrial Assembly";
            case Data.Resource.SubType.IndustryT3:
                return "Advanced Assembly";
            case Data.Resource.SubType.IndustryT4:
                return "Orbital Industries";
                
            case Data.Resource.SubType.InnovationT1:
                return "Education Assets";
            case Data.Resource.SubType.InnovationT2:
                return "Technical Assets";
            case Data.Resource.SubType.InnovationT3:
                return "Information Assets";
            case Data.Resource.SubType.InnovationT4:
                return "Hi-tech Assets";
                
            case Data.Resource.SubType.EconomyT1:
                return "Seed Economy";
            case Data.Resource.SubType.EconomyT2:
                return "Corporate Economy";
            case Data.Resource.SubType.EconomyT3:
                return "Banking Economy";
            case Data.Resource.SubType.EconomyT4:
                return "Planetary Economy";
                
            case Data.Resource.SubType.CultureT1:
                return "Consumer Necessities";
            case Data.Resource.SubType.CultureT2:
                return "Consumer Goods";
            case Data.Resource.SubType.CultureT3:
                return "Consumer Electronics";
            case Data.Resource.SubType.CultureT4:
                return "Consumer Luxuries";
                
            case Data.Resource.SubType.MilitaryT1:
                return "Ordnance";
            case Data.Resource.SubType.MilitaryT2:
                return "Heavy Weapons";
            case Data.Resource.SubType.MilitaryT3:
                return "Weapon Systems";
            case Data.Resource.SubType.MilitaryT4:
                return "Capital Weapons";
                
            case Data.Resource.SubType.BlackMarketT1:
                return "Alcohol";
            case Data.Resource.SubType.BlackMarketT2:
                return "Narcotics";
            case Data.Resource.SubType.BlackMarketT3:
                return "Black Market Goods";
            case Data.Resource.SubType.BlackMarketT4:
                return "Grey Tech";
                
            case Data.Resource.SubType.Unknown:
                return "Unknown";
                
            default:
                return "default";
            }
        }
        public static string getCommodityTypeName(Data.Resource.Type resourceType)
        {
            switch (resourceType)
            {
                case Data.Resource.Type.Food:
                    return "Food";
                
                case Data.Resource.Type.Mineral:
                    return "Materials";

                case Data.Resource.Type.Industry:
                    return "Machinery";

                case Data.Resource.Type.Innovation:
                    return "Innovation";

                case Data.Resource.Type.Economy:
                    return "Investments";
                
                case Data.Resource.Type.Culture:
                    return "Consumer Goods";
              
                case Data.Resource.Type.Military:
                    return "Weapons";
             
                case Data.Resource.Type.BlackMarket:
                    return "Black Market";
              
                default:
                    return "default";
            }
        }
        public static string getCommodityDescription(Data.Resource.SubType resourceType)
        {
            switch (resourceType)
            {
            case Data.Resource.SubType.FoodT1: // Grain
                return "Celphos grain, Fusarium venenatum, soy and other crops that can be stored for transportation.";
            case Data.Resource.SubType.FoodT2: // Canned Food
                return "Airtight preserving of processed and fresh food ingredients: vegetables, meat, seafood and dairy.";
            case Data.Resource.SubType.FoodT3: // Frozen Food
                return "More advanced methods of transporting food while preserving all its nutrients.";
            case Data.Resource.SubType.FoodT4: // Nutrigrafts
                return "Food supplements, balanced meals and genetically modified food made to meet all nutritional requirements.";
                
                
            case Data.Resource.SubType.MineralT1: // Raw Materials
                return "Common materials in minimally processed or unprocessed states: metal ores, raw minerals and extracted chemicals.";
            case Data.Resource.SubType.MineralT2: // Processed Materials
                return "Processed metals, alloys and chemicals for industrial manufacturing: metals, alloys, plastics and refined chemicals.";
            case Data.Resource.SubType.MineralT3: // Advanced Materials
                return "Materials for Hi-tech industrial needs: liquid crystals, semiconductors, superconductors, optics, mesoporous materials, shape memory alloys, light-emitting materials, magnetic materials, thin films, and colloids.";
            case Data.Resource.SubType.MineralT4: // Nanomaterials
                return "Microfabricated materials with structure matrix at the nanoscale are highly sought after due to their extraordinary properties.";
                
            case Data.Resource.SubType.IndustryT1: // Machinery
                return "Machinery, tools, ground vehicles and replacement parts for upkeeping and building basic low-scale infrastructure and manufacturing.";
            case Data.Resource.SubType.IndustryT2: // Industrial Assembly
                return "Assembly lines and components for the needs of scalable heavy industry.";
            case Data.Resource.SubType.IndustryT3: // Advanced Assembly
                return "Advanced manufacturing components for electronics and complex manufacturing industries.";
            case Data.Resource.SubType.IndustryT4: // Orbital Industries
                return "Orbital construction, orbital transportation, highly automated factory modules capable of autonomous deployment and sophisticated manufacturing.";
                
            case Data.Resource.SubType.InnovationT1: // Education Assets
                return "Teachers, training programs, medical supplies, blueprints and other foundational colony assets.";
            case Data.Resource.SubType.InnovationT2: // Technical Assets
                return "Medical instruments, comm systems and specialists like engineers doctors and lawmakers.";
            case Data.Resource.SubType.InnovationT3: // Information Assets
                return "Computer systems, scientists, sensors, satellites and information networks.";
            case Data.Resource.SubType.InnovationT4: // Hi-tech Assets
                return "Robotics, neural cores, med vats, sophisticated software and other cutting edge technology.";
                
            case Data.Resource.SubType.EconomyT1: // Seed Economy
                return "Economic instruments, seed investments, agents and prospectors.";
            case Data.Resource.SubType.EconomyT2: // Corporate Economy
                return "Corporation assets, investors and private funding.";
            case Data.Resource.SubType.EconomyT3: // Banking Economy
                return "Banking instruments, economists and securities.";
            case Data.Resource.SubType.EconomyT4: // Planetary Economy
                return "Planetary assets, major investments and treasuries.";
                
            case Data.Resource.SubType.CultureT1: // Consumer Necessities
                return "Clothing, furniture and other personal effects.";
            case Data.Resource.SubType.CultureT2: // Consumer Goods
                return "Appliances, services and entertainment products.";
            case Data.Resource.SubType.CultureT3: // Consumer Electronics
                return "Personal computers, consumer software, entertainment electronics and digital entertainment.";
            case Data.Resource.SubType.CultureT4: // Consumer Luxuries
                return "Cybernetics, VR, Virtual Personal Assistants and other high end products.";
                
            case Data.Resource.SubType.MilitaryT1: // Ordnance
                return "Personal assault weapons and infantry armor.";
            case Data.Resource.SubType.MilitaryT2: // Heavy Weapons
                return "Support weapons to take out larger targets.";
            case Data.Resource.SubType.MilitaryT3: // Weapon Systems
                return "Rockets, las cannons and combat vehicles.";
            case Data.Resource.SubType.MilitaryT4: // Capital Weapons
                return "Ship-mounted weapon system used in space or fracture.";
                
            case Data.Resource.SubType.BlackMarketT1: // Alcohol
                return "The most used substance frowned upon by the Church. On most worlds, it's openly sold and consumed.";
            case Data.Resource.SubType.BlackMarketT2: // Narcotics
                return "Imperium's controlled substances lists khat drops, god's flesh and a plethora of other mind and body altering drugs. Of course, not all worlds enforce old Imperial custom laws.";
            case Data.Resource.SubType.BlackMarketT3: // Black Market Goods
                return "Commodities found illegal on most worlds: radioactive materials, immerse chips, fugitives, slaves, organs and Arch technology.";
            case Data.Resource.SubType.BlackMarketT4: // Grey Tech
                return "Dangerous, higly illegal and highly sought after grey tech like Tears of Elysium, psy drugs, restricted weapons, nanos and admin software.";
                
            case Data.Resource.SubType.Unknown: // 
                return "Artifacts, Myriad Nodes and unidentified pieces of technology are unique and often end up in the hands of those with most resources. Sometimes Grey Tech, they can be very dangerous.";
                
            default:
                return "default";
            }
        }
        public static string getCommodityItem(Data.Resource.SubType resourceType)
        {
            string[] items;
            
            switch (resourceType)
            {
            case Data.Resource.SubType.FoodT1: // Grain
                items = new string[] {"Celphos grain", "Fusarium venenatum", "Soy beans"}; break;
            case Data.Resource.SubType.FoodT2: // Canned Food
                items = new string[] {"Canned vegetables", "Canned meat", "Canned seafood", "Canned dairy"}; break;
            case Data.Resource.SubType.FoodT3: // Frozen Food
                items = new string[] {"Frozen vegetables", "Frozen meat", "Frozen seafood", "Frozen dairy"}; break;
            case Data.Resource.SubType.FoodT4: // Nutrigrafts
                items = new string[] {"Food supplements", "Nutri-meals", "GMO seeds", "Luxury Foods"}; break;
                
                
            case Data.Resource.SubType.MineralT1: // Raw Materials
                items = new string[] {"Metal ores", "Raw minerals", "Bulk chemicals"}; break;
            case Data.Resource.SubType.MineralT2: // Processed Materials
                items = new string[] {"Steel", "Iron", "Copper", "Aluminium", "Plastics", "Petroleum", "Ammonia"}; break;
            case Data.Resource.SubType.MineralT3: // Advanced Materials
                items = new string[] {"Liquid crystals", "Semiconductors", "Superconductors", "Optics", "Mesoporous materials", "Shape memory alloys", "Light-emitting materials", "Magnetic materials", "Thin films", "Colloids"};  break;
            case Data.Resource.SubType.MineralT4: // Nanomaterials
                items = new string[] {"Nanoplates", "Nanofiber", "Crystalline  Nanoparticles", "Liquid crystals"}; break;
                
            case Data.Resource.SubType.IndustryT1: // Machinery
                items = new string[] {"Machinery", "Tractors and trucks", "Replacement parts", "Construction materials", "Self-sealing stem bolts", "Heavy cables"}; break;
            case Data.Resource.SubType.IndustryT2: // Industrial Assembly
                items = new string[] {"Assembly line", "Factory components", "Generators", "Heavy trucks", "Rail infrastructure", "Power infrastructure", "Precision machines"}; break;
            case Data.Resource.SubType.IndustryT3: // Advanced Assembly
                items = new string[] {"Circuit Assembly", "Electronics", "Industrial lasers", "Aircraft", "Nuclear reactor", "Circuitry"}; break;
            case Data.Resource.SubType.IndustryT4: // Autofabs
                items = new string[] {"Orbital assembly", "Orbital constructors", "Orbital shuttles", "Orbit slings", "Industrial robots", "Autolab", "Core reactor", "Energy field emitters"}; break;
                
            case Data.Resource.SubType.InnovationT1: // Education Assets
                items = new string[] {"Teachers", "Education facilities", "Medical supplies", "Colonial blueprints"}; break;
            case Data.Resource.SubType.InnovationT2: // Technical Assets
                items = new string[] {"Medical instruments", "Comm systems", "Engineering equipment", "Doctors and lawmakers", "Prospecting equipment", "Surveyors"}; break;
            case Data.Resource.SubType.InnovationT3: // Information Assets
                items = new string[] {"Computer systems", "Scientists and instruments", "Sensors", "Satellites", "Information networks"}; break;
            case Data.Resource.SubType.InnovationT4: // Hi-tech Assets
                items = new string[] {"Robotics", "Neural cores", "Med vats", "Advaced computers", "Orbital labs", "VP matrixes", "Fracture sensors", "Beacons"}; break;
                
            case Data.Resource.SubType.EconomyT1: // Seed Economy
                items = new string[] {"Economic instruments", "Seed investments", "Agents and prospectors"}; break;
            case Data.Resource.SubType.EconomyT2: // Corporate Economy
                items = new string[] {"Corporation assets", "Investors", "Private funding"}; break;
            case Data.Resource.SubType.EconomyT3: // Banking Economy
                items = new string[] {"Banking instruments", "Economy infrastructure", "Securities"}; break;
            case Data.Resource.SubType.EconomyT4: // Planetary Economy
                items = new string[] {"Planetary assets", "Major investments", "Treasuries"}; break;
                
            case Data.Resource.SubType.CultureT1: // Consumer Necessities
                items = new string[] {"Clothing", "Furniture", "Personal effects", "Filters", "Tools"}; break;
            case Data.Resource.SubType.CultureT2: // Consumer Goods
                items = new string[] {"Appliances", "Batteries", "Books", "Consumer services", "Radio recievers", "Personal transports"}; break;
            case Data.Resource.SubType.CultureT3: // Consumer Electronics
                items = new string[] {"Personal computers", "Boxed software", "Entertainment electronics", "Digital entertainment"}; break;
            case Data.Resource.SubType.CultureT4: // Consumer Luxuries
                items = new string[] {"Cybernetics", "VR equipment", "Virtual Personal Assistants", "Neural links", "Luxury transports"}; break;
                
            case Data.Resource.SubType.MilitaryT1: // Ordnance
                items = new string[] {"Assault rifles", "Combat armor"}; break;
            case Data.Resource.SubType.MilitaryT2: // Heavy Weapons
                items = new string[] {"Heavy weapons", "Power armor"}; break;
            case Data.Resource.SubType.MilitaryT3: // Weapon Systems
                items = new string[] {"Defense matrix", "Combat vehicles"}; break;
            case Data.Resource.SubType.MilitaryT4: // Capital Weapons
                items = new string[] {"Capital artillery system", "Capital rocket system", "Capital laser system"}; break;
                
            case Data.Resource.SubType.BlackMarketT1: // Alcohol
                items = new string[] {"Alcohol"}; break;
            case Data.Resource.SubType.BlackMarketT2: // Narcotics
                items = new string[] {"Khat drops", "God's flesh", "Methadone", "Amphetamine", "Angel Meld", "Glitterdust"}; break;
            case Data.Resource.SubType.BlackMarketT3: // Black Market Goods
                items = new string[] {"Fissile materials", "Immerse chips", "Fugitives", "Slaves", "Organs", "Arch technology"}; break;
            case Data.Resource.SubType.BlackMarketT4: // Grey Tech
                items = new string[] {"Tears of Elysium", "Psy drugs", "Bio weapons", "Nanos", "Admin software", "Cortex implants"}; break;
                
            case Data.Resource.SubType.Unknown: // 
                items = new string[] {"Artifacts", "Myriad Node", "Unidentified tech", "Excavated relics", "Mysterious artifact"}; break;
                
            default:
                return "default";
            }
            return items[UnityEngine.Random.Range (0, items.Length-1)];
        }
        public static string getCommodityDescription(KeyValuePair<Data.Resource.Type, float> item, bool lowerCase = true)
        {
            return getCommodityDescription(item.Key, lowerCase);
        }
        public static string getCommodityDescription(Data.Resource.Type type, bool lowerCase = true)
        {
            string rv = "";

            if (type == Data.Resource.Type.Food)
            {
                rv += "Food items";
            }
            else if (type == Data.Resource.Type.Mineral)
            {
                rv += "Materials";
            }
            else if (type == Data.Resource.Type.Industry)
            {
                rv += "Industrial machinery";
            }
            else if (type == Data.Resource.Type.Economy)
            {
                rv += "Foreign investments";
            }
            else if (type == Data.Resource.Type.Innovation)
            {
                rv += "Innovation assets";
            }
            else if (type == Data.Resource.Type.Culture)
            {
                rv += "Consumer goods";
            }
            else if (type == Data.Resource.Type.Military)
            {
                rv += "Weapons";
            }
            else if (type == Data.Resource.Type.BlackMarket)
            {
                rv += "Black market goods";
            }
            if (!lowerCase) return rv;
            else return rv.ToLower();
        }
        public static Color32 getTypeColor(Data.Resource.Type type)
        {
            switch (type)
            {
                case Data.Resource.Type.Food:
                    return new Color32(28, 162, 50, 50); // green
                case Data.Resource.Type.Mineral:
                    return new Color32(133, 73, 0, 32); // brown
                case Data.Resource.Type.Industry:
                    return new Color32(146, 88, 141, 30); // cold grey, purplish
                case Data.Resource.Type.Economy:
                    return new Color32(255, 242, 0, 24); // yellow
                case Data.Resource.Type.Innovation:
                    return new Color32(0, 152, 255, 16); // blue
                case Data.Resource.Type.Culture:
                    return new Color32(255, 109, 0, 40); // orange
                case Data.Resource.Type.Military:
                    return new Color32(255, 0, 0, 30); // red
                case Data.Resource.Type.BlackMarket:
                    return new Color32(0, 0, 0, 20); // black

                default:
                    return new Color32(0, 0, 0, 0);
            }
        }

        public static float getCommodityValue(Data.Resource.SubType resourceType)
        {
            switch (resourceType)
            {
            case Data.Resource.SubType.FoodT1:
                return 5.0f;
            case Data.Resource.SubType.FoodT2:
                return 8.0f;
            case Data.Resource.SubType.FoodT3:
                return 12.0f;
            case Data.Resource.SubType.FoodT4:
                return 30.0f; 
                
                
            case Data.Resource.SubType.MineralT1:
                return 6.0f;
            case Data.Resource.SubType.MineralT2:
                return 10.0f;
            case Data.Resource.SubType.MineralT3:
                return 20.0f;
            case Data.Resource.SubType.MineralT4:
                return 40.0f;
                
            case Data.Resource.SubType.IndustryT1:
                return 8.0f;
            case Data.Resource.SubType.IndustryT2:
                return 18.0f;
            case Data.Resource.SubType.IndustryT3:
                return 28.0f;
            case Data.Resource.SubType.IndustryT4:
                return 50.0f;
                
            case Data.Resource.SubType.InnovationT1:
                return 10.0f;
            case Data.Resource.SubType.InnovationT2:
                return 20.0f;
            case Data.Resource.SubType.InnovationT3:
                return 35.0f;
            case Data.Resource.SubType.InnovationT4:
                return 70.0f;
                
            case Data.Resource.SubType.EconomyT1:
                return 10.0f;
            case Data.Resource.SubType.EconomyT2:
                return 20.0f;
            case Data.Resource.SubType.EconomyT3:
                return 35.0f;
            case Data.Resource.SubType.EconomyT4:
                return 70.0f;
                
            case Data.Resource.SubType.CultureT1:
                return 10.0f;
            case Data.Resource.SubType.CultureT2:
                return 20.0f;
            case Data.Resource.SubType.CultureT3:
                return 35.0f;
            case Data.Resource.SubType.CultureT4:
                return 70.0f;
                
            case Data.Resource.SubType.MilitaryT1:
                return 20.0f;
            case Data.Resource.SubType.MilitaryT2:
                return 40.0f;
            case Data.Resource.SubType.MilitaryT3:
                return 60.0f;
            case Data.Resource.SubType.MilitaryT4:
                return 80.0f;
                
            case Data.Resource.SubType.BlackMarketT1:
                return 5.0f;
            case Data.Resource.SubType.BlackMarketT2:
                return 30.0f;
            case Data.Resource.SubType.BlackMarketT3:
                return 60.0f;
            case Data.Resource.SubType.BlackMarketT4:
                return 150.0f;
                
            case Data.Resource.SubType.Unknown:
                return 500.0f;
                
            default:
                return 1.0f;
            }
        }

        public static float calculateItemPriceMultiplier (Data.TradeItem item)
        {
            float mul;

            if (item.isExported)
            {
                // export
                mul = 1.0f - (item.weight/20.0f); //1.0f/ (item.weight+0.1f);
            }
            else
            {
                // import
                if (item.weight == 0.0f) mul = 1.0f; // probably upgrading resource - don't over-buy
                else if (item.weight < 1.0f) mul = 1.1f; // less interest in importing
                else mul = item.weight;
            }
            return Mathf.Round(mul *100.0f)/100.0f;
        }
    }
}
