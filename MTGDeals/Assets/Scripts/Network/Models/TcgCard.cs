using System.Collections.Generic;
using UnityEngine;
using System.Collections;
using Newtonsoft.Json;

namespace DealFinder.Network.Models
{
    public class TcgCard
    {
        [JsonProperty]
        public decimal HiPrice { get; private set; }

        [JsonProperty]
        public decimal AvgPrice { get; private set; }

        [JsonProperty]
        public decimal LowPrice { get; private set; }

        [JsonProperty]
        public int MultiverseID { get; private set; }

        [JsonProperty]
        public string Link { get; private set; }

        [JsonProperty]
        public string Name { get; private set; }

        [JsonProperty]
        public string CardSetName { get; private set; }

        [JsonProperty]
        public string Rarity { get; private set; }
        
        [JsonProperty]
        public List<string> Formats { get; private set; }
    }
}


