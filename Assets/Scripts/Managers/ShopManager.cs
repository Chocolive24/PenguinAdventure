using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ShopManager : MonoBehaviour
{
    // Attributes ------------------------------------------------------------------------------------------------------
    
    
    // References ------------------------------------------------------------------------------------------------------
    [SerializeField] private GameObject _shopPanel;
    
    [Header("Cards Rect Transform")]
    [SerializeField] private RectTransform _rareCard1RectTrans;
    [SerializeField] private RectTransform _rareCard2RectTrans;
    [SerializeField] private RectTransform _epicCard1RectTrans;
    [SerializeField] private RectTransform _epicCard2RectTrans;
    [SerializeField] private RectTransform _legCard1RectTrans;
    [SerializeField] private RectTransform _legCard2RectTrans;
    [SerializeField] private RectTransform _aoeTilemapRectTrans;

    [Header("Cards Cost Textes")] 
    [SerializeField] private TextMeshProUGUI _rareCard1Cost;
    [SerializeField] private TextMeshProUGUI _rareCard2Cost;
    [SerializeField] private TextMeshProUGUI _epicCard1Cost;
    [SerializeField] private TextMeshProUGUI _epicCard2Cost;
    [SerializeField] private TextMeshProUGUI _legCard1Cost;
    [SerializeField] private TextMeshProUGUI _legCard2Cost;
    
    [Header("Relics Rect Transfrom")]
    [SerializeField] private RectTransform _relic1RectTrans;
    [SerializeField] private RectTransform _relic2RectTrans;

    [Header("Relics Cost Textes")] 
    [SerializeField] private TextMeshProUGUI _relic1Cost;
    [SerializeField] private TextMeshProUGUI _relic2Cost;
    
    private CardsManager _cardsManager;
    private GridManager _gridManager;

    // Methods ---------------------------------------------------------------------------------------------------------
    // Start is called before the first frame update
    private void Awake()
    {
        DoorTileCell.OnDoorTileEnter += ActivateShop;
    }

    private void Start()
    {
        _cardsManager = CardsManager.Instance;
        _gridManager = GridManager.Instance;
    }

    private void ActivateShop(DoorTileCell doorTile)
    {
        if (doorTile.GetRoomNeighbour().Type == RoomData.RoomType.SHOP)
        {
            _shopPanel.SetActive(true);

            DesactivateTiles(doorTile.GetRoomNeighbour());

            CreateObjects();
        }
    }

    private void DesactivateTiles(RoomData room)
    {
        for (int x = room.Bounds.xMin; x < room.Bounds.xMax; x++)
        {
            for (int y = room.Bounds.yMin; y < room.Bounds.yMax; y++)
            {
                TileCell tile = _gridManager.GetTileAtPosition(new Vector3(x, y, 0));
                tile.BoxCollider2D.enabled = false;
            }
        }
    }

    private void CreateObjects()
    {
        // TODO mettre dans une méthode.
        BaseCard cardRare1 = _cardsManager.InstantiateCard(_cardsManager.ScrBasicMoveCards, Rarety.Rare);
        cardRare1.transform.position = _rareCard1RectTrans.position;
        cardRare1.transform.parent = _rareCard1RectTrans.transform.parent;
        _rareCard1Cost.text = cardRare1.GetCost(50, 75).ToString();
        cardRare1.OnCollected += _cardsManager.AddCollectedCardToDeck;
    }
}
