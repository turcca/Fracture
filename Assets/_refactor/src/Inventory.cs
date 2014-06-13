using System;
using System.Collections.Generic;

public struct CommodityData
{
    public string name;
    public string description;
    public float marketShareBase;
    public float shortagedPriceMult;
    public int value;
}

//static public class Commodities
//{
//    static public string C_GRAIN = "grain";
//    // etc

//    static public Dictionary<string, CommodityData> Items;

//    static public int CalculatePrice(string commodity, float localShare, int shortage, bool producable)
//    {
//        var priceMult = 1.0;

//        if (!producable)
//        {
//            priceMult += 0.1;
//        }
//        if (localShare > Commodities.Items[commodity].marketShareBase)
//        {
//            priceMult += 0.1;
//        }
//        if (shortage > 0)
//        {
//            priceMult += shortage / 10.0;
//            priceMult += Commodities.Items[commodity].shortagedPriceMult;
//        }

//        return (int)(priceMult * Commodities.Items[commodity].value);
//    }

//    static public int CalculateAmountInSale(string commodity, float localShare, int totalAmount,
//                                            float consumptionFactor, int shortage, bool producable)
//    {
//        if (!producable || localShare > Commodities.Items[commodity].marketShareBase)
//        {
//            return (int)Math.Max(totalAmount - 1 - (consumptionFactor * 3 + shortage), 0.0);
//        }
//        else
//        {
//            return (int)Math.Max(totalAmount - 1 - (consumptionFactor + shortage), 0);
//        }
//    }
//}

//public class Inventory
//{
//    Dictionary<string, int> Items;
//}
