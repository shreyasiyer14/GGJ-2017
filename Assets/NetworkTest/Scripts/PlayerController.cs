using UnityEngine;
using System.Collections;

[RequireComponent(typeof(PlayerMotor))]
public class PlayerController : MonoBehaviour {

	[SerializeField]
	private float speed = 5f;

	[SerializeField]
	private float sensitivity = 3f;

	[SerializeField]
	private float thrusterForce = 1000f; 
	/*
	[Header("Spring Settings: ")]
	[SerializeField]
	private JointDriveMode jointMode = JointDriveMode.Position;
	[SerializeField]
	private float jointSpring = 20f;
	[SerializeField]
	private float jointMaxForce = 40f;
	*/

	private PlayerMotor motor;
	//private ConfigurableJoint joint;
//	private bool Jumped = false;
	void Start () {
		motor = GetComponent<PlayerMotor> ();
		//joint = GetComponent<ConfigurableJoint> ();
		//SetJointSettings (jointSpring);
		Physics.gravity *= 5;
	}

	void Update () {
		float xMov = Input.GetAxisRaw ("Horizontal");
		float zMov = Input.GetAxisRaw ("Vertical");

		Vector3 movHorizontal = transform.right * xMov;
		Vector3 movVertical = transform.forward * zMov;

		Vector3 velocity = (movHorizontal + movVertical).normalized * speed;

		motor.Move (velocity);
		/*
		float yRot = Input.GetAxisRaw ("Mouse X");
		Vector3 rotation = new Vector3 (0f, yRot, 0f) * sensitivity;

		motor.Rotate (rotation);

		float xRot = Input.GetAxisRaw ("Mouse Y");
		float cameraRotation = xRot * sensitivity;

		motor.CameraRotate (cameraRotation);

		Vector3 _thrusterForce = Vector3.zero;
		if (Input.GetButton ("Jump")) {
			_thrusterForce = Vector3.up * thrusterForce;
			SetJointSettings (0f);
			Jumped = true;
		} else {
			SetJointSettings (jointSpring);
			Jumped = false;
		}

		motor.ApplyThruster (_thrusterForce);
		*/
	}

	//private void SetJointSettings (float _jointSpring) {
	//	joint.yDrive = new JointDrive { mode = jointMode, positionSpring = _jointSpring, maximumForce = jointMaxForce }; 
	//}
}
