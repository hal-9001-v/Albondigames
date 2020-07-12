using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEditorInternal;
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
        public float bottom;
        public float right;
        public float left;
    }

    //Modo normal.
    //Prefab del canvas.
    public GameObject transitionCanvas;
    //Instancia del jugador.
    public GameObject playerFollowed;
    public Vector2 relativeFollowing;

    public Limits limits;
    public UnityEvent inTransition;

    private Camera camera;
    private bool followingPlayer;
    private float startingSize;
    private float aspectRatio;

    //Modo cinemático.
    public CameraNode[] nodes;

    private Queue<CameraNode> nodeQueue;
    private CameraNode currentNode;
    private Vector3 destination;
    private Vector3 startPosition;
    private bool arrived;
    private bool usingNode;
    private float timeToReachTarget;
    private float timeCounter;

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

        nodeQueue = new Queue<CameraNode>();
        foreach (CameraNode node in nodes)
        {
            nodeQueue.Enqueue(node);
        }
        arrived = true;
        usingNode = false;
    }

    private void FixedUpdate()
    {
        if (!arrived)
        {
            timeCounter += Time.deltaTime / timeToReachTarget;
            if (Vector3.Distance(destination, transform.position) < 0.05)
            {
                transform.position = destination;
                if (usingNode) StartCoroutine(NodeTransition());
                else arrived = true;
            }
            else
            {
                transform.position = Vector3.Lerp(startPosition, destination, timeCounter);
                if (followingPlayer)
                {
                    destination = new Vector3(playerFollowed.transform.position.x + relativeFollowing.x,
                        playerFollowed.transform.position.y + relativeFollowing.y, startPosition.z);
                }
                destination = FitToLimits(destination);
            }
        }
    }
    // Update is called once per frame
    void Update()
    {
        //Para que se siga correctamente al personaje, su movimiento
        //deberá ser realizado mediante FixedUpdate (físicas).
        if (followingPlayer && arrived)
        {
            transform.position = new Vector3(playerFollowed.transform.position.x + relativeFollowing.x,
                playerFollowed.transform.position.y + relativeFollowing.y, transform.position.z);

            //Ajustamos la cámara a los límites.
            camera.transform.position = FitToLimits(camera.transform.position);
        }
    }
    
    //Getters y Setters
    public bool DoFollowPlayer(bool follow, float seconds)
    {
        if (arrived)
        {
            if (playerFollowed)
            {
                if (!followingPlayer && follow)
                {
                    if (seconds <= 0)
                    {
                        followingPlayer = follow;
                    }
                    else
                    {
                        followingPlayer = true;
                        arrived = false;
                        timeCounter = 0;
                        timeToReachTarget = seconds;
                        startPosition = transform.position;
                        destination = new Vector3(playerFollowed.transform.position.x + relativeFollowing.x,
                            playerFollowed.transform.position.y + relativeFollowing.y, startPosition.z);
                    }
                    return true;
                }
                else
                {
                    followingPlayer = follow;
                    return true;
                }
            }
            return false;
        }
        else
        {
            return false;
        }
    }

    //Deja de seguir al personaje y mueve la cámara a cierto punto.
    public bool MoveCamera(Vector2 pos, float seconds)
    {
        if (arrived)
        {
            followingPlayer = false;
            arrived = false;
            timeCounter = 0;
            timeToReachTarget = seconds;
            startPosition = transform.position;
            destination = new Vector3(pos.x, pos.y, startPosition.z);
            return true;
        }
        else
        {
            return false;
        }
    }

    //Establece los límites del following de la cámara.
    public bool SetLimits(Limits lim)
    {
        //Falla en caso de emplear límites contradictorios.
        if (((lim.right - lim.left) < 0) || ((lim.top - lim.bottom) < 0)) return false;

        limits = lim;
        return true;
    }

    //Establece un Zoom a la cámara (valores de 0 a 1 alejan el objetivo).
    public bool SetZoom(float zoom)
    {
        if (zoom <= 0) return false;
        camera.orthographicSize = startingSize * (1 / zoom);
        return true;
    }

    //TRANSICIONES
    //Transición a negro.
    public void TransitionToBlack(float seconds)
    {
        if (transitionCanvas)
        {
            GameObject canvas = Instantiate(transitionCanvas, new Vector3(), Quaternion.identity);
            canvas.GetComponent<Canvas>().worldCamera = Camera.current;
            RawImage image = canvas.GetComponentInChildren<RawImage>();
            image.color = new Color(0, 0, 0, 0);
            StartCoroutine(StartTransition(seconds, canvas, image));
        }
    }
    //Transición a blanco.
    public void TransitionToWhite(float seconds)
    {
        if (transitionCanvas)
        {
            GameObject canvas = Instantiate(transitionCanvas, new Vector3(), Quaternion.identity);
            canvas.GetComponent<Canvas>().worldCamera = Camera.current;
            RawImage image = canvas.GetComponentInChildren<RawImage>();
            image.color = new Color(1, 1, 1, 0);
            StartCoroutine(StartTransition(seconds, canvas, image));
        }
    }

    IEnumerator StartTransition(float seconds, GameObject canvas, RawImage image)
    {
        while (image.color.a < 1)
        {
            image.color = new Color(image.color.r, image.color.g, image.color.b,
                image.color.a + seconds * 0.5f * Time.deltaTime);
            yield return new WaitForSeconds(Time.deltaTime);
        }
        inTransition.Invoke();
        yield return new WaitForSeconds(Time.deltaTime);
        StartCoroutine(EndTransition(seconds, canvas, image));
    }
    IEnumerator EndTransition(float seconds, GameObject canvas, RawImage image)
    {
        while (image.color.a > 0)
        {
            image.color = new Color(image.color.r, image.color.g, image.color.b,
                image.color.a - seconds * 0.5f * Time.deltaTime);
            yield return new WaitForSeconds(Time.deltaTime);
        }
        Destroy(canvas);
    }

    //EVENTO DE TRANSICIONES.
    //Establece evento de transicion.
    public void setTransition(UnityAction action)
    {
        inTransition.RemoveAllListeners();
        inTransition.AddListener(action);
    }

    //Añade evento de transicion.
    public void addTransition(UnityAction action)
    {
        inTransition.AddListener(action);
    }

    //Elimina eventos de transicion.
    public void removeTransition(UnityAction action)
    {
        inTransition.RemoveAllListeners();
    }

    //Ajusta un Vector3 de posición a unos límites relativos a la cámara.
    private Vector3 FitToLimits(Vector3 pos)
    {
        Vector3 resultado = new Vector3();
        //Ajustamos la camara para no salirse de los límites.
        if (limits.useHorizontalLimits)
        {

            //Izquierda.
            if (pos.x - camera.orthographicSize * aspectRatio < limits.left)
                resultado.x = limits.left + camera.orthographicSize * aspectRatio;

            //Derecha.
            else if (pos.x + camera.orthographicSize * aspectRatio > limits.right)
                resultado.x = limits.right - camera.orthographicSize * aspectRatio;

            //No se exceden los topes.
            else resultado.x = pos.x;
        }
        else
        {
            resultado.x = pos.x;
        }

        if (limits.useVerticalLimits)
        {
            //Abajo.
            if (pos.y - camera.orthographicSize < limits.bottom)
                resultado.y = limits.bottom + camera.orthographicSize;

            //Arriba.
            else if (pos.y + camera.orthographicSize > limits.top)
                resultado.y = limits.top - camera.orthographicSize;
            
            else resultado.y = pos.y;
        }
        else
        {
            resultado.y = pos.y;
        }
        resultado.z = pos.z;
        return resultado;
    }

    //MODO CINEMÁTICO.
    //Desactiva el seguimiento de la cámara y realiza una transición
    //a una posición dada en los segundos indicados en el nodo.
    //Devuelve true si ha podido ejecutarse, false en caso contrario.
    public bool GoToNextNode()
    {
        if (arrived && nodeQueue.Count != 0)
        {
            followingPlayer = false;
            arrived = false;
            usingNode = true;
            currentNode = nodeQueue.Dequeue();

            timeCounter = 0;
            timeToReachTarget = currentNode.timeToGet;
            startPosition = transform.position;
            destination = currentNode.transform.position;

            currentNode.atStartEvent.Invoke();
            return true;
        }
        return false;
    }

    //Comprueba si la cámara está disponible para moverse.
    public bool HasArrived()
    {
        return arrived;
    }

    //Añade un nodo a las transiciones de la cámara.
    public void addNode(CameraNode node)
    {
        nodeQueue.Enqueue(node);
    }

    IEnumerator NodeTransition()
    {
        //En espera
        currentNode.delayEvent.Invoke();
        yield return new WaitForSeconds(currentNode.delay);

        //Finalizar nodo.
        currentNode.atEndEvent.Invoke();
        arrived = true;
        usingNode = false;
    }
}
