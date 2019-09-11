using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.UIElements.StyleEnums;

public class Tutorial : MonoBehaviour
{
    [SerializeField] private Transform lava;
    [SerializeField]
    private List<Wraper> popUps;

    private int popUpIndex = 0;

    public static bool closeTutorial = false;


    private bool activateLives;

    [System.Serializable]
    public class Wraper
    {
        public List<PopUp> list;
        
    }

    [SerializeField]
    private SceneTransition transition;
    
    void Start()
    {
        Time.timeScale = 0f;
        PauseMenu.GameIsPaused = true;
        UpdatePopUp();
        ShapeBehaviour.ShapeFell += ShapeBehaviourOnShapeFell;
        closeBtn.SetActive(closeTutorial);
    }

    [SerializeField] private GameObject closeBtn;

    public void Close()
    {
        transition.FadeOutAndLoadScene("MainMenu");
    }

    private void ShapeBehaviourOnShapeFell()
    {      
        if(!activateLives)
            GameManager.Instance.lives++;
    }

    private void UpdatePopUp()
    {
        Wraper prevPopUps;
        if (popUpIndex > 0)
        {
            prevPopUps = popUps[popUpIndex - 1];
            foreach (var popUp in prevPopUps.list)
            {
                if(!popUp.stay)
                    popUp.obj.SetActive(false);
            }
        }
        if(popUpIndex >= popUps.Count)
            return;
        List<PopUp> nextPopUps = popUps[popUpIndex].list;
        foreach (var popUp in nextPopUps)
        {
            popUp.obj.SetActive(true);
        }
        popUpIndex++;
    }

    void Update()
    {
        switch (popUpIndex)
        {
            case 1 :
                if(PlayerInput.FingerMoved > 30)
                    UpdatePopUp();
                break;
            case 2 :
                if(PlayerInput.Shoots > 2)
                    UpdatePopUp();
                break;
            case 3 :
                if (PlayerInput.Shoots > 4)
                {
                    activateLives = true;
                    UpdatePopUp();
                }
                break;
            case 4 :
                if (PlayerInput.Shoots > 6)
                {
                    UpdatePopUp();
                }
                break;
        }
        
    }
}
[System.Serializable]
public class PopUp
{
    public GameObject obj;
    public bool stay = false;
}
