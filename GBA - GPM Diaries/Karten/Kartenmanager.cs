using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

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
    
    int round = 1;
    [SerializeField] TextMeshProUGUI roundText;

    [SerializeField] GameObject freizeitSlot;
    [SerializeField] GameObject freizeitSlot2;
    [SerializeField] GameObject noInteractionAllowed;
    public RecruitedNpcScriptableObject teamData;

    //[SerializeField] AudioSource battleTheme;
    [SerializeField] AudioSource drawCards;
    [SerializeField] AudioSource turnIsOverSound;
    [SerializeField] AudioSource aufgabeIsDestroyed;

    [SerializeField] GameObject dialogstuff;
    [SerializeField] GameObject dialogManager;
    [SerializeField] NPCData[] specialCards;
    


    void Start()
    {
        //battleTheme.Play();
      
        if (teamData.team.Count == 5)
        {
           teamData.team.Add(npcs[16]);
            
        }

        CreateAufgaben();

       

        ResetErschöpfung();
        GiveDataToDeck();
        StartCoroutine(GiveCardsToHandAnimated());

        if(teamData.chapterIndex > 1 || teamData.tutorial == false)
        {
            dialogstuff.SetActive(false);
            dialogManager.SetActive(false);
        }

        //only activate when testing/coding
       // teamData.team.Remove(npcs[16]);
        
    }

    public void RemoveTutorial()
    {
        teamData.tutorial = false;
    }

    //resets the ersch�pfung at start of round weil scritable objects komisch sind
    void ResetErschöpfung()
    {
        
        for(int i = 0; i < npcs.Length; i++)
        {
            npcs[i].erschöpfung = 3;
        }
    }



    //get your cards data from the team object and transfer the data to the cards
    void GiveDataToDeck()
    {
        cardsInDeck = teamData.team;

       
        
        

        for (int i = 0; i < teamData.specialCards.Count; i++)
        {
            GameObject createdCard = Instantiate(cardPrefab, deck.transform.position, Quaternion.identity);
            createdCard.transform.SetParent(deck.transform);
            createdCard.transform.SetAsFirstSibling();
            deck.transform.GetChild(0).GetComponent<DisplayKarten>().npcData = teamData.specialCards[i];

        }

        for (int i = 0; i < cardsInDeck.Count; i++)
        {
            for (int j = 0; j < 2; j++) // This loop will run twice
            {
                GameObject createdCard = Instantiate(cardPrefab, deck.transform.position, Quaternion.identity);
                createdCard.transform.SetParent(deck.transform);
                createdCard.transform.SetAsFirstSibling();
                deck.transform.GetChild(0).GetComponent<DisplayKarten>().npcData = cardsInDeck[i];
            }
        }

        ShuffleDeck();

        for(int i = 0; i < 6;i++)
        {
            GameObject.Find("Erschöpfung").gameObject.transform.GetChild(i).GetComponent<Erschöpfung>().GetNPCDataToErschöpfung();
        }
       
    }



    //creates the aufgaben from list
    void CreateAufgaben()
    {
        round = 1;
        if(teamData.chapterIndex == 1)
        {
            for (int i = 0; i < 3; i++)
            {
                GameObject createdAufgabe = Instantiate(aufgabenPrefab, GameObject.Find("Aufgaben").transform.position, Quaternion.identity);
                createdAufgabe.transform.SetParent(GameObject.Find("Aufgaben").transform);
                createdAufgabe.GetComponent<Aufgaben>().aufgabenSO = aufgabenList[i];
            }
        }
        else if(teamData.chapterIndex == 2)
        {
            for (int i = 3; i < 8; i++)
            {
                GameObject createdAufgabe = Instantiate(aufgabenPrefab, GameObject.Find("Aufgaben").transform.position, Quaternion.identity);
                createdAufgabe.transform.SetParent(GameObject.Find("Aufgaben").transform);
                createdAufgabe.GetComponent<Aufgaben>().aufgabenSO = aufgabenList[i];
            }
        }
        else if(teamData.chapterIndex == 3)
        {

        }
        else if (teamData.chapterIndex == 4)
        {

        }
        else if (teamData.chapterIndex == 5)
        {

        }





    }

    //outdated
    //move the cards from deck to the players hand
    IEnumerator GiveCardsToHandCoroutine()
    {
       
        for (int i = 0; i < cardsOnHand; i++)
        {
            Transform child = deck.transform.GetChild(0);
            child.SetParent(hand.transform);
            drawCards.Play();

            yield return new WaitForSeconds(1f);
        }

        

        noInteractionAllowed.SetActive(false);
        cardsOnHand = 5;
    }

    //method to move the cards to the hand but animated
    IEnumerator GiveCardsToHandAnimated()
    {
        //setup and coordinations
        float speed = 2000f; 
        Vector3 startPosition = deck.transform.position;
        Vector3 targetPosition = hand.transform.position;

        for (int i = 0; i < cardsOnHand; i++)
        {

           
            Transform cardTransform = deck.transform.GetChild(0).transform;
            cardTransform.localEulerAngles = new Vector3(0, 0, -45);
            drawCards.Play();

            while (Vector3.Distance(cardTransform.position, targetPosition) > 0.1f)
            {
                cardTransform.position = Vector3.MoveTowards(cardTransform.position, targetPosition, speed * Time.deltaTime);
                cardTransform.localEulerAngles += new Vector3(0, 0, 1f);

                yield return null;
            }

            // After the animation is complete, move the card to the hand
            Transform child = deck.transform.GetChild(0);
            child.SetParent(hand.transform);
            child.localEulerAngles = new Vector3(0, 0, 0);

            
        }

        noInteractionAllowed.SetActive(false);
        cardsOnHand = 5;
    }

    IEnumerator ThrowCardsAwayFromHandAnimated()
    {
        //setup and coordinations
        float speed = 4000f;
        Vector3 startPosition = hand.transform.position;
        Vector3 targetPosition = ablageStapel.transform.position;
        int currentCardsOnHandNumber = hand.transform.childCount;

        if (freizeitSlot.transform.childCount == 1)
        {
            Transform cardTransformi = freizeitSlot.transform.GetChild(0).transform;
            while (Vector3.Distance(cardTransformi.position, targetPosition) > 3f)
            {
                cardTransformi.position = Vector3.MoveTowards(cardTransformi.position, targetPosition, speed * Time.deltaTime);
                cardTransformi.localEulerAngles += new Vector3(0, 0, 1f);

                yield return null;
            }

           cardTransformi.GetComponent<DisplayKarten>().transform.SetParent(ablageStapel.transform);
           cardTransformi.localEulerAngles = new Vector3(0, 0, 0);

        }

        if (freizeitSlot.transform.childCount == 2)
        {
            for (int i = 0; i < 2; i++)
            {
                Transform cardTransformi = freizeitSlot.transform.GetChild(0).transform;
                while (Vector3.Distance(cardTransformi.position, targetPosition) > 3f)
                {
                    cardTransformi.position = Vector3.MoveTowards(cardTransformi.position, targetPosition, speed * Time.deltaTime);
                    cardTransformi.localEulerAngles += new Vector3(0, 0, 1f);

                    yield return null;
                }

                cardTransformi.GetComponent<DisplayKarten>().transform.SetParent(ablageStapel.transform);
                cardTransformi.localEulerAngles = new Vector3(0, 0, 0);
            }

        }

        if (GameObject.Find("ActionableField").transform.childCount == 1)
        {
            Transform cardTransformi = GameObject.Find("ActionableField").transform.GetChild(0).transform;
            while (Vector3.Distance(cardTransformi.position, targetPosition) > 3f)
            {
                cardTransformi.position = Vector3.MoveTowards(cardTransformi.position, targetPosition, speed * Time.deltaTime);
                cardTransformi.localEulerAngles += new Vector3(0, 0, 1f);

                yield return null;
            }

            cardTransformi.GetComponent<DisplayKarten>().transform.SetParent(ablageStapel.transform);
            cardTransformi.localEulerAngles = new Vector3(0, 0, 0);

            if(cardTransformi.GetComponent<DisplayKarten>().npcData.minus3NextTurn)
            {
                Destroy(cardTransformi.gameObject);
            }
        }

        for (int i = 0; i < currentCardsOnHandNumber ; i++)
        {
            

            Transform cardTransform = hand.transform.GetChild(0).transform;

            while (Vector3.Distance(cardTransform.position, targetPosition) > 3f)
            {
                cardTransform.position = Vector3.MoveTowards(cardTransform.position, targetPosition, speed * Time.deltaTime);
                cardTransform.localEulerAngles += new Vector3(0, 0, 1f);

                yield return null;
            }

            // After the animation is complete, move the card to the hand
            Transform child = hand.transform.GetChild(0);
            child.SetParent(ablageStapel.transform);
            child.localEulerAngles = new Vector3(0, 0, 0);

           
        }

        //hand out new hand
        StartCoroutine(GiveCardsToHandAnimated());

        //lower counter of aufgaben
        for (int i = 0; i < aufgaben.transform.childCount; i++)
        {
            aufgaben.transform.GetChild(i).GetComponent<Aufgaben>().aufgabenSO.countdown -= 1;
            aufgaben.transform.GetChild(i).GetComponent<Aufgaben>().counterText.text = aufgaben.transform.GetChild(i).GetComponent<Aufgaben>().aufgabenSO.countdown.ToString();
        }

        //next round and reset everything
        round += 1;
        roundText.text = "Runde " + round + " / 10";


        yield return null;
    }



    void Update()
    {



    }

    //turn is over when player clicks hourglass
    public void TurnIsOver()
    {
        turnIsOverSound.Play();
        noInteractionAllowed.SetActive(true);
        ResetAttack();

        if(teamData.tutorial)
        {
            teamData.tutorial = false;
        }
        // remove current hand on ablagestapel
        /* while (hand.transform.childCount > 0)
         {
             Transform child = hand.transform.GetChild(0);
             child.SetParent(ablageStapel.transform);
         }*/
        StartCoroutine(ThrowCardsAwayFromHandAnimated());

        //clear freizeit slots onto ablagestapel if there are even any cards
        if (freizeitSlot.transform.childCount == 1)
        {
           // freizeitSlot.GetComponentInChildren<DisplayKarten>().transform.SetParent(ablageStapel.transform);
            
        }

        if (freizeitSlot.transform.childCount == 2)
        {
            for(int i = 0; i < 2; i++)
            {
              //  freizeitSlot.GetComponentInChildren<DisplayKarten>().transform.SetParent(ablageStapel.transform);
            }
            
        }

        if(GameObject.Find("ActionableField").transform.childCount == 1)
        {
            //GameObject.Find("ActionableField").transform.GetChild(0).GetComponent<DisplayKarten>().allowDrag = true;
            //GameObject.Find("ActionableField").transform.GetChild(0).GetComponent<DisplayKarten>().parentbeforeDrag = GameObject.Find("Ablagestapel").transform;
            //GameObject.Find("ActionableField").transform.GetChild(0).SetParent(GameObject.Find("Ablagestapel").transform);
        }


        GameObject.Find("FirstSlot").GetComponent<Freizeit>().text.text = GameObject.Find("FirstSlot").GetComponent<Freizeit>().transform.childCount.ToString() + "/2";

        //if there are not enough cards in deck, get them from ablagestapel and shuffle through
        if (deck.transform.childCount < 5)
        {
            while (ablageStapel.transform.childCount > 0)
            {
                ablageStapel.transform.GetChild(0).SetParent(deck.transform);
            }



            ShuffleDeck();
        }

       
      

       

      
    }
    public IEnumerator GameIsOverCoroutine()
    {
        teamData.chapterIndex += 1;
        teamData.team.Remove(teamData.team[5]);
        teamData.specialCards.Add(specialCards[0]);
        teamData.specialCards.Add(specialCards[0]);
        teamData.specialCards.Add(specialCards[0]);

        if (teamData.chapterIndex == 2)
        {
            GameObject.Find("SceneTransition").GetComponent<LevelTransition>().FadedLoadScene("1_6Message");
           
        }
        if (teamData.chapterIndex == 3)
        {
            GameObject.Find("SceneTransition").GetComponent<LevelTransition>().FadedLoadScene("2_5Pitch1");

        }

        yield return null;
       
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
       

    }

    public void PlusOneAttack()
    {
        for(int i = 0; i < npcs.Length; i++)
        {
            npcs[i].attack += 1;
            npcs[i].weakAttack += 1;
        }
    }

    public void ResetAttack()
    {
        for (int i = 0; i < npcs.Length; i++)
        {
            npcs[i].attack = 3;
            npcs[i].weakAttack = 1;
        }
    }

    public void StreitkarteMinus3NextTurn()
    {
        cardsOnHand -= 3;
    }

    public void NothingCard()
    {

    }

    public void SoundWhenAufgabeIsDestroyed()
    {
        aufgabeIsDestroyed.Play();
    }

    public void ResetGame()
    {
        
        aufgabenList[0].countdown = 7;
        aufgabenList[0].health = 10;
        aufgabenList[1].countdown = 7;
        aufgabenList[1].health = 20;
        aufgabenList[2].countdown = 7;
        aufgabenList[2].health = 10;
        aufgabenList[3].countdown = 8;
        aufgabenList[3].health = 15;
        aufgabenList[4].countdown = 10;
        aufgabenList[4].health = 10;
        aufgabenList[5].countdown = 10;
        aufgabenList[5].health = 25;
        aufgabenList[6].countdown = 10;
        aufgabenList[6].health = 10;
        aufgabenList[7].countdown = 8;
        aufgabenList[7].health = 12;
        teamData.specialCards.Add(specialCards[1]);
        GameObject.Find("SceneTransition").GetComponent<LevelTransition>().FadedLoadScene("Kartenspiel");
    }

    
}
