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
        public float bottom;
        public float right;
        public float left;
    }

    //Modo normal.
    //Prefab del canvas.
    public GameObject transitionCanvas;
    //Instancia del jugador.
    public GameObject playerFollowed;
    public bool followingPlayer;
    public Vector2 relativeFollowing;

    public Limits limits;
    public UnityEvent inTransition;

    private new Camera camera;
    private float startingSize;
    private float aspectRatio;

    //Modo cinemático.
    public CameraNode[] nodes;
    public CameraZoomNode[] zoomNodes;

    private Queue<CameraNode> nodeQueue;
    private Queue<CameraZoomNode> zoomNodeQueue;
    private CameraNode currentNode;
    private CameraZoomNode currentZoomNode;
    private Vector3 destination;
    private Vector3 startPosition;
    private float startZoom;
    private float currentZoom;
    private float destinationZoom;
    private bool arrived;
    private bool transition;
    private bool usingNode;
    private bool usingZoomNode;
    private float timeToReachTarget;
    private float timeCounter;

    void Awake()
    {   //Nada más emplearse se comprueba si es viable el GameObject asociado.
        camera = GetComponent<Camera>();
        if (!camera) { Destroy(this); }
    }

    void Start()
    {   //Inicializacion.
        startingSize = camera.orthographicSize;
        aspectRatio = (float) camera.pixelWidth / camera.pixelHeight;
        if (inTransition == null)
            inTransition = new UnityEvent();

        nodeQueue = new Queue<CameraNode>();
        foreach (CameraNode node in nodes)
        {
            nodeQueue.Enqueue(node);
        }
        zoomNodeQueue = new Queue<CameraZoomNode>();
        foreach (CameraZoomNode node in zoomNodes)
        {
            zoomNodeQueue.Enqueue(node);
        }
        arrived = true;
        usingNode = false;
        usingZoomNode = false;
        transition = false;
        currentZoom = 1;
    }

    private void FixedUpdate()
    {
        if (!arrived && !transition)
        {
            timeCounter += Time.deltaTime / timeToReachTarget;
            if (!usingZoomNode)
            {
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
            else
            {
                if (Mathf.Abs(destinationZoom - currentZoom) < Time.deltaTime / timeToReachTarget)
                {
                    SetZoom(destinationZoom);
                    StartCoroutine(NodeZoomTransition());
                }
                else
                {
                    currentZoom = Mathf.Lerp(startZoom, destinationZoom, timeCounter);
                    SetZoom(currentZoom);
                }
            }
        }
    }
    // Update is called once per frame
    void Update()
    {
        //Para que se siga correctamente al personaje, su movimiento
        //deberá ser realizado mediante FixedUpdate (físicas).
        if (followingPlayer && (arrived || usingZoomNode))
        {
            transform.position = new Vector3(playerFollowed.transform.position.x + relativeFollowing.x,
                playerFollowed.transform.position.y + relativeFollowing.y, transform.position.z);

            //Ajustamos la cámara a los límites.
            camera.transform.position = FitToLimits(camera.transform.position);
        }
    }
    
    //Getters y Setters
    public void DoFollowPlayer(float seconds)
    {
        if (arrived && playerFollowed && !followingPlayer)
        {
            followingPlayer = true;
            if (seconds > 0)
            {
                arrived = false;
                timeCounter = 0;
                timeToReachTarget = seconds;
                startPosition = transform.position;
                destination = new Vector3(playerFollowed.transform.position.x + relativeFollowing.x,
                    playerFollowed.transform.position.y + relativeFollowing.y, startPosition.z);
            }
        }
    }

    public void DoNotFollowPlayer()
    {
        if (arrived && playerFollowed && followingPlayer)
        {
            followingPlayer = false;
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
    public void SetZoom(float zoom)
    {
        if (zoom > 0)
        {
            camera.orthographicSize = startingSize * (1 / zoom);
            currentZoom = zoom;
        }
    }

    public void SetRelativeFollowing(Vector2 rel)
    {
        relativeFollowing = rel;
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

    //MODO CINEMÁTICO.
    //Desactiva el seguimiento de la cámara y realiza una transición
    //a una posición dada en los segundos indicados en el nodo.
    //Devuelve true si ha podido ejecutarse, false en caso contrario.
    public void GoToNextNode()
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
        }
    }

    public void GoToNextZoomNode()
    {
        if (arrived && zoomNodeQueue.Count != 0)
        {
            arrived = false;
            usingZoomNode = true;
            currentZoomNode = zoomNodeQueue.Dequeue();

            timeCounter = 0;
            timeToReachTarget = currentZoomNode.timeToGet;
            startZoom = currentZoom;
            destinationZoom = currentZoomNode.zoom;

            currentZoomNode.atStartEvent.Invoke();
        }
    }

    //Comprueba si la cámara está disponible para moverse.
    public bool HasArrived()
    {
        return arrived;
    }

    //Añade un nodo a las transiciones de la cámara.
    public void AddNode(CameraNode node)
    {
        nodeQueue.Enqueue(node);
    }

    public void AddZoomNode(CameraZoomNode node)
    {
        zoomNodeQueue.Enqueue(node);
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

    IEnumerator NodeTransition()
    {
        //En espera
        transition = true;
        currentNode.delayEvent.Invoke();
        yield return new WaitForSeconds(currentNode.delay);
        
        //Finalizar nodo.
        arrived = true;
        usingNode = false;
        transition = false;
        currentNode.atEndEvent.Invoke();
    }

    IEnumerator NodeZoomTransition()
    {
        //En espera
        transition = true;
        currentZoomNode.delayEvent.Invoke();
        yield return new WaitForSeconds(currentZoomNode.delay);

        //Finalizar nodo.
        arrived = true;
        usingZoomNode = false;
        transition = false;
        currentZoomNode.atEndEvent.Invoke();
    }


}
