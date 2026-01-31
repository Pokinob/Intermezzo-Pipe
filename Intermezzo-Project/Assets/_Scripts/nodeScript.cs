using NUnit.Framework.Constraints;
using System.Collections;
using UnityEngine;

public class nodeScript : MonoBehaviour
{
    [SerializeField]
    private bool once=false;
    [SerializeField]
    private detectorScript dScript;
    public powerSource source;
    public int value = -1;

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.tag == "pipe"|| collision.tag == "detector" || collision.tag == "Target" || once) return;

        powerSource ps = collision.GetComponent<powerSource>();
        if(collision.tag == "Power" && ps != null)
        {
            //Debug.Log("Power" + " " + collision.tag);
            source = ps;
            value = 1;
            once = true;
        }
        else
        {
            detectorScript other = collision.GetComponentInParent<detectorScript>();
            if(other == null) return;
            if (other.value == -1 && source == null)
            {
                value = -1;
                source = null;
                once = false;
            }
            else
            {
                value = other.value + 1;
                source = other.source;
                once = true;
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "pipe"||collision.tag == "detector") return;
        //Debug.Log("exit");
        if(collision.tag == "Power" && value==1)
        {
            value = -1;
            source = null;
            once = false;
            return;
        }
        detectorScript other = collision.GetComponentInParent<detectorScript>();
        if(other == null) return;
        value = -1;
        source = null;
        once = false;
        if (other.value < value) dScript._reset();
    }

    public void _Resetvalue()
    {
        value = -1;
        source = null;
        once = false;
    }

}
