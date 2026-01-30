using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class detectorScript : MonoBehaviour
{
    [SerializeField]
    private string tagObj = "";
    public int value = -1;
    powerSource Source = null;
    private void OnTriggerEnter2D(Collider2D col)
    {
        if (value != -1) return;
        powerSource ps = col.GetComponent<powerSource>();
        if (col.tag == "Power")
        {
            tagObj = col.tag;
            Source = ps;
            value = 1;
            return;
        }
        //source dari pipa lain
        detectorScript other = col.GetComponent<detectorScript>();
        if (other == null) return;
        if (other != null && Source == null && other.value != -1 && value == -1) 
        {
            Source = other.Source;
            value = other.value + 1;
        }
        else
        {
            int _value= other.value;
            if(_value < value + 1)
            { 
                Source = null;
                value = -1;
                return;
            }
            Source = other.Source;
            value = other.value + 1;
        }


    }
    private void OnTriggerExit2D(Collider2D col)
    {
        if (value != -1 && (tagObj == "Power" && col.tag != "Power")) return;
        Source = null;
        value = -1;
    }

}
