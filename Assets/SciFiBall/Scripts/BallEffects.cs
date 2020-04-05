using UnityEngine;

public class BallEffects : MonoBehaviour {

    private Material mat;
    private ParticleSystem emmiter;
    private ParticleSystem.EmissionModule emmisionMod;
    

	// Use this for initialization
	void Start () {
        mat = GetComponent<Renderer>().material;
        emmiter = GetComponent<ParticleSystem>();
        emmisionMod = emmiter.emission;
    }
	
	// Update is called once per frame
	void Update () {
        float boostValue = Input.GetAxis("Boost");
        Color emmisionsCol = new Color(boostValue, boostValue, boostValue);
        mat.SetColor("_EmissionColor", emmisionsCol);

        if (boostValue > 0)
        {
            emmisionMod.enabled = true;
        }
        else
        {
            emmisionMod.enabled = false;
        }
	}
}
