using ListForEat;
using UnityEngine;
using UnityEngine.UI;

public class ItemUI : MonoBehaviour
{
    public Ingrediente data;

    public void Init(Ingrediente obj)
    {
        data = obj;
        GetComponent<Image>().sprite = obj.icon;
    }
}
