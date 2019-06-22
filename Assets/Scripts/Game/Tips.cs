using UnityEngine;

[CreateAssetMenu]
public class Tips : ScriptableObject
{
    [SerializeField] private Sentence[] sentences;

    public string GetRandomSentence()
    {
        return sentences[UnityEngine.Random.Range(0, sentences.Length - 1)].text;
    }
}

[System.Serializable]
public class Sentence
{
    [TextArea]
    public string text;
}
