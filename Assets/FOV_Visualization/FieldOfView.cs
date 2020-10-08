using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using UnityEngine;
using UnityEngine.UIElements;

public class FieldOfView : MonoBehaviour {
    public float ViewRadius;
    [Range(0, 360)]
    public float ViewAngle;

    public LayerMask TargetMask;
    public LayerMask ObstacleMask;

    public float MeshResolution;
    public int EdgeResolveIterations;
    public float EdgeDistanceThreshold;

    public MeshFilter ViewMeshFilter;
    Mesh viewMesh;

    [HideInInspector]
    public List<Transform> VisibleTargetList = new List<Transform>();
    private void Start() {
        viewMesh = new Mesh();
        viewMesh.name = "View Mesh";
        ViewMeshFilter.mesh = viewMesh;
        StartCoroutine("FindVisibleDelayWithDelay", 0.2f);
    }
    private void LateUpdate() {
        DrawFieldOfView();
    }
    void DrawFieldOfView() {
        int stepCount = Mathf.RoundToInt(ViewAngle * MeshResolution);
        float stepAngleSize = ViewAngle / stepCount;
        List<Vector3> viewPoints = new List<Vector3>();
        ViewCastInfo oldViewCast = new ViewCastInfo();

        for (int i = 0; i <= stepCount; i++) {
            float angle = transform.eulerAngles.y - ViewAngle / 2 + stepAngleSize * i;
            //Debug.DrawLine(transform.position, transform.position + DirFromAngle(angle, true) * ViewRadius, Color.red);
            ViewCastInfo newViewCast = GetViewCast(angle);
            viewPoints.Add(newViewCast.point);
            if (i > 0) {
                if (oldViewCast.hasHit == newViewCast.hasHit) {
                    EdgeInfo edge = GetEdgeInfo(oldViewCast, newViewCast);
                    if (edge.pointA != Vector3.zero) {
                        viewPoints.Add(edge.pointA);
                    }
                    if (edge.pointB != Vector3.zero) {
                        viewPoints.Add(edge.pointB);
                    }

                }
            }
        }
        int vertexCount = viewPoints.Count + 1;
        Vector3[] vertices = new Vector3[vertexCount];
        int[] triangles = new int[(vertexCount - 2) * 3];
        vertices[0] = Vector3.zero;
        for (int i = 0; i < vertexCount - 1; i++) {
            vertices[i + 1] = transform.InverseTransformPoint(viewPoints[i]);
            if (i < vertexCount - 2) {
                triangles[i * 3] = 0;
                triangles[i * 3 + 1] = i + 1;
                triangles[i * 3 + 2] = i + 2;
            }
        }
        viewMesh.Clear();
        viewMesh.vertices = vertices;
        viewMesh.triangles = triangles;
        viewMesh.RecalculateNormals();
    }

    public EdgeInfo GetEdgeInfo(ViewCastInfo minViewCast, ViewCastInfo maxViewCast) {

        float minAngle = minViewCast.angle;
        float maxAngle = maxViewCast.angle;
        Vector3 minPoint = Vector3.zero;
        Vector3 maxPoint = Vector3.zero;
        for (int i = 0; i < EdgeResolveIterations; i++) {
            float angle = (minAngle + maxAngle) / 2;
            ViewCastInfo newViewCast = GetViewCast(angle);
            if (newViewCast.hasHit == minViewCast.hasHit) {
                minAngle = angle;
                minPoint = minViewCast.point;
            }
            else {
                maxAngle = angle;
                minPoint = maxViewCast.point;
            }
        }
        return new EdgeInfo(minPoint, maxPoint);
    }
    public ViewCastInfo GetViewCast(float globalAngle) {
        Vector3 dir = DirFromAngle(globalAngle, true);
        RaycastHit hit;
        if (Physics.Raycast(transform.position, dir, out hit, ViewRadius, ObstacleMask)) {
            return new ViewCastInfo(true, hit.point, hit.distance, globalAngle);
        }
        else {
            return new ViewCastInfo(false, transform.position + dir * ViewRadius, ViewRadius, globalAngle);
        }
    }
    public Vector3 DirFromAngle(float angleInDegrees, bool isAngleGlobal) {
        if (!isAngleGlobal) {
            angleInDegrees += transform.eulerAngles.y;
        }
        return new Vector3(Mathf.Sin(angleInDegrees * Mathf.Deg2Rad), 0, Mathf.Cos(angleInDegrees * Mathf.Deg2Rad));
    }

    void FindVisibleTarget() {
        VisibleTargetList.Clear();
        Collider[] targetInViewRadius = Physics.OverlapSphere(transform.position, ViewRadius, TargetMask);
        for (int i = 0; i < targetInViewRadius.Length; i++) {
            Transform target = targetInViewRadius[i].transform;
            Vector3 dirToTarget = (target.position - transform.position).normalized;
            if (Vector3.Angle(transform.forward, dirToTarget) < ViewAngle / 2.0f) {
                float distanceToTarget = Vector3.Distance(transform.position, target.position);
                if (!Physics.Raycast(transform.position, dirToTarget, distanceToTarget, ObstacleMask)) {
                    VisibleTargetList.Add(target);
                }
            }
        }
    }
    IEnumerator FindVisibleDelayWithDelay(float delay) {
        while (true) {
            yield return new WaitForSeconds(delay);
            FindVisibleTarget();
        }
    }

    public struct ViewCastInfo {
        public bool hasHit;
        public Vector3 point;
        public float distance;
        public float angle;
        public ViewCastInfo(bool hasHit_, Vector3 point_, float distance_, float angle_) {
            hasHit = hasHit_;
            point = point_;
            distance = distance_;
            angle = angle_;
        }
    }
    public struct EdgeInfo {
        public Vector3 pointA;
        public Vector3 pointB;
        public EdgeInfo(Vector3 pointA_, Vector3 pointB_) {
            pointA = pointA_;
            pointB = pointB_;
        }
    }
}
