using UnityEngine;

[CreateAssetMenu(fileName = "kitchenObjectSO", menuName = "Scriptable Objects/kitchenObjectSO")]
public class KitchenObjectSO : ScriptableObject
{
    public Transform prefab;
    public Sprite sprite;
    public string objectName;
}
