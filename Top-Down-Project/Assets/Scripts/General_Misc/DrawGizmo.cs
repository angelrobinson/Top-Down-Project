using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// This is a utility script to draw gizmos within the scene view only.  
/// When adding this component to an object in your scene you need to choose the type of gizmo you want
/// and the color of the gizmo.
/// 
/// </summary>
public class DrawGizmo : MonoBehaviour
{
    public enum GizmoType { NONE, CUBE, FRUSTRUM, GUI_TEXT, ICON, LINE, MESH, RAY, SPHERE, WIRE_CUBE, WIRE_MESH, WIRE_SPHERE }

    

    [Header("General Settings")]
    [SerializeField] GizmoType type = GizmoType.NONE;
    [SerializeField] Color gizmoColor = Color.cyan;

    [Header("Cube Settings")]
    [SerializeField] Vector3 scale;
    [Header("Frustrum Settings")]
    [SerializeField] float FOV;
    [SerializeField] Vector3 center;
    [SerializeField] float farDist;
    [SerializeField] float nearDist;
    [SerializeField] float aspect;

    [Header("GUI Texture Settings")]
    [Tooltip("X & Y cordinates of the texture")]
    [SerializeField] Vector2 XYPosition;
    [Tooltip("X = Width, Y=Height")]
    [SerializeField] Vector2 XYSize = new Vector2();
    [Tooltip("texture to show")]
    [SerializeField] Texture texture = null;
    [Tooltip("If you want border, set thickness of each side: Left, Right, Top, Bottom")]
    [SerializeField] int lftBorder;
    [Tooltip("If you want border, set thickness of each side: Left, Right, Top, Bottom")]
    [SerializeField] int rtBorder;
    [Tooltip("If you want border, set thickness of each side: Left, Right, Top, Bottom")]
    [SerializeField] int topBorder;
    [Tooltip("If you want border, set thickness of each side: Left, Right, Top, Bottom")]
    [SerializeField] int bottBorder;
    [Tooltip("Optional material to put on the texture")]
    [SerializeField] Material mat = null;

    [Header("Icon Settings")]
    [Tooltip("This texture needs to be stored in the Assets/Gizmos folder")]
    [SerializeField] Texture icon = null;
    [Tooltip("The file extension of the icon photo you are using. DO NOT put the dot (.) before it. For example: jpg, png, ico, tiff")]
    [SerializeField] string fileExt = null;

    [Header("Line Settings")]
    [SerializeField] Transform startPosition;
    [SerializeField] Transform endPosition;

    [Header("Mesh Settings")]
    [SerializeField] Mesh mesh = null;

    [Header("Ray Settings")]
    [SerializeField] Transform rayStart;

    [Header("Wire-Cube Settings")]
    [SerializeField] Vector3 wireScale;

    [Header("Wire-Mesh Settings")]
    [SerializeField] Mesh wireMesh = null;

    [Header("Wire-Sphere Settings")]
    [SerializeField] float wireRadius = 0;

    [Header("Sphere Settings")]
    [SerializeField] float radius;

    
    

    private void OnDrawGizmos()
    {
        Gizmos.matrix = Matrix4x4.TRS(transform.position, transform.rotation, Vector3.one);
        Gizmos.color = Color.Lerp(gizmoColor, Color.clear, 0.5f);
        

        switch (type)
        {            
            case GizmoType.CUBE:
                if (scale == null)
                {
                    scale = Vector3.one;
                }
                Gizmos.DrawCube(Vector3.up * scale.y / 2f, scale);
                Gizmos.color = gizmoColor;
                break;
            case GizmoType.FRUSTRUM:
                if (nearDist == 0)
                {
                    nearDist = 1;
                }
                if (farDist == 0)
                {
                    farDist = 100;
                }
                if (FOV == 0)
                {
                    FOV = 30;
                }
                if (aspect == 0)
                {
                    aspect = 1.33f;
                }
                if (center == null)
                {
                    center = Vector3.zero;
                }
                Gizmos.DrawFrustum(center, FOV, farDist, nearDist, aspect);
                Gizmos.color = gizmoColor;
                break;
            case GizmoType.GUI_TEXT:
                //defaults for boarder
                if (lftBorder < 0)
                {
                    lftBorder = 0;
                }
                if (rtBorder < 0)
                {
                    rtBorder = 0;
                }
                if (topBorder < 0)
                {
                    topBorder = 0;
                }
                if (bottBorder < 0)
                {
                    bottBorder = 0;
                }

                if (XYSize == null)
                {
                    XYSize = new Vector2(1, 1);
                }

                if (XYPosition == null)
                {
                    XYPosition = new Vector2(0, 0);
                }

                if (mat)
                {
                    Gizmos.DrawGUITexture(new Rect(XYPosition, XYSize), texture, lftBorder, rtBorder, topBorder, bottBorder, mat);
                }
                else
                {
                    Gizmos.DrawGUITexture(new Rect(XYPosition, XYSize), texture, lftBorder, rtBorder, topBorder, bottBorder);
                }
                
                break;
            case GizmoType.ICON:
                Gizmos.DrawIcon(transform.position, icon.name + "." + fileExt, true);
                break;
            case GizmoType.LINE:
                if (startPosition == null)
                {
                    startPosition = transform;
                }

                if (endPosition = null)
                {
                    endPosition = transform;
                }
                Gizmos.DrawLine(startPosition.position, endPosition.position);
                break;
            case GizmoType.MESH:
                Gizmos.DrawMesh(mesh, transform.position, transform.rotation);
                break;
            case GizmoType.RAY:
                if (rayStart == null)
                {
                    rayStart = transform;
                }
                Gizmos.DrawRay(rayStart.localPosition, Vector3.forward * 5);
                break;
            case GizmoType.SPHERE:
                if (radius == 0)
                {
                    radius = 1;
                }
                Gizmos.DrawSphere(Vector3.zero, radius);
                break;
            case GizmoType.WIRE_CUBE:
                if (wireScale == null)
                {
                    wireScale = Vector3.one;
                }
                Gizmos.DrawWireCube(Vector3.zero, wireScale);
                break;
            case GizmoType.WIRE_MESH:
                Gizmos.DrawWireMesh(wireMesh);
                break;
            case GizmoType.WIRE_SPHERE:
                Gizmos.DrawWireSphere(Vector3.zero, wireRadius);
                break;
            default:
                break;
        }
        
    }
}
