using UnityEngine;

public class CameraRaycaster : MonoBehaviour
{
    public Layer[] layerPriorities = {
        Layer.Enemy,
        Layer.Walkable
    };

    [SerializeField] private float m_distanceToBackground = 100f;
    Camera m_viewCamera;

    RaycastHit m_hit;
    public RaycastHit hit
    {
        get { return m_hit; }
    }

    Layer m_currentLayerHit;
    public Layer layerHit
    {
        get { return m_currentLayerHit; }
    }


    public delegate void OnLayerChange( Layer newLayer ); // declare delegate type.
    public event OnLayerChange OnLayerChanged; // instantiate an observer set.
    
    protected virtual void RaiseLayerChanged( Layer newLayer )
    {
        var del = OnLayerChanged as OnLayerChange;
        if( del != null )
        {
            del(newLayer);
        }
    }

    void Start() // TODO Awake?
    {
        m_viewCamera = Camera.main;
    }

    void Update()
    {
        // Look for and return priority layer hit
        foreach (Layer layer in layerPriorities)
        {
            var hit = RaycastForLayer(layer);
            if (hit.HasValue)
            {
                m_hit = hit.Value;
                if (layer != m_currentLayerHit)
                {
                    m_currentLayerHit = layer;
                    RaiseLayerChanged(layer);
                }
                m_currentLayerHit = layer;
                return;
            }
        }

        // Otherwise return background hit
        m_hit.distance = m_distanceToBackground;
        m_currentLayerHit = Layer.RaycastEndStop;
    }

    RaycastHit? RaycastForLayer(Layer layer)
    {
        int layerMask = 1 << (int)layer; // See Unity docs for mask formation
        Ray ray = m_viewCamera.ScreenPointToRay(Input.mousePosition);

        RaycastHit hit; // used as an out parameter
        bool hasHit = Physics.Raycast(ray, out hit, m_distanceToBackground, layerMask);
        if (hasHit)
        {
            return hit;
        }
        return null;
    }
}
