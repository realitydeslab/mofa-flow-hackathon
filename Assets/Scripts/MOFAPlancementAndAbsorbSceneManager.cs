using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;

public class MOFAPlancementAndAbsorbSceneManager : MonoBehaviour
{
    [SerializeField]
    ARRaycastManager m_RaycastManager;

    [SerializeField]
    GameObject m_CollctableObjcetPrefab;


    [SerializeField]
    GameObject m_CollctableObjcetObject;

    GameObject m_CollctableObjectInsatance;

    List<ARRaycastHit> m_Hits = new List<ARRaycastHit>();

    void Update()
    {
        if (Input.touchCount == 0)
            return;

        if (m_RaycastManager.Raycast(Input.GetTouch(0).position, m_Hits))
        {
            // Only returns true if there is at least one hit
            if(m_CollctableObjectInsatance == null)
            {
                //m_CollctableObjectInsatance = Instantiate(m_CollctableObjcetPrefab);

                m_CollctableObjcetObject.SetActive(true);
                m_CollctableObjcetObject.transform.position = m_Hits[0].pose.position + (Vector3.up * 0f);
            }
        }
    }
}
