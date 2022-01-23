using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ProtoBuf;
public class ShopHandler : MonoBehaviour
{
    public static Dictionary<int, ShopItem.Skins> AvailableSkins = new Dictionary<int, ShopItem.Skins>()
    {
        { 1, new ShopItem.Skins { id = 1, name = "Default", description = "Do the default dance!!!!!", price=0 } },
        { 2, new ShopItem.Skins { id = 2, name = "Red", description = "An NFT reskin", price = 69 } },
        { 3, new ShopItem.Skins { id = 3, name = "Blue", description = "Another NFT reskin??!??!?", price = 69 } },
    };

    private void Awake()
    {

    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

namespace ShopItem
{
    [ProtoContract]
    public class Skins
    {
        [ProtoMember(1)]
        public int id { get; set; }
        [ProtoMember(2)]
        public string name { get; set; }
        [ProtoMember(3)]
        public string description { get; set; }
        [ProtoMember(4)]
        public int price { get; set; }
    }
}