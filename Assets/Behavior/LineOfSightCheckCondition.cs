using System;
using Unity.Behavior;
using UnityEngine;

[Serializable, Unity.Properties.GeneratePropertyBag]
[Condition(name: "Line of Sight check", story: "Check [Target] with Line Of Sight [Detector]", category: "Conditions", id: "e411e9cd9397bda21e5cc86703ad65e2")]
public partial class LineOfSightCheckCondition : Condition
{
    [SerializeReference] public BlackboardVariable<GameObject> Target;
    [SerializeReference] public BlackboardVariable<LineOfSightDetector> Detector;

    public override bool IsTrue()
    {
        return Detector.Value.PerformDetection(Target.Value) != null; 
    }
}
