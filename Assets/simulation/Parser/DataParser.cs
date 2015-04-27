using System.Collections;

public static class DataParser
{
    public static Data.LocationFeatures parseLocationFeatures(string locationData)
    {
        int i = 0;
        //string factionData = "";
        string locationFeaturesRaw = "";
        foreach (string value in locationData.Split(','))
        {
            if (i == 1)
            {
                // name
            }
            else if (i == 2)
            {
                // desc
            }
            else if (i >= 3 && i <= 11)
            {
                //factionData += value + ",";
            }
            else if (i >= 12)
            {
                locationFeaturesRaw += value + ",";
            }
            ++i;
        }

        return new Data.LocationFeatures(locationFeaturesRaw);
    }
}
