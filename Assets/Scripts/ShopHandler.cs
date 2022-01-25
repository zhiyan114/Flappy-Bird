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
    [SerializeField]
    private Transform Pages;
    [SerializeField]
    private Transform CheckoutUI;

    private ShopItem.IShopItem CurrentItem;
    private static Dictionary<int, ShopItem.Skins> AvailableSkins = new Dictionary<int, ShopItem.Skins>()
    {
        { 1, new ShopItem.Skins { id = 1, name = "Default", description = "Do the default dance!!!!!", price=0, DefaultSprite = "Skins/Default/yellowbird-midflap" } },
        { 2, new ShopItem.Skins { id = 2, name = "Red", description = "An NFT reskin", price = 69, DefaultSprite = "Skins/Red/redbird-midflap" } },
        { 3, new ShopItem.Skins { id = 3, name = "Blue", description = "Another NFT reskin??!??!?", price = 69, DefaultSprite = "Skins/Blue/bluebird-midflap" } },
    };
    private void RenderSkinLists()
    {
        foreach(KeyValuePair<int,ShopItem.Skins> skin in AvailableSkins)
        {
            TemplateGenerator(Pages.Find("SkinPage"), skin.Value).GetComponent<Button>().onClick.AddListener(delegate { ShopDetailDisplay_Handler(skin.Value); });
        }
    }
    private void ShopDetailDisplay_Handler(ShopItem.IShopItem skinObj)
    {
        CheckoutUI.Find("Title").GetComponent<TextMeshProUGUI>().text = skinObj.name;
        CheckoutUI.Find("Description").GetComponent<TextMeshProUGUI>().text = skinObj.description;
        CheckoutUI.Find("Image").GetComponent<Image>().sprite = Resources.Load<Sprite>("ShopItem/" + skinObj.DefaultSprite);
        CheckoutUI.Find("Price").GetComponent<TextMeshProUGUI>().text = skinObj.price.ToString();
        TextMeshProUGUI ActionBtnText = CheckoutUI.Find("ActionBtn").Find("ActionName").GetComponent<TextMeshProUGUI>();
        CurrentItem = skinObj;
        Button ActionBtn = CheckoutUI.Find("ActionBtn").GetComponent<Button>();
        if (SaveManager.Data.CurrentSkin == skinObj.id)
        {
            // User already owned the skin and equipped
            ActionBtn.interactable = false;
            ActionBtnText.text = "Equipped";
        }
        else if (SaveManager.Data.OwnedSkin.Contains(skinObj.id))
        {
            // User already owned the skin but didn't equip it
            ActionBtn.interactable = true;
            ActionBtnText.text = "Equip";
        }
        else
        {
            // User neither owned the skin or equip it
            ActionBtn.interactable = true;
            ActionBtnText.text = "Buy";
        }
    }
    RectTransform LastTemplate = null;
    [SerializeField]
    Transform DisplayShelf;
    private Transform TemplateGenerator(Transform parentobj, ShopItem.IShopItem ShopItem)
    {
        Transform Template = Instantiate(DisplayTemplate, parentobj);
        Template.Find("Title").GetComponent<TextMeshProUGUI>().text = ShopItem.name;
        Template.Find("Image").GetComponent<Image>().sprite = Resources.Load<Sprite>("ShopItem/" + ShopItem.DefaultSprite);
        Template.Find("Price").GetComponent<TextMeshProUGUI>().text = ShopItem.price.ToString();
        RectTransform Template_Rect = Template.GetComponent<RectTransform>();
        if (!(LastTemplate is null))
        {
            RectTransform Default_Rect = DisplayShelf.GetComponent<RectTransform>();
            float xdiff = Mathf.Abs(Default_Rect.anchorMax.x - Default_Rect.anchorMin.x);
            float ydiff = Mathf.Abs(Default_Rect.anchorMax.y - Default_Rect.anchorMin.y);
            if (LastTemplate.anchorMax.x < 0.8)
            {
                Template_Rect.anchorMin = new Vector2((float)(LastTemplate.anchorMax.x + 0.02), LastTemplate.anchorMin.y);
                float xValCalc = (float)(LastTemplate.anchorMax.x + xdiff + 0.02);
                Template_Rect.anchorMax = new Vector2((float)(xValCalc >= 1 ? 1 : xValCalc), LastTemplate.anchorMax.y);
            }
            else
            {
                // Needs to change Y position to fill in

                Template_Rect.anchorMin = new Vector2(Default_Rect.anchorMin.x, (float)((Default_Rect.anchorMin.y - ydiff) - 0.02));
                Template_Rect.anchorMax = new Vector2(Default_Rect.anchorMax.x, (float)(Default_Rect.anchorMin.y - 0.02));
            }
            //Template_Rect.anchorMin = new Vector2((float)((LastClone.anchorMin.x * 2)+0.02), (float)(LastClone.anchorMin.x > 0.95 ? (LastClone.anchorMin.y*2)+0.02 : LastClone.anchorMin.y));
        }
        LastTemplate = Template.GetComponent<RectTransform>();
        Template.gameObject.SetActive(true);
        return Template;
    }
    public void CheckoutAction_Handler()
    {
        if (LastTemplate is null) return;
        if (CurrentItem.id == SaveManager.Data.CurrentSkin) return;
        if (!SaveManager.Data.OwnedSkin.Contains(CurrentItem.id))
        {
            // Perform skin purchase
            if (!EconomyManager.setBalance(-CurrentItem.price))
            {
                // Not enough balance was detected
                StartCoroutine(LowBalanceMsg());
                return;
            };
            SaveManager.Data.OwnedSkin.Add(CurrentItem.id);
        }
        // Equip the skin if the user already owns it or after the purchase
        Transform ActionBtn = CheckoutUI.Find("ActionBtn");
        SaveManager.Data.CurrentSkin = CurrentItem.id;
        ActionBtn.GetComponent<Button>().interactable = false;
        ActionBtn.Find("ActionName").GetComponent<TextMeshProUGUI>().text = "Equipped";
    }
    private IEnumerator LowBalanceMsg()
    {
        Button ActionBtn = CheckoutUI.Find("ActionBtn").GetComponent<Button>();
        TextMeshProUGUI ActionBtnText = CheckoutUI.Find("ActionBtn").Find("ActionName").GetComponent<TextMeshProUGUI>();
        ActionBtn.interactable = false;
        ActionBtnText.text = "Not Enough Money";
        yield return new WaitForSeconds(3);
        ActionBtn.interactable = true;
        ActionBtnText.text = "Buy";
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
        string DefaultSprite { get; set; }
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
        [ProtoMember(5)]
        public string DefaultSprite { get; set; }
    }
}