using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Planet", menuName = "ScriptableObjects/SciencePuzzle")]
public class SciencePuzzle : ScriptableObject
{
    public string PuzzleName;
    public string Description;
    public Sprite Icon;
}
