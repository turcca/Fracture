using System.Collections;
using System.Globalization;

public static class DataParser
{
    public static Data.LocationFeatures parseLocationFeatures(string locationData)
    {
        Data.LocationFeatures data = new Data.LocationFeatures();
        int i = 0;
        foreach (string value in locationData.Split('\t'))
        {
            switch(i)
            {
                case 1: data.name = value;
                    break;
                case 2: data.subsector = value;
                    break;
                case 3: data.description1 = value;
                    break;
                case 4: data.description2 = value;
                    break;
                case 5: data.hidden = value == "hiding" ? true : false;
                    break;

                case 27: data.resourceMultiplier[Data.Resource.Type.Food] = float.Parse(value, CultureInfo.InvariantCulture.NumberFormat);
                    break;
                case 28: data.resourceMultiplier[Data.Resource.Type.Mineral] = float.Parse(value, CultureInfo.InvariantCulture.NumberFormat);
                    break;
                case 29: data.resourceMultiplier[Data.Resource.Type.BlackMarket] = float.Parse(value, CultureInfo.InvariantCulture.NumberFormat);
                    break;
                case 30: data.resourceMultiplier[Data.Resource.Type.Innovation] = float.Parse(value, CultureInfo.InvariantCulture.NumberFormat);
                    break;
                case 31: data.resourceMultiplier[Data.Resource.Type.Culture] = float.Parse(value, CultureInfo.InvariantCulture.NumberFormat);
                    break;
                case 32: data.resourceMultiplier[Data.Resource.Type.Industry] = float.Parse(value, CultureInfo.InvariantCulture.NumberFormat);
                    break;
                case 33: data.resourceMultiplier[Data.Resource.Type.Economy] = float.Parse(value, CultureInfo.InvariantCulture.NumberFormat);
                    break;
                case 34: data.resourceMultiplier[Data.Resource.Type.Military] = float.Parse(value, CultureInfo.InvariantCulture.NumberFormat);
                    break;

                case 35: data.techLevel = int.Parse(value, CultureInfo.InvariantCulture.NumberFormat);
                    break;
                case 36: data.infrastructure = int.Parse(value, CultureInfo.InvariantCulture.NumberFormat);
                    break;
                case 37: data.militaryTechLevel = int.Parse(value, CultureInfo.InvariantCulture.NumberFormat);
                    break;

                case 38: data.population =  float.Parse(value, CultureInfo.InvariantCulture.NumberFormat);
                    break;
                case 39: data.assets = int.Parse(value, CultureInfo.InvariantCulture.NumberFormat);
                    break;

                default:
                    //Tools.debug("Read error, cell not in data range: " + i.ToString());
                    break;
            }
            ++i;
        }

        return data;
    }
}
