using BusinessObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BusinessLogic
{
    public class DressUPLogic
    {
        /// <summary>
        /// constants for the problem 
        ///     FAIL - the failed state
        ///     clothes - list of possible clothes to wear / actions to do
        /// </summary>
        public String FAIL;
        public List<String> clothes;

        public DressUPLogic()
        {
            FAIL = "fail";
            clothes = new List<string> { "sandals", "boots", "sunglasses", "hat", FAIL, "socks", "shirt", "shirt", FAIL, "jacket", "shorts", "pants", "leaving house", "leaving house", "Removing PJs", "Removing PJs" };

        }


        //command hash table that stores the mapping of command number to hot/cold responses
        public Dictionary<int, Dictionary<String, String>> commandMap;

        /// <summary>
        /// populates the hash table before processing the query
        /// </summary>
        public void populateResponses()
        {
            commandMap = new Dictionary<int, Dictionary<String, String>>();
            Dictionary<string, string> response;

            for (int i = 0, j = 1; i < clothes.Count; ++i)
            {
                response = new Dictionary<string, string>();
                response.Add(Temp.HOT.ToString(), clothes.ElementAt(i++));
                response.Add(Temp.COLD.ToString(), clothes.ElementAt(i));
                commandMap.Add(j++, response);
            }

        }
        /// <summary>
        /// Method to query the command data store to get list of responses
        /// </summary>
        /// <param name="key"> Temperature param </param>
        /// <param name="val"> Command number </param>
        /// <returns></returns>
        public List<String> queryData(String key, int val)
        {
            // Querying the hashtable using LINQ 
            var query = commandMap.Where(q => q.Key == val)
                              .SelectMany(p => p.Value)
                              .Where(f => f.Key == key)
                              .Select(d => d.Value)
                              .ToList();
            return query;
        }

        /// <summary>
        /// Request Handler for the user to get the actions to do based on a sequence of command numbers
        /// </summary>
        /// <param name="commandLine">
        ///     The string that the user needs to decode to decide on the actions to do
        /// </param>
        /// <returns></returns>
        public String getResponses(String Temprature, String commandLine)
        {
            populateResponses();    //populate the hash table

            HashSet<Int32> commands = new HashSet<int>();       // hashset to detect duplicate steps
            StringBuilder returnValue = new StringBuilder();    //return string that holds actions to do

            //unpack the command line to get the temperature and sequence of commands
            String[] paramList = commandLine.Split(',');
            String rest = String.Join("", paramList);

            string key = Temprature;  // Temperature outside
            for (var i = 1; i <= paramList.Length; ++i)
            {
                // iterate over the command number 
                int val = 0;
                Int32.TryParse(paramList[i - 1].TrimEnd(new char[] { ',' }), out val);

                //check if first command is to remove pajamas
                if (i == 1 && val != 8)
                {
                    //cannot put on clothes over pajamas
                    returnValue.Append(FAIL);
                    return returnValue.ToString();
                }
                else if (val > 8 || val < 1)
                {
                    //command not in range
                    returnValue.Append(FAIL);
                    return returnValue.ToString();
                }
                else if (val == 1)      //if Shoes, then check socks and pants
                {
                    if (key == Temp.COLD.ToString() && (!commands.Contains(3) || !commands.Contains(6)))
                    {
                        //Socks and Pants must be worn before shoes
                        returnValue.Append(FAIL);
                        return returnValue.ToString();
                    }
                    else if (key == Temp.HOT.ToString() && (!commands.Contains(6)))
                    {
                        //Pants must be worn before shoes
                        returnValue.Append(FAIL);
                        return returnValue.ToString();
                    }
                }
                else if (val == 2 || val == 5)      //if Headwear or jacket, check for Shirt!
                {
                    if (!commands.Contains(4))
                    {
                        //Shirt must be worn before headwear or jacket
                        returnValue.Append(FAIL);
                        return returnValue.ToString();
                    }
                }
                else if (val == 7)      // if leaving house, check for cardinality of clothes worn
                {
                    if ((key == Temp.COLD.ToString() && commands.Count != 7) || (key == Temp.HOT.ToString() && commands.Count < 5))
                    {
                        //Not all clothes are on
                        returnValue.Append(FAIL);
                        return returnValue.ToString();
                    }
                }

                //get the response corresponding to the temperature and command number
                var result = queryData(key, val);

                if (commands.Contains(val))
                {
                    //already worn -- duplicates
                    returnValue.Append(FAIL);
                    return returnValue.ToString();
                }
                else
                {
                    String resp = result[0].ToString();
                    //if fail is the response, then stop processing and return the actions so far
                    if (resp == FAIL)
                    {
                        //cannot put on socks or jacket in HOT weather"
                        returnValue.Append(FAIL);
                        return returnValue.ToString();
                    }
                    // add the command to hashset - Update the commands encountered so far
                    commands.Add(val);

                    // add the action to return value, avoiding comma at the end
                    if (val != 7)
                    {
                        returnValue.Append(resp);
                        returnValue.Append(", ");
                    }
                    else
                    {
                        returnValue.Append(resp);
                    }
                }
            }
            // all processing done, return the action responses
            return returnValue.ToString();
        }
    }
}