using UnityEngine;

public class CCDIKJoint : MonoBehaviour {
  public Vector3 axis = Vector3.right;
  public float maxAngle = 180;
  Vector3 perpendicular; void Start() { perpendicular = axis.perpendicular(); }

  public void Evaluate(Transform ToolTip, Transform Target, float alpha = 1f) {
    //Rotate the assembly so the tooltip better matches the target position/direction
    transform.rotation = Quaternion.Slerp(Quaternion.identity, Quaternion.FromToRotation(ToolTip.position - transform.position, Target.position - transform.position), alpha) * transform.rotation;

    //Enforce only rotating with the hinge
    transform.rotation = Quaternion.FromToRotation(transform.rotation * axis, transform.parent.rotation * axis) * transform.rotation;

    //Enforce Joint Limits
    transform.rotation = Quaternion.FromToRotation(transform.rotation * perpendicular, (transform.rotation * perpendicular).ConstrainToNormal(transform.parent.rotation * perpendicular, maxAngle)) * transform.rotation;
  }
}
