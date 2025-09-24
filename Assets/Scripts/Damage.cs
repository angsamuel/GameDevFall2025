using UnityEngine;

public class Damage
{
    int _amount;
    public int Amount{
        get{
            return _amount;
        }
        set{
            _amount = Mathf.Max(value,0);
        }
    }


    public Damage(int amount){
        Amount = amount;
    }
}
