using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shell : MonoBehaviour
{

    [SerializeField] public Rigidbody myRigidBody;
    [SerializeField] public float forceMin;
    [SerializeField] public float forceMax;

    float lifeTime = 4;
    float fadeTime = 2;

    void Start()
    {
        float force = Random.Range(forceMin, forceMax);
        myRigidBody.AddForce(transform.right * force);
        myRigidBody.AddTorque(Random.insideUnitSphere * force);

        StartCoroutine(Fade());
    }

    IEnumerator Fade()
    {
        yield return new WaitForSeconds(lifeTime);

        float percent = 0;
        float fadeSpeed = 1 / fadeTime;
        Material mat = GetComponent<Renderer>().material;
        Color initialColour = mat.color;

        while (percent < 1)
        {
            percent += Time.deltaTime * fadeSpeed;
            mat.color = Color.Lerp(initialColour, Color.clear, percent);
            yield return null;
        }

        Destroy(gameObject);
    }

}
