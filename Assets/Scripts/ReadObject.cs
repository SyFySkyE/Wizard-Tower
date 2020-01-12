using UnityEngine;

public class ReadObject : MonoBehaviour
{
    [Header("What text the player sees when looking at this object")]
    [SerializeField] private string description = "\"Dear Diary\"";

    public string ReturnReadObjDescription()
    {
        return description;
    }
}
