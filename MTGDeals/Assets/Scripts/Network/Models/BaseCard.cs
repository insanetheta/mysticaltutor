using System.Collections.Generic;
using UnityEngine;
using System.Collections;
using Newtonsoft.Json;

namespace DealFinder.Network.Models
{
    public class BaseCard
    {

        [JsonProperty]
        public int MultiverseID { get; private set; }

        [JsonProperty]
        public string Name { get; private set; }

        [JsonProperty]
        public string Rarity { get; private set; }
        
        //[JsonProperty]
        //public List<string> Formats { get; private set; }
    }
}


