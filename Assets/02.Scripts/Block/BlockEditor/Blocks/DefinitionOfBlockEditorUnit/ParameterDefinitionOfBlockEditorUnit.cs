using System;
using UnityEngine;

[System.Serializable]
public abstract class ParameterDefinitionOfBlockEditorUnit : DefinitionOfBlockEditorUnit
{
    private int parameterIndex;
    public int ParameterIndex
    {
        private get
        {
            return this.parameterIndex;
        }
        set
        {
            if(value == -1)
            {
                this.parameterIndex = value;
                Debug.Log("Reset Parameter Indexx");
                return;
            }

            if(value < 0 || value > 3)
            {
                Debug.LogError("Improper parameterIndex");
                return;
            }

            Type parameterType = base.OwnerBlockEditorUnit.TargetBlock.ParametersTypes[value];

            if (parameterType == null || ( parameterType != this.TargetParameterBlockType && parameterType.IsSubclassOf(TargetParameterBlockType) == false) )
            {
                Debug.LogError(base.OwnerBlockEditorUnit.TargetBlock.GetType().Name +  " Fail to Set ParameterIndex ( Different Parameter Type ).     ParameterDefinitionOfBlockEditorUnit : " + TargetParameterBlockType.Name + "    ParameterIndex " + value.ToString() + "  Block Type : " + parameterType?.Name);
                return;
            }

            this.parameterIndex = value;

            //Spawn ValueBlock
            if(base.OwnerBlockEditorUnit.TargetBlock.TryGetParameterValue(this.parameterIndex, out ValueBlock parameterValueBlock) == true)
            {
                this.PassedParameterValueBlock = parameterValueBlock;
            }
        }
    }

    protected abstract Type TargetParameterBlockType { get; }

    protected abstract ValueBlock PassedParameterValueBlock { set; get; }

   

    protected void PassParameterToTargetBlock()
    {
        base.OwnerBlockEditorUnit.TargetBlock.PassParameter(ParameterIndex, PassedParameterValueBlock);
    }

    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();
    }

    // Update is called once per frame
    protected override void Update()
    {
        base.Update();
    }

    protected override void OnEnable()
    {
        base.OnEnable();
    }

    protected override void OnDisable()
    {
        base.OnDisable();
    }

    public override void Release()
    {
        this.ParameterIndex = -1;
        base.Release();
    }
}
