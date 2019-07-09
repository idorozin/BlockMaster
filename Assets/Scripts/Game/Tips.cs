using UnityEngine;

[CreateAssetMenu]
public class Tips : ScriptableObject
{
    [SerializeField] private Sentence[] sentences;

    public string GetRandomSentence()
    {
        if(sentences.Length != 0)
            return sentences[UnityEngine.Random.Range(0, sentences.Length - 1)].text;
        return null;
    }
}

[System.Serializable]
public class Sentence
{
    [TextArea]
    public string text;
}
