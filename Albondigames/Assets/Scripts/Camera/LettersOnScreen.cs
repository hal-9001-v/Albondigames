using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LettersOnScreen : MonoBehaviour
{
    public float time;
    public GameObject canvasObject;
    private GameObject canvas;
    private SpriteRenderer A;
    private SpriteRenderer A_Press;
    private SpriteRenderer CTRL;
    private SpriteRenderer CTRL_Press;
    private SpriteRenderer D;
    private SpriteRenderer D_Press;
    private SpriteRenderer WASD;
    private SpriteRenderer WASJ;
    private SpriteRenderer E;
    private SpriteRenderer E_Press;
    private SpriteRenderer SPACE;
    private SpriteRenderer SPACE_Press;
    private SpriteRenderer SHIFT;
    private SpriteRenderer SHIFT_Press;
    private SpriteRenderer Z;
    private SpriteRenderer Z_Press;

    private bool changing = false;
    private bool state = false;
    private float currentTime = 0;
    private bool AEnabled = false;
    private bool CTRLEnabled = false;
    private bool DEnabled = false;
    private bool EEnabled = false;
    private bool SPACEEnabled = false;
    private bool SHIFTEnabled = false;
    private bool ZEnabled = false;


     private void Awake() {
        
    
    
        canvas = Instantiate(canvasObject, new Vector3(), Quaternion.identity);
        A = GameObject.Find("A").GetComponent<SpriteRenderer>();
        A_Press = GameObject.Find("A_Press").GetComponent<SpriteRenderer>();
        CTRL = GameObject.Find("CTRL").GetComponent<SpriteRenderer>();
        CTRL_Press = GameObject.Find("CTRL_Press").GetComponent<SpriteRenderer>();
        D = GameObject.Find("D").GetComponent<SpriteRenderer>();
        D_Press = GameObject.Find("D_Press").GetComponent<SpriteRenderer>();
        WASD = GameObject.Find("WASD").GetComponent<SpriteRenderer>();
        WASJ = GameObject.Find("WASJ").GetComponent<SpriteRenderer>();
        E = GameObject.Find("E").GetComponent<SpriteRenderer>();
        E_Press = GameObject.Find("E_Press").GetComponent<SpriteRenderer>();
        SPACE = GameObject.Find("SPACE").GetComponent<SpriteRenderer>();
        SPACE_Press = GameObject.Find("SPACE_Press").GetComponent<SpriteRenderer>();
        SHIFT = GameObject.Find("SHIFT").GetComponent<SpriteRenderer>();
        SHIFT_Press = GameObject.Find("SHIFT_Press").GetComponent<SpriteRenderer>();
        Z = GameObject.Find("Z").GetComponent<SpriteRenderer>();
        Z_Press = GameObject.Find("Z_Press").GetComponent<SpriteRenderer>();
        A.enabled = false;
        A_Press.enabled = false;
        CTRL.enabled = false;
        CTRL_Press.enabled = false;
        D.enabled = false;
        D_Press.enabled = false;
        WASD.enabled = false;
        WASJ.enabled = false;
        E.enabled = false;
        E_Press.enabled = false;
        SPACE.enabled = false;
        SPACE_Press.enabled = false;
        SHIFT.enabled = false;
        SHIFT_Press.enabled = false;
        Z.enabled = false;
        Z_Press.enabled = false;

    }

    
    // Update is called once per frame
    void Update()
    {
        
        if (!canvas.GetComponent<Canvas>().worldCamera)
        {
            canvas.GetComponent<Canvas>().worldCamera = Camera.main;
            canvas.GetComponent<Canvas>().sortingLayerName = "GUI";
        }

        if (changing)
        {
            currentTime += Time.deltaTime;
            if(currentTime >= time)
            {
                state = !state;
                currentTime = 0;
            }
            if (state)
            {
                if (AEnabled)
                {
                    A.enabled = true;
                    A_Press.enabled = false;
                }

                if (CTRLEnabled)
                {
                    CTRL.enabled = true;
                    CTRL_Press.enabled = false;
                }

                if (DEnabled)
                {
                    D.enabled = true;
                    D_Press.enabled = false;
                }

                if (EEnabled)
                {
                    E.enabled = true;
                    E_Press.enabled = false;
                }

                if (SPACEEnabled)
                {
                    SPACE.enabled = true;
                    SPACE_Press.enabled = false;
                }

                if (SHIFTEnabled)
                {
                    SHIFT.enabled = true;
                    SHIFT_Press.enabled = false;
                }
                 if (ZEnabled)
                {
                    Z.enabled = true;
                    Z_Press.enabled = false;
                }
            }
            else
            {
                if (AEnabled)
                {
                    A.enabled = false;
                    A_Press.enabled = true;
                }

                if (CTRLEnabled)
                {
                    CTRL.enabled = false;
                    CTRL_Press.enabled = true;
                }

                if (DEnabled)
                {
                    D.enabled = false;
                    D_Press.enabled = true;
                }

                if (EEnabled)
                {
                    E.enabled = false;
                    E_Press.enabled = true;
                }

                if (SPACEEnabled)
                {
                    SPACE.enabled = false;
                    SPACE_Press.enabled = true;
                }

                if (SHIFTEnabled)
                {
                    SHIFT.enabled = false;
                    SHIFT_Press.enabled = true;
                }
                if (ZEnabled)
                {
                    Z.enabled = false;
                    Z_Press.enabled = true;
                }
            }
        }
    }

    public void showA()
    {
        changing = true;
        AEnabled = true;
    }

    public void hideA()
    {
        changing = false;
        AEnabled = false;
        A.enabled = false;
        A_Press.enabled = false;
    }

    public void showCTRL()
    {
        changing = true;
        CTRLEnabled = true;
    }

    public void hideCTRL()
    {
        changing = false;
        CTRLEnabled = false;
        CTRL.enabled = false;
        CTRL_Press.enabled = false;
    }

    public void showD()
    {
        changing = true;
        DEnabled = true;
    }

    public void hideD()
    {
        changing = false;
        DEnabled = false;
        D.enabled = false;
        D_Press.enabled = false;
    }

    public void showE()
    {
        changing = true;
        EEnabled = true;
    }

    public void hideE()
    {
        changing = false;
        EEnabled = false;
        E.enabled = false;
        E_Press.enabled = false;
    }

    public void showSPACE()
    {
        changing = true;
        SPACEEnabled = true;
    }

    public void hideSPACE()
    {
        changing = false;
        SPACEEnabled = false;
        SPACE.enabled = false;
        SPACE_Press.enabled = false;
    }

    public void showSHIFT()
    {
        changing = true;
        SHIFTEnabled = true;
    }

    

    public void hideSHIFT()
    {
        changing = false;
        SHIFTEnabled = false;
        SHIFT.enabled = false;
        SHIFT_Press.enabled = false;
    }
     public void showZ()
    {
        changing = true;
        ZEnabled = true;
    }

     public void hideZ()
    {
        changing = false;
        ZEnabled = false;
        Z.enabled = false;
        Z_Press.enabled = false;
    }

    public void showWASD()
    {
        WASD.enabled = true;
        WASD.enabled = true;
    }

    public void hideWASD()
    {
        WASD.enabled = false;
        WASD.enabled = false;
    }

    public void showWASJ()
    {
        WASJ.enabled = true;
        WASJ.enabled = true;
    }

    public void hideWASJ()
    {
        WASJ.enabled = false;
        WASJ.enabled = false;
    }

    public void changeTime(float times)
    {
        time = times;
    }
}
