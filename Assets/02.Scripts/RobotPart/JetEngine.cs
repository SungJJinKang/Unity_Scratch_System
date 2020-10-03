using UnityEngine;

public sealed class JetEngine : RobotPart
{
    private float maxEnginePower = 5;

    private float enginePower;
    public float EnginePower
    {
        get
        {
            return this.enginePower;
        }
        set
        {
            this.enginePower = Mathf.Clamp(value, 0, this.maxEnginePower);
        }
    }


    public Vector2 Destination;


    protected override void Awake()
    {
        base.Awake();

        Destination = new Vector2();
    }

    protected override void Start()
    {
        base.Start();
    }

    private void Update()
    {
        base.MotherRobotBase.transform.position = Vector3.MoveTowards(base.MotherRobotBase.transform.position, this.Destination, Time.deltaTime * this.EnginePower);
    }


}

