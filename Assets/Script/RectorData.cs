using UnityEngine;

[System.Serializable]
public class RectorData
{
    public Sprite photo;
    public string name;
    [TextArea(3, 8)]
    public string bio;
}