using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Kartenmanager : MonoBehaviour
{
    [SerializeField] GameObject deck;
    [SerializeField] GameObject ablageStapel;
    [SerializeField] GameObject hand;
    [SerializeField] NPCData[] npcs;
    int cardsOnHand = 5;

    public List<NPCData> cardsInDeck = new List<NPCData>();
    [SerializeField] GameObject cardPrefab;
    public AufgabenSO[] aufgabenList;
    public int aufgabenThisRound = 3;

    [SerializeField] GameObject aufgaben;
    [SerializeField] GameObject aufgabenPrefab;
    int aufgabenInstance = 3;
    
    int round = 1;
    [SerializeField] TextMeshProUGUI roundText;

    [SerializeField] GameObject freizeitSlot;
    [SerializeField] GameObject freizeitSlot2;
    [SerializeField] GameObject noInteractionAllowed;
    [SerializeField] RecruitedNpcScriptableObject teamData;
    

    void Start()
    {
        CreateAufgaben();
        
        ResetErschöpfung();
        GiveDataToDeck();
        StartCoroutine(GiveCardsToHandCoroutine());
        
    }

    //resets the erschöpfung at start of round weil scritable objects komisch sind
    void ResetErschöpfung()
    {
        
        for(int i = 0; i < cardsInDeck.Count; i++)
        {
            cardsInDeck[i].erschöpfung = 3;
        }
    }


    //get your cards data from the team object and transfer the data to the cards
    void GiveDataToDeck()
    {
        cardsInDeck = teamData.team;

        for (int i = 0; i < cardsInDeck.Count; i++)
        {
            for (int j = 0; j < 2; j++) // This loop will run twice
            {
                GameObject createdCard = Instantiate(cardPrefab, deck.transform.position, Quaternion.identity);
                createdCard.transform.SetParent(deck.transform);
                deck.transform.GetChild(i * 2 + j).GetComponent<DisplayKarten>().npcData = cardsInDeck[i];
            }
        }

       
    }



    //creates the aufgaben from list
    void CreateAufgaben()
    {
        for(int i = 0; i < aufgabenInstance; i++)
        {
            GameObject createdAufgabe = Instantiate(aufgabenPrefab, GameObject.Find("Aufgaben").transform.position, Quaternion.identity);
            createdAufgabe.transform.SetParent(GameObject.Find("Aufgaben").transform);
            createdAufgabe.GetComponent<Aufgaben>().aufgabenSO = aufgabenList[i];
        }
        

    }

    //move the cards from deck to the players hand
    IEnumerator GiveCardsToHandCoroutine()
    {
        for (int i = 0; i < cardsOnHand; i++)
        {
            Transform child = deck.transform.GetChild(0);
            child.SetParent(hand.transform);

            yield return new WaitForSeconds(1f);
        }

        noInteractionAllowed.SetActive(false);
    }


    void Update()
    {



    }

    //turn is over when player clicks hourglass
    public void TurnIsOver()
    {
        noInteractionAllowed.SetActive(true);
        // remove current hand on ablagestapel
        while (hand.transform.childCount > 0)
        {
            Transform child = hand.transform.GetChild(0);
            child.SetParent(ablageStapel.transform);
        }

        //clear freizeit slots onto ablagestapel if there are even any cards
        if (freizeitSlot.transform.childCount == 1)
        {
            freizeitSlot.GetComponentInChildren<DisplayKarten>().transform.SetParent(ablageStapel.transform);
        }

        if (freizeitSlot2.transform.childCount == 1)
        {
            freizeitSlot2.GetComponentInChildren<DisplayKarten>().transform.SetParent(ablageStapel.transform);
        }

        //if there are not enough cards in deck, get them from ablagestapel and shuffle through
        if (deck.transform.childCount < 5)
        {
            while (ablageStapel.transform.childCount > 0)
            {
                ablageStapel.transform.GetChild(0).SetParent(deck.transform);
            }



            ShuffleDeck();
        }

        //hand out new hand
        StartCoroutine(GiveCardsToHandCoroutine());

        //next round and reset everything
        round += 1;
        roundText.text = "Runde " + round + " / 10";

       

      
    }

    //shuffles the deck kind off randomly
    void ShuffleDeck()
    {
        int deckSize = deck.transform.childCount;

        //generate random position and place the child in there
        for (int i = 0; i < deckSize; i++)
        {
            int randomIndex = Random.Range(0, deckSize);

            deck.transform.GetChild(0).SetSiblingIndex(randomIndex);
        }
    }


   

    public void DrawTwoCards()
    {
        if (deck.transform.childCount < 2)
        {
            while (ablageStapel.transform.childCount > 0)
            {
                ablageStapel.transform.GetChild(0).SetParent(deck.transform);
            }



            ShuffleDeck();
        }

        cardsOnHand = 2;
        StartCoroutine(GiveCardsToHandCoroutine());
        cardsOnHand = 5;

    }

    public void PlusOneAttack()
    {
        for(int i = 0; i < npcs.Length; i++)
        {
            npcs[i].attack += 1;
            npcs[i].weakAttack += 1;
        }
    }

    public void StreitkarteMinus3NextTurn()
    {
        cardsOnHand -= 3;
    }

    
}
