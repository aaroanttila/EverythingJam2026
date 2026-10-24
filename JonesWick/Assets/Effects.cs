using UnityEngine;

public static class Effects
{
    public static void Particle(string Name, Vector3 position, float Scale, Transform parent = null) {
        GameObject g = GameObject.Instantiate((GameObject)Resources.Load(Name), position, Quaternion.identity);
        
        g.transform.parent = parent;

        g.transform.localScale = Vector3.one * Scale;

        g.GetComponent<ParticleSystem>().Play();

        GameObject.Destroy(g, g.GetComponent<ParticleSystem>().main.duration);
    }
}
