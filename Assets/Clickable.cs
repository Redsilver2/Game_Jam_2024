using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Clickable : MonoBehaviour
{
    // Start is called before the first frame update

    private ListHolder list;
    void Start()
    {
        list = GameObject.FindGameObjectWithTag("Holder").GetComponent<ListHolder>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnMouseDown()
    {
        Debug.Log("You clicked this gameObject : " + this.gameObject.name);
        list.AddVegetable(this.gameObject);
        //Destroy(this.gameObject);
    }
}
