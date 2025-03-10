using UnityEngine;

public class Shape : MonoBehaviour {
    private bool additive;
    private Color highlightAdditiveColor = new Color(0.6773301f, 0.8490566f, 0.2923638f); 
    private Color highlighSubtractiveColor = new Color(0.8509804f, 0.4262924f, 0.2941177f);
    private Renderer rend;
    private VolumeInputController _volumeInputController;
    private ShapesManager _shapeManager;
    private bool selected;


    void OnEnable()
    {
        rend = GetComponent<Renderer>();
        selected = false;
        if (_volumeInputController == null)
            _volumeInputController = FindObjectOfType<VolumeInputController>(); 

        if(_shapeManager == null)
            _shapeManager = FindObjectOfType<ShapesManager>(); 

    }

    void OnTriggerEnter(Collider other)
    {
        if(_shapeManager.GetMoveableShape() != null) return;
        if(additive)
        {
            rend.material.color = highlightAdditiveColor;
        }
        else{
            rend.material.color = highlighSubtractiveColor;
        }
        selected = true;
        _shapeManager.SetMoveableShape(gameObject);
    }

    void OnTriggerExit(Collider other)
    {
        if(_shapeManager.GetMoveableShape() != gameObject) return;
        if(additive)
        {
            rend.material.color = Color.green; 
        }
        else{
            rend.material.color = Color.red;
        }
        selected = false;
        _shapeManager.RemoveSelectedShape(gameObject);
        _shapeManager.SetMoveableShape(null); 
    }
    
    public void SetAdditive(bool isAdditive) {
        rend = GetComponent<Renderer>();
        if(selected){
            if(isAdditive)
            {
                rend.material.color = highlightAdditiveColor;
            }
            else{
                rend.material.color = highlighSubtractiveColor;
            }
        }
        else {
            if(isAdditive)
            {
                rend.material.color = Color.green;
            }
            else{
                rend.material.color = Color.red;
            }
        }
        additive = isAdditive;
    }

    public void ShapeClicked() {
        if(!selected){
            if(additive)
            {
                rend.material.color = highlightAdditiveColor;
            }
            else{
                rend.material.color = highlighSubtractiveColor;
            }
            selected = true;
            _shapeManager.AddSelectedShape(gameObject);
        }
        else {
            if(additive)
            {
                rend.material.color = Color.green;
            }
            else{
                rend.material.color = Color.red;
            }
            selected = false;
            _shapeManager.RemoveSelectedShape(gameObject);
        }
        
    }

    public bool isAdditive() {
        return additive;
    }

    public void SetSelected(bool isSelected) {
        selected = isSelected;
    }


    public void DestroyShape() {
        Destroy(gameObject);
    }
}