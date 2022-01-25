using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ProtoBuf;
using TMPro;
using UnityEngine.UI;

public class ShopHandler : MonoBehaviour
{
    [SerializeField]
    private Transform DisplayTemplate;
    private static Dictionary<int, ShopItem.Skins> AvailableSkins = new Dictionary<int, ShopItem.Skins>()
    {
        { 1, new ShopItem.Skins { id = 1, name = "Default", description = "Do the default dance!!!!!", price=0 } },
        { 2, new ShopItem.Skins { id = 2, name = "Red", description = "An NFT reskin", price = 69 } },
        { 3, new ShopItem.Skins { id = 3, name = "Blue", description = "Another NFT reskin??!??!?", price = 69 } },
    };
    private void RenderSkinLists()
    {

    }
    private Transform TemplateGenerator(Transform parentobj, ShopItem.IShopItem ShopItem, Sprite ItemImg)
    {
        Transform Template = Instantiate(DisplayTemplate, parentobj);
        Template.Find("Title").GetComponent<TextMeshProUGUI>().text = ShopItem.name;
        Template.Find("Image").GetComponent<Image>().sprite = ItemImg;
        Template.Find("Price").GetComponent<TextMeshProUGUI>().text = ShopItem.price.ToString();
        return Template;
    }
    private void Awake()
    {

    }
    // Start is called before the first frame update
    void Start()
    {
        RenderSkinLists();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

namespace ShopItem
{
    interface IShopItem
    {
        int id { get; set; }
        string name { get; set; }
        string description { get; set; }
        int price { get; set; }
    }
    [ProtoContract]
    public class Skins : IShopItem
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