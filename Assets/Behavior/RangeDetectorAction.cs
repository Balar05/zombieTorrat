using System;
using Unity.Behavior;
using UnityEngine;
using Action = Unity.Behavior.Action;
using Unity.Properties;

[Serializable, GeneratePropertyBag]
[NodeDescription(name: "RangeDetector", story: "update Range [Detector] and assign [Target]", category: "Action", id: "cbcd852cd0f5ee098f7c0760ae44c892")]
public partial class RangeDetectorAction : Action
{
    [SerializeReference] public BlackboardVariable<RangeDetector> Detector;
    [SerializeReference] public BlackboardVariable<GameObject> Target;

    protected override Status OnUpdate()
    {
        Target.Value = Detector.Value.UpdateDetector();
        return Target.Value == null ? Status.Failure : Status.Success;
        //return Status.Success;
    }
}

