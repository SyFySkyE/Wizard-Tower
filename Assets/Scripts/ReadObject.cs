using UnityEngine;

public class ReadObject : MonoBehaviour
{
    [Header("What text the player sees when looking at this object")]
    [SerializeField] private string description = "\"Dear Diary\"";

    private void Start()
    {
        this.tag = "Read Obj";
    }
    public string ReturnReadObjDescription()
    {
        return description;
    }
}
