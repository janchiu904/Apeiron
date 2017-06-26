using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class PlayerMagicScript : MonoBehaviour {
    private BattleManagerScript BattleManager;
    //private int MagicType = 0;
    [SerializeField]
    private Animator[] MoveCardArray;
    [SerializeField]
    private Animator[] HPCardArray;
    [SerializeField]
    private Animator DamegeBuffIcon;
    [SerializeField]
    private Transform[] MagicCardArray;
    private bool[] MagicCardDecisionArray;
    private int[] MagicCardDecisionChooseArray;
    [SerializeField]
    private Transform DecisionMagicCard;
    private int DecisionMagic;
    [SerializeField]
    private Transform UseCard;
    [SerializeField]
    private Animator DecisionCardAnim;

    private int CardSelectTarget;
    private bool CardSelectTargetFlashes = true;

    //private int DecisionMagic;

    private int HowUseCard;
    [SerializeField]
    private Sprite[] AllUseCardArray;
    // Use this for initialization
    void Start () {
        BattleManager = GameObject.FindWithTag("BattleManager").GetComponent<BattleManagerScript>();

        MagicCardDecisionArray = new bool[MagicCardArray.Length];
        MagicCardDecisionChooseArray = new int[MagicCardArray.Length];
        for (int x = 0; x < MagicCardDecisionArray.Length; x++) {
            MagicCardDecisionArray[x] = false;
            MagicCardDecisionChooseArray[x] = 0;
        }
    }
	
	// Update is called once per frame
	void Update () {
        if(BattleManager.PlayerCanCantroller == true)
        {
            MoveCard();
            HPCard();
            MagicCard();
            Magic();
            DemageBuffIcon();

        }
        
    }

    void Magic() {
        if (BattleManager.PlayerDamagePos[BattleManager.playerPosXY] != 0)
        {
            if (BattleManager.PlayerDamagePos[BattleManager.playerPosXY] != 11) {
                BattleManager.PlayerCanMove = true;
                CancelMagic();
                CardDecision(false);

            }
                
        }
        if (BattleManager.CanMagic == false) {
            CancelMagic();
            CardDecision(false);
            BattleManager.CanMagic = true;
            BattleManager.PlayerCanMove = true;
            

        }

            if (BattleManager.MagicType == 1)
        {
            CardSelect();
        }
        if (BattleManager.MagicType == 2)
        {
            CardDecisionSelect();
            
        }


        if (BattleManager.PlayerCanMagic == true)
        {
            if (Input.GetButtonDown("Fire1") && BattleManager.PlayerMagicCount > 0 && BattleManager.MagicCoolDown == false)
            {
                

                if (BattleManager.MagicType == 0)
                {
                    CardSelectTarget = 0;
                    BattleManager.PlayerCanMove = false;
                    BattleManager.MagicType = 1;
                }
                else if (BattleManager.MagicType == 1)
                {
                    CardDecision(true);
                }
                else if (BattleManager.MagicType == 2)
                {
                    BattleManager.MagicType = 0;
                    BattleManager.PlayerCanMove = true;
                    StartCoroutine(UseMagic());
                    StartCoroutine(BattleManager.CanMagicCoolDown());

                }
            }
            if (Input.GetButtonDown("Fire2"))
            {
                if (BattleManager.MagicType == 1)
                {
                    if (MagicCardDecisionArray[CardSelectTarget] == true)
                    {
                        MagicCardDecisionArray[CardSelectTarget] = false;
                        MagicCardArray[CardSelectTarget].transform.GetComponent<Animator>().SetBool("CardSelectTrue", false);

                        CardColor();

                    }
                    else {
                        BattleManager.PlayerCanMove = true;
                        BattleManager.MagicType = 0;
                        CancelMagic();
                    }

                   
                }
                else if (BattleManager.MagicType == 2)
                {
                    BattleManager.MagicType = 1;
                    CardDecision(false);
                }
            }
        }
    }

    void CancelMagic() {
        for (int x = 0; x < MagicCardArray.Length; x++)
        {

                //MagicCardArray[x].transform.GetComponent<RectTransform>().localPosition = new Vector2(MagicCardArray[x].transform.localPosition.x, 50f);
            MagicCardArray[x].transform.GetComponent<Image>().color = new Color(1F, 1F, 1F);
            MagicCardDecisionArray[x] = false;
            MagicCardArray[x].transform.GetComponent<Animator>().SetBool("CardSelect", false);
            MagicCardArray[x].transform.GetComponent<Animator>().SetBool("CardSelectTrue", false);
        }

    }

    private IEnumerator UseMagic() {
        
        yield return new WaitForSeconds(0.25f);

        BattleManager.CreatMagic(HowUseCard, DecisionMagic);

        int y = 0;
        for (int x = 0; x < MagicCardArray.Length; x++)
        {
            if (MagicCardDecisionArray[x] == true)
            {
                BattleManager.MagicArray[x] = 0;
                y += 1;
            }
        }
        BattleManager.PlayerMagicCount -= y;
        int z = 0;
        while (z <= BattleManager.PlayerMagicCount-1) {
            if (BattleManager.MagicArray[z] != 0)
            {
                z += 1;
            }
            else {
                for (int j = z; j < BattleManager.MagicArray.Length-1; j++) {
                    BattleManager.MagicArray[j] = BattleManager.MagicArray[j + 1];

                }

            }

        }
        
        CardDecision(false);
        CancelMagic();
        
    }

    void CardDecision(bool y) {
        if (y == true)
        {
            if (MagicCardDecisionArray[CardSelectTarget] == false)
            {
                MagicCardDecisionArray[CardSelectTarget] = true;
                MagicCardArray[CardSelectTarget].transform.GetComponent<Animator>().SetBool("CardSelectTrue", true);
                CardColor();
            }
            else{
                int z = 0;
                for (int x = 0; x < MagicCardArray.Length; x++)
                {
                    //if (BattleManager.PlayerMagicCount > x) MagicCardArray[x].transform.GetComponent<Image>().enabled = true;
                    if (BattleManager.PlayerMagicCount > x) MagicCardArray[x].transform.GetComponent<Animator>().SetBool("MagicCanUse", true);

                    //if (MagicCardArray[x].transform.GetComponent<RectTransform>().localPosition.y != 50)
                    if (MagicCardDecisionArray[x] == true)
                    {
                        BattleManager.ChooseMagicCard[z] = BattleManager.MagicArray[x];
                        z += 1;
                    }
                }
                MagicCardArray[CardSelectTarget].transform.GetComponent<Animator>().SetBool("CardSelect", false);
                BattleManager.MagicType = 2;
                DecisionCardAnim.SetBool("DecisionTrue", true);
                //DecisionMagicCard.transform.GetComponent<Image>().enabled = true;
                DecisionMagicCardImger();

                BattleManager.GroundAtk(false, BattleManager.playerPosXY, DecisionMagic,true, HowUseCard);
                //UseCard.transform.GetComponent<Image>().enabled = true;
            }
        }
        else {
            BattleManager.GroundAtk(false, BattleManager.playerPosXY, DecisionMagic, false, HowUseCard);
            for (int x = 0; x < BattleManager.ChooseMagicCard.Length; x++) {
                BattleManager.ChooseMagicCard[x] = 0;
                DecisionCardAnim.SetBool("DecisionTrue", false);
                //DecisionMagicCard.transform.GetComponent<Image>().enabled = false;
                //UseCard.transform.GetComponent<Image>().enabled = false;
            }

        }

    }

    void DecisionMagicCardImger() {
        if(BattleManager.ChooseMagicCard[0] == 1) {
            if (BattleManager.ChooseMagicCard[1] == 0)
            {
                DecisionMagic = 11;
                DecisionMagicCard.transform.GetComponent<Image>().sprite =
             BattleManager.AllMagicCardArray[1];
            }
            if (BattleManager.ChooseMagicCard[1] == 1)
            {
                DecisionMagic = 12;
                DecisionMagicCard.transform.GetComponent<Image>().sprite =
             BattleManager.AllAdvancedMagicCardArray[1];
            }
            if (BattleManager.ChooseMagicCard[1] == 2)
            {
                DecisionMagic = 01;
                DecisionMagicCard.transform.GetComponent<Image>().sprite =
             BattleManager.AllAdvancedMagicCardArray[0];
            }
            if (BattleManager.ChooseMagicCard[1] == 3)
            {
                DecisionMagic = 02;
                DecisionMagicCard.transform.GetComponent<Image>().sprite =
             BattleManager.AllAdvancedMagicCardArray[2];
            }
        }
        if (BattleManager.ChooseMagicCard[0] == 2)
        {
            if (BattleManager.ChooseMagicCard[1] == 0)
            {
                DecisionMagic = 21;
                DecisionMagicCard.transform.GetComponent<Image>().sprite =
             BattleManager.AllMagicCardArray[2];
            }
            if (BattleManager.ChooseMagicCard[1] == 1)
            {
                DecisionMagic = 01;
                DecisionMagicCard.transform.GetComponent<Image>().sprite =
             BattleManager.AllAdvancedMagicCardArray[0];
            }
            
        }
        if (BattleManager.ChooseMagicCard[0] == 3)
        {
            if (BattleManager.ChooseMagicCard[1] == 0)
            {
                DecisionMagic = 31;
                DecisionMagicCard.transform.GetComponent<Image>().sprite =
             BattleManager.AllMagicCardArray[3];
            }
            if (BattleManager.ChooseMagicCard[1] == 1)
            {
                DecisionMagic = 02;
                DecisionMagicCard.transform.GetComponent<Image>().sprite =
             BattleManager.AllAdvancedMagicCardArray[2];
            }

        }
        if (BattleManager.ChooseMagicCard[0] == 4)
        {
            if (BattleManager.ChooseMagicCard[1] == 0)
            {
                DecisionMagic = 41;
                DecisionMagicCard.transform.GetComponent<Image>().sprite =
             BattleManager.AllMagicCardArray[4];
            }
        }

    }

    void CardDecisionSelect() {

        UseCard.transform.GetComponent<Image>().sprite = AllUseCardArray[HowUseCard];
        if (Input.anyKeyDown && Mathf.Abs(Input.GetAxisRaw("Horizontal")) == 1) {
            BattleManager.GroundAtk(false, BattleManager.playerPosXY, DecisionMagic, false, HowUseCard);
            HowUseCard += 1;
            if (HowUseCard == AllUseCardArray.Length) HowUseCard = 0;
            BattleManager.GroundAtk(false, BattleManager.playerPosXY, DecisionMagic, true, HowUseCard);
        }

    }

    void CardSelect() {
        for (int x = 0; x < MagicCardArray.Length; x++)
        {
             MagicCardArray[x].transform.GetComponent<Animator>().SetBool("CardSelect", false);
            if (x == CardSelectTarget && BattleManager.MagicType == 1) MagicCardArray[x].transform.GetComponent<Animator>().SetBool("CardSelect", true);
        }
        if (Input.anyKeyDown)
        {
            
            if (Input.GetAxisRaw("Horizontal") == 1)
            {
                //MagicCardArray[CardSelectTarget].transform.GetComponent<Image>().enabled = true;
                MagicCardArray[CardSelectTarget].transform.GetComponent<Animator>().SetBool("CradSelect", false);
                for (int z = 0; z < 1; z=z) {
                    CardSelectTarget -= 1;
                    if (CardSelectTarget < 0) CardSelectTarget = BattleManager.PlayerMagicCount - 1;
                    if (MagicCardArray[CardSelectTarget].transform.GetComponent<Image>().color != new Color(0.5F, 0.5F, 0.5F)) {
                        z += 1;
                    }
                }
                
            }
            if (Input.GetAxisRaw("Horizontal") == -1 )
            {
                MagicCardArray[CardSelectTarget].transform.GetComponent<Animator>().SetBool("CradSelect", false);
                for (int z = 0; z < 1; z = z)
                {
                    CardSelectTarget += 1;
                    if (CardSelectTarget > BattleManager.PlayerMagicCount - 1) CardSelectTarget = 0;
                    if (MagicCardArray[CardSelectTarget].transform.GetComponent<Image>().color != new Color(0.5F, 0.5F, 0.5F))
                    {
                        z += 1;
                    }
                }
            }

            CardColor();

        }

    }

    void MagicCard() {
        for (int x = 0; x < MagicCardArray.Length; x++)
        {
            if (BattleManager.PlayerMagicCount > x)
            {
                
                MagicCardArray[x].transform.GetComponent<Image>().sprite = BattleManager.AllMagicCardArray[BattleManager.MagicArray[x]];
                MagicCardArray[x].transform.GetComponent<Animator>().SetBool("MagicCanUse", true);
            }
            else
            {
                MagicCardArray[x].transform.GetComponent<Animator>().SetBool("MagicCanUse", false);
            }


        }
    }

    void MoveCard() {
        for (int x = 0; x < MoveCardArray.Length ; x++) {
            if (BattleManager.PlayerMoveCount > x)
            {
                MoveCardArray[x].SetBool("MoveCard",true);
            }
            else {
                MoveCardArray[x].SetBool("MoveCard", false);
            }


        }
    }

    void DemageBuffIcon()
    {
        if (BattleManager.PlayerDamageBuff > 0)
        {
            DamegeBuffIcon.SetBool("MoveCard", true);
        }
        else
        {
            DamegeBuffIcon.SetBool("MoveCard", false);
        }

    }


    void HPCard()
    {
        for (int x = 0; x < HPCardArray.Length; x++)
        {
            if (BattleManager.PlayerHPCount > x)
            {
                HPCardArray[x].SetBool("MoveCard", true);
            }
            else
            {
                HPCardArray[x].SetBool("MoveCard", false);
            }


        }
    }
    private IEnumerator CardSelectTargetNow()
    {
        CardSelectTargetFlashes = false;
        MagicCardArray[CardSelectTarget].transform.GetComponent<Image>().enabled = false;
        yield return new WaitForSeconds(0.2f);
       MagicCardArray[CardSelectTarget].transform.GetComponent<Image>().enabled = true;
       yield return new WaitForSeconds(0.2f);
       CardSelectTargetFlashes = true;
    }

    void CardColor() { /* 換顏色 */
        int y = 0;
        for (int x = 0; x < MagicCardArray.Length; x++)
        {
            if (MagicCardDecisionArray[x] == true)
            {
                MagicCardDecisionChooseArray[y] = BattleManager.MagicArray[x];
                y += 1;
            }
        }
        if (y == 0)
        {
            for (int x = 0; x < MagicCardArray.Length; x++)
            {
                MagicCardArray[x].transform.GetComponent<Image>().color = new Color(1, 1, 1);
            }

        }else
        {
            if (MagicCardDecisionChooseArray[0] == 1)
            {
                if (y >= 2)
                {
                    for (int x = 0; x < MagicCardArray.Length; x++)
                    {
                        if (MagicCardDecisionArray[x] == false)
                        {
                            MagicCardArray[x].transform.GetComponent<Image>().color = new Color(0.5F, 0.5F, 0.5F);
                        }
                    }
                }
                else if (y == 1)
                {
                    for (int x = 0; x < MagicCardArray.Length; x++)
                    {
                        if (MagicCardDecisionArray[x] == false)
                        {
                            if (BattleManager.MagicArray[x] == 1 || BattleManager.MagicArray[x] == 2 || BattleManager.MagicArray[x] == 3)
                            {
                                MagicCardArray[x].transform.GetComponent<Image>().color = new Color(1, 1, 1);
                            }

                            else
                            {
                                MagicCardArray[x].transform.GetComponent<Image>().color = new Color(0.5F, 0.5F, 0.5F);
                            }

                        }
                    }

                }

            }
            if (MagicCardDecisionChooseArray[0] == 2)
            {
                if (y >= 2)
                {
                    for (int x = 0; x < MagicCardArray.Length; x++)
                    {
                        if (MagicCardDecisionArray[x] == false)
                        {
                            MagicCardArray[x].transform.GetComponent<Image>().color = new Color(0.5F, 0.5F, 0.5F);
                        }
                    }
                }
                else if (y == 1)
                {
                    for (int x = 0; x < MagicCardArray.Length; x++)
                    {
                        if (MagicCardDecisionArray[x] == false)
                        {
                            if (BattleManager.MagicArray[x] == 1)
                            {
                                MagicCardArray[x].transform.GetComponent<Image>().color = new Color(1, 1, 1);
                            }

                            else
                            {
                                MagicCardArray[x].transform.GetComponent<Image>().color = new Color(0.5F, 0.5F, 0.5F);
                            }

                        }
                    }

                }

            }
            if (MagicCardDecisionChooseArray[0] == 3)
            {
                if (y >= 2)
                {
                    for (int x = 0; x < MagicCardArray.Length; x++)
                    {
                        if (MagicCardDecisionArray[x] == false)
                        {
                            MagicCardArray[x].transform.GetComponent<Image>().color = new Color(0.5F, 0.5F, 0.5F);
                        }
                    }
                }
                else if (y == 1)
                {
                    for (int x = 0; x < MagicCardArray.Length; x++)
                    {
                        if (MagicCardDecisionArray[x] == false)
                        {
                            if (BattleManager.MagicArray[x] == 1)
                            {
                                MagicCardArray[x].transform.GetComponent<Image>().color = new Color(1, 1, 1);
                            }

                            else
                            {
                                MagicCardArray[x].transform.GetComponent<Image>().color = new Color(0.5F, 0.5F, 0.5F);
                            }

                        }
                    }

                }
            }
            if (MagicCardDecisionChooseArray[0] == 4)
            {
                for (int x = 0; x < MagicCardArray.Length; x++)
                {
                    if (MagicCardDecisionArray[x] == false)
                    {
                        MagicCardArray[x].transform.GetComponent<Image>().color = new Color(0.5F, 0.5F, 0.5F);
                    }
                }
            }


        }

    }
}
