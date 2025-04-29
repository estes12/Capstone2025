using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Events;
using Random = UnityEngine.Random;

[RequireComponent(typeof(Rigidbody))]
public class Dice : MonoBehaviour
{
    public Transform[] diceFaces; //set transforms for dice faces so dice can have any amount of sides.
    public Rigidbody rb; //dice rigidbody

    public UnityAction<int> OnStopped; //

    private bool _stoppedRolling;
    private bool _delayFinished; //gives a second to determine movement

    public static int faceValue; //which transform in the array faces up

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        if (!_delayFinished) return; //check if delay is finished
        if (!_stoppedRolling && rb.IsSleeping()) //check if die has stopped 
        { 
            int result = GetSideUp();
            OnStopped?.Invoke(result);
        }
    }

    public int GetSideUp()
    {
        if (diceFaces == null) return -1;

        int topFace = 0; //sets to first element in array
        var lastYPosition = diceFaces[0].position.y; //y space is higher than other numbers/faces upwards, sets dice to face up at 1

        for (int i = 0; i < diceFaces.Length; i++) //
        {
            if (diceFaces[i].position.y > lastYPosition) //lastYPosition = 1, so else would be 1
            {
                lastYPosition = diceFaces[i].position.y;
                topFace = i;
            }
        }
        int upSide = topFace + 1; //turning variable into int
        
        _stoppedRolling = true ;//make sure it only gets called once
        Debug.Log($"Dice result {upSide}");
        return upSide;//topFace is element in array, +1 gives actual value

    }

    public void RollDice() //*float throwForce,* Add to throw instead of drop
    {
        var randomVariance = Random.Range(-1f, 1f); //allows dice rolls to be random each time
        //rb.AddForce(transform.forward * (throwForce + randomVariance), ForceMode.Impulse); //to throw dice vs drop dice

        var randX = Random.Range(0f, 1f); //roll x value
        var randY = Random.Range(0f, 1f); //roll y value
        var randZ = Random.Range(0f, 1f); //roll z value

        rb.AddTorque(new Vector3(randX, randY, randZ) * (20f + randomVariance), ForceMode.Impulse); //rolls dice when thrown or dropped

        _stoppedRolling = false;

        DelayResult();
    }

    private async void DelayResult()
    {
        await Task.Delay(1000);
        _delayFinished = true;
    }
}
