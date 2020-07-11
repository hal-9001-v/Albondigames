using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

//El siguiente Script debe aplicarse a un GameObject camera,
//de no ser así el componente será eliminado.

public class CameraController : MonoBehaviour
{
    //Visible en el editor de Unity.
    [System.Serializable]
    public struct Limits
    {
        public bool useVerticalLimits;
        public bool useHorizontalLimits;
        public float top;
        public float right;
        public float left;
        public float bottom;
    }

    public GameObject playerFollowed;
    public Limits limits;

    public UnityEvent inTransition;

    private Camera camera;
    private bool followingPlayer;
    private float startingSize;
    private float aspectRatio;
    
    void Awake()
    {   //Nada más emplearse se comprueba si es viable el GameObject asociado.
        camera = GetComponent<Camera>();
        if (!camera) { Destroy(this); }
    }

    void Start()
    {   //Inicializacion.
        followingPlayer = playerFollowed ? true : false;
        startingSize = camera.orthographicSize;
        aspectRatio = (float) camera.pixelWidth / camera.pixelHeight;
        if (inTransition == null)
            inTransition = new UnityEvent();
    }

    // Update is called once per frame
    void Update()
    {
        //Si la cámara está siguiendo el personaje.
        if (followingPlayer)
        {
            transform.position = new Vector3(playerFollowed.transform.position.x,
                playerFollowed.transform.position.y, transform.position.z);
        }

        //Ajustamos la cámara a los límites.
        fitToLimits();

    }
    
    //Getters y Setters
    public bool doFollowPlayer(bool follow)
    {
        if (playerFollowed)
        {
            followingPlayer = follow;
            return true;
        }
        return false;
    }

    public bool setLimits(Limits lim)
    {
        //Falla en caso de emplear límites contradictorios.
        if (((lim.right - lim.left) < 0) || ((lim.top - lim.bottom) < 0)) return false;

        limits = lim;
        return true;
    }

    public bool setZoom(float zoom)
    {
        if (zoom <= 0) return false;
        camera.orthographicSize = startingSize * (1 / zoom);
        return true;
    }

    //Transiciones
    public void transitionToBlack(float seconds)
    {
        RawImage canvas = GameObject.Find("Transiciones").GetComponentInChildren<RawImage>();
        canvas.color = new Color(0, 0, 0, 0);
        StartCoroutine(startTransition(seconds, canvas));
    }
    public void transitionToWhite(float seconds)
    {
        RawImage canvas = GameObject.Find("Transiciones").GetComponentInChildren<RawImage>();
        canvas.color = new Color(1, 1, 1, 0);
        StartCoroutine(startTransition(seconds, canvas));
    }

    IEnumerator startTransition(float seconds, RawImage canvas)
    {
        while (canvas.color.a < 1)
        {
            canvas.color = new Color(canvas.color.r, canvas.color.g, canvas.color.b,
                canvas.color.a + seconds * 0.5f * Time.deltaTime);
            yield return new WaitForSeconds(Time.deltaTime);
        }
        inTransition.Invoke();
        yield return new WaitForSeconds(Time.deltaTime);
        StartCoroutine(endTransition(seconds, canvas));
    }
    IEnumerator endTransition(float seconds, RawImage canvas)
    {
        while (canvas.color.a > 0)
        {
            canvas.color = new Color(canvas.color.r, canvas.color.g, canvas.color.b,
                canvas.color.a - seconds * 0.5f * Time.deltaTime);
            yield return new WaitForSeconds(Time.deltaTime);
        }
    }

    //Privados.
    private void fitToLimits()
    {
        //Ajustamos la camara para no salirse de los límites.
        if (limits.useHorizontalLimits)
        {
            //Derecha.
            if (camera.transform.position.x + camera.orthographicSize * aspectRatio > limits.right)
                camera.transform.position = new Vector3(limits.right - camera.orthographicSize * aspectRatio,
                    camera.transform.position.y, camera.transform.position.z);

            //Izquierda.
            if (camera.transform.position.x - camera.orthographicSize * aspectRatio < limits.left)
                camera.transform.position = new Vector3(limits.left + camera.orthographicSize * aspectRatio,
                    camera.transform.position.y, camera.transform.position.z);
        }

        if (limits.useVerticalLimits)
        {
            //Arriba.
            if (camera.transform.position.y + camera.orthographicSize > limits.top)
                camera.transform.position = new Vector3(camera.transform.position.x,
                    limits.top - camera.orthographicSize, camera.transform.position.z);
            //Abajo.
            if (camera.transform.position.y - camera.orthographicSize < limits.bottom)
                camera.transform.position = new Vector3(camera.transform.position.x,
                    limits.bottom + camera.orthographicSize, camera.transform.position.z);
        }
    }
}
