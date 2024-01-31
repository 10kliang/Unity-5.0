using UnityEngine;
using System.Collections;
public class touchdisapear : MonoBehaviour
{
    public Renderer rend;
    public MeshCollider rend2;

    void Start()
    {
        rend = GetComponent<Renderer>();
        rend2 = GetComponent<MeshCollider>();
        rend.enabled = true;
        rend2.enabled = true;
        
    }
    void OnCollisionEnter(Collision col)
    {
        if (col.gameObject.tag == "Player")
        {
            
            StartCoroutine(Time());
        }
    }
    // Update is called once per frame
    void Update()
    {

    }
    IEnumerator Time()
    {
        yield return new WaitForSeconds(3f);
        rend.enabled = false;
        rend2.enabled = false;
        
        yield return new WaitForSeconds(5f);
        rend.enabled = true;
        rend2.enabled = true;
       
    }
}