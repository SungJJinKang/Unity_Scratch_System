using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public abstract class ElementOfBlockUnit : BlockEdidtorElement
{
    [SerializeField]
    public BlockEditorUnit OwnerBlockUnit
    {
        protected get;
        set;
    }

    // Start is called before the first frame update
    protected virtual void Start()
    {
        
    }

    // Update is called once per frame
    protected virtual void Update()
    {
        
    }

    protected virtual void OnEnable()
    {

    }

    protected virtual void OnDisable()
    {

    }

    public virtual void SetElementContent(ElementContent elementContent)
    {

    }
}
