using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using TMPro;
public class DiceThrower : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI totalText;
    [SerializeField] private TextMeshProUGUI d4Text;
    [SerializeField] private TextMeshProUGUI d6Text;
    [SerializeField] private TextMeshProUGUI d8Text;
    [SerializeField] private TextMeshProUGUI d10Text;
    [SerializeField] private TextMeshProUGUI d12Text;
    [SerializeField] private TextMeshProUGUI d20Text;

    public List<Dice> _diceList; //Holds type of dice
    private List<GameObject> _spawnedDice = new List<GameObject>(); //list of all dice spawned in roll

    public Dice[] diceType; //Array of each die type, can be customized
    
    //public float throwForce = 15f; //if throwing
    //public float rollForce = 20f; //better for throwing
        
    int _diceTotal = 0;
    int dFour = 0;
    int dSix = 0;
    int dEight = 0;
    int dTen = 0;
    int dTwelve = 0;
    int dTwenty = 0;

    private int _stillRolling;
    
    public async void RollAllDice() //async prevents dice spawing inside each other
    {
        _diceTotal = 0;
        totalText.text = " ";
        foreach (var die in _spawnedDice) //clears dice from previous roll
        {
            Destroy(die);
            
        }
        
        _stillRolling = _diceList.Count; //tracks how many dice potentially still rolling

        foreach (Dice d in _diceList)
        {
            Dice dice = Instantiate(d, transform.position, transform.rotation); //instantiates dice game object
            _spawnedDice.Add(dice.gameObject); //adds to list of spawned dice
            dice.RollDice(); //*throwForce* Add to throw instead of drop
            dice.OnStopped += OnHalt; //call the unity action to get the total value of dice
            await Task.Yield();
        }
    }
    
    void OnHalt(int result)
    {
        _diceTotal += result;
        
        _stillRolling--;

        if (_stillRolling == 0)
        {
            Debug.Log($"Dice result {_diceTotal}");
            totalText.text = _diceTotal.ToString();
        }
    }

    public void AddDFour()
    {
        _diceList.Add(diceType[0]);
        dFour += 1;
        d4Text.text = dFour.ToString();
    }

    public void SubtractDFour()
    {
        _diceList.Remove(diceType[0]);
        dFour -= 1;
        if (dFour <= 0)
        {
            dFour = 0;
        }
         d4Text.text = dFour.ToString();
    }

    public void AddDSix()
    {
        _diceList.Add(diceType[1]);
        dSix += 1;
        d6Text.text = dSix.ToString();
    }

    public void SubtractDSix()
    {
        _diceList.Remove(diceType[1]);
        dSix -= 1;
        if (dSix <= 0)
        {
            dSix = 0;
        }
        d6Text.text = dSix.ToString();
    }

    public void AddDEight()
    {
        _diceList.Add(diceType[2]);
        dEight += 1;       
        d8Text.text = dEight.ToString();
    }

    public void SubtractDEight()
    {
        _diceList.Remove(diceType[2]);
        dEight -= 1;
        if (dEight <= 0)
        {
            dEight = 0;
        }
        d8Text.text = dEight.ToString();
    }

    public void AddDTen()
    {
        _diceList.Add(diceType[3]);
        dTen += 1;
        d10Text.text = dTen.ToString();
    }

    public void SubtractDTen()
    {
        _diceList.Remove(diceType[3]);
        dTen -= 1;
        if (dTen <= 0)
        {
            dTen = 0;
        }
        d10Text.text = dTen.ToString();
    }

    public void AddDTwelve()
    {
        _diceList.Add(diceType[4]);
        dTwelve += 1;
        d12Text.text = dTwelve.ToString();
    }

    public void SubtractDTwelve()
    {
        _diceList.Remove(diceType[4]);
        dTwelve -= 1;
        if (dTwelve <= 0)
        {
            dTwelve = 0;
        }
        d12Text.text = dTwelve.ToString();
    }

    public void AddDTwenty()
    {
        _diceList.Add(diceType[5]);
        dTwenty += 1;
        d20Text.text = dTwenty.ToString();
    }

    public void SubtractDTwenty()
    {
        _diceList.Remove(diceType[5]);
        dTwenty -= 1;
        if (dTwenty <= 0)
        {
            dTwenty = 0;
        }
        d20Text.text = dTwenty.ToString();
    }
}
