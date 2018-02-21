using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElementalComboBase : MonoBehaviour {

    public float Timer;
    float timer;
    
    // Use this for initialization
    void Start () {
        timer = Timer;
        DoInit();
    }
	
	// Update is called once per frame
	void Update () {
        timer -= Time.deltaTime;
        if (timer <= 0)
        {
            OnEndEffect();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        ComboEffect(other);
    }

    protected virtual void DoInit() { }
    /// <summary>
    /// L'effetto che combiono le classi che ereditano da questa classe
    /// </summary>
    /// <param name="other"></param>
    protected virtual void ComboEffect(Collider other) { }

    /// <summary>
    /// Le azioni da svolegere una volta scaduto il timer
    /// </summary>
    protected virtual void OnEndEffect()
    {
        Destroy(gameObject);
    }
}
