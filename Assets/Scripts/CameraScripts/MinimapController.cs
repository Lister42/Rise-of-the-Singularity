using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MinimapController : MonoBehaviour
{
    public Material _cameraBoxMaterial;
    public Camera _minimap;
    private float lineWidth = 0.012F;
    public Collider mapCollider;

    private Vector3 GetCameraFrustumPoint(Vector3 position)
    {
        Ray positionRay = Camera.main.ScreenPointToRay(position);
        RaycastHit hit;
        Vector3 result = mapCollider.Raycast(positionRay, out hit, Camera.main.transform.position.y * 2) ? hit.point : new Vector3();

        return result;
    }

    //public void OnPostRender()
    //{
    //    float z = -1;
    //    Vector3 minViewportPoint = _minimap.WorldToViewportPoint(GetCameraFrustumPoint(new Vector3(0f, 0f)));
    //    Vector3 maxViewportPoint = _minimap.WorldToViewportPoint(GetCameraFrustumPoint(new Vector3(Screen.width, Screen.height)));

    //    float minX = minViewportPoint.x;
    //    float minY = minViewportPoint.y;

    //    float maxX = maxViewportPoint.x;
    //    float maxY = maxViewportPoint.y;

    //    GL.PushMatrix();
    //    {
    //        _cameraBoxMaterial.SetPass(0);
    //        GL.LoadOrtho();

    //        GL.Begin(GL.QUADS);
    //        GL.Color(Color.red);
    //        {

    //            GL.Vertex(new Vector3(minX, minY + lineWidth, z));
    //            GL.Vertex(new Vector3(minX, minY - lineWidth, z));
    //            GL.Vertex(new Vector3(maxX, minY - lineWidth, z));
    //            GL.Vertex(new Vector3(maxX, minY + lineWidth, z));

    //            GL.Vertex(new Vector3(minX + lineWidth, minY, z));
    //            GL.Vertex(new Vector3(minX - lineWidth, minY, z));
    //            GL.Vertex(new Vector3(minX - lineWidth, maxY, z));
    //            GL.Vertex(new Vector3(minX + lineWidth, maxY, z));

    //            GL.Vertex(new Vector3(minX, maxY + lineWidth, z));
    //            GL.Vertex(new Vector3(minX, maxY - lineWidth, z));
    //            GL.Vertex(new Vector3(maxX, maxY - lineWidth, z));
    //            GL.Vertex(new Vector3(maxX, maxY + lineWidth, z));

    //            GL.Vertex(new Vector3(maxX + lineWidth, minY, z));
    //            GL.Vertex(new Vector3(maxX - lineWidth, minY, z));
    //            GL.Vertex(new Vector3(maxX - lineWidth, maxY, z));
    //            GL.Vertex(new Vector3(maxX + lineWidth, maxY, z));
    //        }
    //        GL.End();
    //    }
    //    GL.PopMatrix();
    //}
}