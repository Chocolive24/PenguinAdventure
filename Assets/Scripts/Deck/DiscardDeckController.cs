using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public enum DiscardCardType
{
    Movement,
    Main
}

public class DiscardDeckController : MonoBehaviour
{
    [SerializeField] protected List<BaseCard> _discardDeck;

    [SerializeField] protected HeroClass _heroClass;
    
    [SerializeField] private TextMeshProUGUI _carNbrTxt;

    [SerializeField] private DiscardCardType _discardCardType;
    
    // References ------------------------------------------------------------------------------------------------------
    private UnitsManager _unitsManager;
    
    // Events ----------------------------------------------------------------------------------------------------------
    public static event Action<DiscardDeckController> OnDiscarFull; 

    // Getters and Setters ---------------------------------------------------------------------------------------------
    public List<BaseCard> DiscardDeck
    {
        get => _discardDeck;
        set => _discardDeck = value; 
    }
   
    public HeroClass HeroClass => _heroClass;
    public DiscardCardType DiscardCardType => _discardCardType;

    // -----------------------------------------------------------------------------------------------------------------

    private void Awake()
    {
        BattleManager.OnBattleStart += SetDiscardDeck;
    }

    private void SetDiscardDeck(BattleManager obj, RoomData room)
    {
        if (!_unitsManager)
        {
            _unitsManager = UnitsManager.Instance;
        }
        
        _carNbrTxt.text = _discardDeck.Count.ToString();
        
        if (!_unitsManager.HeroPlayer.MainDiscardDeck)
        {
            if (_discardCardType == DiscardCardType.Main)
            {
                _unitsManager.HeroPlayer.MainDiscardDeck = this;
            }
        }
        if (!_unitsManager.HeroPlayer.MovDiscardDeck)
        {
            if (_discardCardType == DiscardCardType.Movement)
            {
                _unitsManager.HeroPlayer.MovDiscardDeck = this;
            }
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void AddACard(BaseCard card, DeckController deck)
    {
        _discardDeck.Add(card);
        
        if (_discardDeck.Count == deck.Size)
        {
            OnDiscarFull?.Invoke(this);
        }
        
        _carNbrTxt.text = _discardDeck.Count.ToString();
    }
    
    public void ShuffleCardsBackToDeck(DeckController deckController)
    {
        foreach (var card in _discardDeck)
        {
            deckController.Deck.Add(card);
        }
        
        deckController.UpdateCardTxtNbr();
        _discardDeck.Clear();
    }
}
