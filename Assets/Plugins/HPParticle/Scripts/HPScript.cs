// this script controls the HP and Instantiates an HP Particle

using UnityEngine;
using System.Collections;

public class HPScript : MonoBehaviour {

	//the HP Particle
	public GameObject HPParticle;

	//Default Forces
	public Vector3 DefaultForce = new Vector3(0f,1f,0f);
	public float DefaultForceScatter = 0.5f;


	//Change the HP and Instantiates an HP Particle with a Custom Force and Color
	public void ChangeHP(float Delta,Vector3 Position, Vector3 Force, float ForceScatter, Color ThisColor)
	{
		GameObject NewHPP = Instantiate(HPParticle,Position,gameObject.transform.rotation) as GameObject;
		NewHPP.GetComponent<AlwaysFace>().Target = GameObject.Find("Main Camera").gameObject;

		TextMesh TM  = NewHPP.transform.Find("HPLabel").GetComponent<TextMesh>();

		if (Delta > 0)
		{
			TM.text = "+" + Delta.ToString();
		}
		else
		{
			TM.text = Delta.ToString();
		}

		TM.color =  ThisColor;

		NewHPP.GetComponent<Rigidbody>().AddForce( new Vector3(Force.x + Random.Range(-ForceScatter,ForceScatter),Force.y + Random.Range(-ForceScatter,ForceScatter),Force.z + Random.Range(-ForceScatter,ForceScatter)));
	}

	//Change the HP and Instantiates an HP Particle with a Custom Force
	public void ChangeHP(float Delta,Vector3 Position, Vector3 Force, float ForceScatter)
	{
		GameObject NewHPP = Instantiate(HPParticle,Position,gameObject.transform.rotation) as GameObject;
		NewHPP.GetComponent<AlwaysFace>().Target = GameObject.Find("Main Camera").gameObject;
		
		TextMesh TM  = NewHPP.transform.Find("HPLabel").GetComponent<TextMesh>();

		if (Delta > 0f)
		{
			TM.text = "+" + Delta.ToString();
			TM.color =  new Color(0f,1f,0f,1f);
		}
		else
		{
			TM.text = Delta.ToString();
			TM.color =  new Color(1f,0f,0f,1f);
		}
		
		NewHPP.GetComponent<Rigidbody>().AddForce( new Vector3(Force.x + Random.Range(-ForceScatter,ForceScatter),Force.y + Random.Range(-ForceScatter,ForceScatter),Force.z + Random.Range(-ForceScatter,ForceScatter)));
	}

	//Change the HP and Instantiates an HP Particle with a Custom Color
	public void ChangeHP(float Delta,Vector3 Position, Color ThisColor)
	{
		GameObject NewHPP = Instantiate(HPParticle,Position,gameObject.transform.rotation) as GameObject;
		NewHPP.GetComponent<AlwaysFace>().Target = GameObject.Find("Main Camera").gameObject;
		
		TextMesh TM  = NewHPP.transform.Find("HPLabel").GetComponent<TextMesh>();

		if (Delta > 0)
		{
			TM.text = "+" + Delta.ToString();
		}
		else
		{
			TM.text = Delta.ToString();
		}

		TM.color =  ThisColor;
		
		NewHPP.GetComponent<Rigidbody>().AddForce(new Vector3(DefaultForce.x + Random.Range(-DefaultForceScatter,DefaultForceScatter),DefaultForce.y + Random.Range(-DefaultForceScatter,DefaultForceScatter),DefaultForce.z + Random.Range(-DefaultForceScatter,DefaultForceScatter)));
	}

	//Change the HP and Instantiates an HP Particle with default force and color
	public void ChangeHP(float Delta,Vector3 Position)
	{
		GameObject NewHPP = Instantiate(HPParticle,Position,gameObject.transform.rotation) as GameObject;
		NewHPP.GetComponent<AlwaysFace>().Target = GameObject.Find("Main Camera").gameObject;
		
		TextMesh TM  = NewHPP.transform.Find("HPLabel").GetComponent<TextMesh>();

		if (Delta > 0f)
		{
			TM.text = "+" + Delta.ToString();
			TM.color =  new Color(0f,1f,0f,1f);
		}
		else
		{
			TM.text = Delta.ToString();
			TM.color =  new Color(1f,0f,0f,1f);
		}

		
		NewHPP.GetComponent<Rigidbody>().AddForce( new Vector3(DefaultForce.x + Random.Range(-DefaultForceScatter,DefaultForceScatter),DefaultForce.y + Random.Range(-DefaultForceScatter,DefaultForceScatter),DefaultForce.z + Random.Range(-DefaultForceScatter,DefaultForceScatter)));
	}

	//Change the HP and Instantiates an HP Particle with Custom Text
	public void ChangeHP(float Delta,Vector3 Position, string text)
	{
		GameObject NewHPP = Instantiate(HPParticle,Position,gameObject.transform.rotation) as GameObject;
		NewHPP.GetComponent<AlwaysFace>().Target = GameObject.Find("Main Camera").gameObject;
		
		TextMesh TM  = NewHPP.transform.Find("HPLabel").GetComponent<TextMesh>();
		TM.text = text;
		
		if (Delta > 0f)
		{
			TM.color =  new Color(0f,1f,0f,1f);
		}
		else
		{
			TM.color =  new Color(1f,0f,0f,1f);
		}
		
		
		NewHPP.GetComponent<Rigidbody>().AddForce( new Vector3(DefaultForce.x + Random.Range(-DefaultForceScatter,DefaultForceScatter),DefaultForce.y + Random.Range(-DefaultForceScatter,DefaultForceScatter),DefaultForce.z + Random.Range(-DefaultForceScatter,DefaultForceScatter)));
	}

	//Change the HP and Instantiates an HP Particle with Custom Text and Force,
	public void ChangeHP(float Delta,Vector3 Position, Vector3 Force, float ForceScatter, string text)
	{
		GameObject NewHPP = Instantiate(HPParticle,Position,gameObject.transform.rotation) as GameObject;
		NewHPP.GetComponent<AlwaysFace>().Target = GameObject.Find("Main Camera").gameObject;
		
		TextMesh TM  = NewHPP.transform.Find("HPLabel").GetComponent<TextMesh>();
		TM.text = text;
		
		if (Delta > 0f)
		{
			TM.color =  new Color(0f,1f,0f,1f);
		}
		else
		{
			TM.color =  new Color(1f,0f,0f,1f);
		}
		
		
		NewHPP.GetComponent<Rigidbody>().AddForce( new Vector3(Force.x + Random.Range(-ForceScatter,ForceScatter),Force.y + Random.Range(-ForceScatter,ForceScatter),Force.z + Random.Range(-ForceScatter,ForceScatter)));
	}

	//Change the HP and Instantiates an HP Particle with Custom Text, Force and Color
	public void ChangeHP(float Delta,Vector3 Position, Vector3 Force, float ForceScatter, Color ThisColor, string text)
	{
		
		GameObject NewHPP = Instantiate(HPParticle,Position,gameObject.transform.rotation) as GameObject;
		NewHPP.GetComponent<AlwaysFace>().Target = GameObject.Find("Main Camera").gameObject;
		
		TextMesh TM  = NewHPP.transform.Find("HPLabel").GetComponent<TextMesh>();
		TM.text = text;
		TM.color =  ThisColor;

		NewHPP.GetComponent<Rigidbody>().AddForce( new Vector3(Force.x + Random.Range(-ForceScatter,ForceScatter),Force.y + Random.Range(-ForceScatter,ForceScatter),Force.z + Random.Range(-ForceScatter,ForceScatter)));
	}

	//Change the HP and Instantiates an HP Particle with Custom Text and Color
	public void ChangeHP(Vector3 Position, Color ThisColor, string text)
	{
		GameObject NewHPP = Instantiate(HPParticle,Position,gameObject.transform.rotation) as GameObject;
        NewHPP.GetComponent<AlwaysFace>().Target = Camera.main.gameObject; //GameObject.Find("Main Camera").gameObject;
		
		TextMesh TM  = NewHPP.transform.Find("HPLabel").GetComponent<TextMesh>();
		TM.text = text;
		TM.color =  ThisColor;
		
		NewHPP.GetComponent<Rigidbody>().AddForce( new Vector3(DefaultForce.x + Random.Range(-DefaultForceScatter,DefaultForceScatter),DefaultForce.y + Random.Range(-DefaultForceScatter,DefaultForceScatter),DefaultForce.z + Random.Range(-DefaultForceScatter,DefaultForceScatter)));
	}
	
}
