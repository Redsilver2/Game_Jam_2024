using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ListHolder : MonoBehaviour
{
    [SerializeField] private List<GameObject> vegetableList;
    // Start is called before the first frame update
    public void AddVegetable(GameObject vegetable)
    {
        vegetableList.Add(vegetable);
        Debug.Log("This vegetable has been added to the list : " + vegetable.name);

        foreach (var item in vegetableList)
        {
            Debug.Log("Current List : " + item.name);
        }
    }
}
