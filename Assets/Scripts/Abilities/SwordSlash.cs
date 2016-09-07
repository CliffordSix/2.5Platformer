using UnityEngine;
using System.Collections;

public class SwordSlash : Ability
{
    /* public Transform SlashBegin;
     public Transform SlashEnd;*/

    public ParticleSystem Slash;

  //  public Material trailMaterial;

    bool SlashEffect = false;

    public override void CallAbility(GameObject projectile)
    {
        Debug.Log("SwordSlash");
        Slash.Play();
       // Debug.Log("SWORD SLASH");
       // SlashEffect = true;
       // SlashEffectCoolDown();
    }

    void Update()
    {
    /*    Debug.Log("SwordSlashUpdate");
       // base.Update();
        if(SlashEffect)
        {
            Vector3[] trailVerts = new Vector3[2];
            trailVerts[0] = SlashBegin.position;
            trailVerts[1] = SlashEnd.position;
            Vector2[] trailUV = new Vector2[trailVerts.Length];
            for(int i = 0; i < trailUV.Length; i++)
            {
                trailUV[i] = new Vector2(trailVerts[i].x, trailVerts[i].z);
            }
            Mesh weaponTrail = new Mesh();
            GetComponent<MeshFilter>().mesh = weaponTrail;
            weaponTrail.vertices = trailVerts;
            weaponTrail.uv = trailUV;
            weaponTrail.triangles = new int[] { 0, 1, 2 };
            Color[] colours = new Color[trailVerts.Length];
            for(int i = 0; i <trailVerts.Length; i++)
            {
                colours[i] = Color.Lerp(Color.red, Color.green, trailVerts[i].y);
            }
            weaponTrail.colors = colours;

            Graphics.DrawMesh(weaponTrail, trailVerts[0], Quaternion.identity, trailMaterial, 0);
        }*/
    }

  /*  IEnumerator SlashEffectCoolDown()
    {
        yield return new WaitForSeconds(CD);
        SlashEffect = false;
    }*/
}
