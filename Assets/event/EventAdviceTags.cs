using UnityEngine;
using System.Collections;
using System.Collections.Generic;

static public class EventAdviceTags
{

    //static public Dictionary<string, Dictionary<string, string> > 

    public static string resolveTags(string tagString, Character c)
    {
        if (tagString == null || c == null)
        {
            Debug.LogError("ERROR: null inputs. tagString: " + tagString + ", Character.name: " + "NOT IMPLEMENTED");
            return tagString;
        }
        // key: tagFound - see if more. Value: tagString
        KeyValuePair<bool, string> check = new KeyValuePair<bool, string>(true, tagString);

        // iterate string while it contains "£" tags
        while (check.Key)
        {
            check = resolveTag(check.Value, c);
        }

        return check.Value;
    }




    private static KeyValuePair<bool, string> resolveTag(string tagString, Character c)
    {
        int length = tagString.Length;

        if (length > 0)
        {
            for (int i = 0; i < length; i++)
            {
                // find personality tags
                if (tagString[i] == EventAdviceTagParser.tagKey[0]) // tag symbol, '&'
                {
                    string replaceText = null;
                    string tagRecord = null;
                    int end = length - i;

                    // look for end tag
                    for (int t = 1; t < end; t++)
                    {
                        if (tagString[i + t] == EventAdviceTagParser.tagKey[0])
                        {
                            // get rid of tags
                            tagRecord = tagString.Substring(i + 1, t - 1);
                            // check if first letter is uppercase
                            bool firstCharIsUppercase = false;
                            if (char.IsUpper(tagRecord[0])) firstCharIsUppercase = true;
                            // resolve tag from template
                            replaceText = getTemplateTag(tagRecord, c);
                            // replace first letter to uppercase if needed
                            if (firstCharIsUppercase) replaceText = uppercaseFirst(replaceText);
                            // patch resolved tag to its string
                            tagString = tagString.Replace(EventAdviceTagParser.tagKey[0] + tagRecord + EventAdviceTagParser.tagKey[0], replaceText);

                            return new KeyValuePair<bool, string>(true, tagString);
                        }
                    }
                    Debug.LogError("ERROR: second tag not found. tagRecord: " + tagRecord);
                    return new KeyValuePair<bool, string>(false, null);
                }
            }
        }
        return new KeyValuePair<bool, string>(false, tagString);
    }

    private static string getTemplateTag(string tagRecord, Character c)
    {
        // find tag from parsed EventAdviceTags.txt
        return EventAdviceTagsParsed.getEventAdviceTag(tagRecord, c);
    }

    public static string uppercaseFirst(string input)
    {
        char[] a = input.ToCharArray();
        a[0] = char.ToUpper(a[0]);
        return new string(a);
    }
    public static string lowercaseFirst(string input)
    {
        char[] a = input.ToCharArray();
        a[0] = char.ToLower(a[0]);
        return new string(a);
    }
}
