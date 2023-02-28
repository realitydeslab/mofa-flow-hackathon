using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.SceneManagement;


public class ExploreSceneManager : MonoBehaviour
{
    [SerializeField]
    ARRaycastManager m_RaycastManager;

    [SerializeField]
    GameObject m_CollctableObjcetPrefab;


    [SerializeField]
    GameObject m_CollctableObjcetObject;

    [Header("UI Components")]
    [SerializeField]
    GameObject m_PopupWindow;

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

    public void ClosePopupWindows()
    {
        m_PopupWindow.SetActive(false);
    }

    public void OpenPopupWindows()
    {
        m_PopupWindow.SetActive(true);
    }

    public void LoadScene(string SceneName)
    {
        SceneManager.LoadScene(SceneName, LoadSceneMode.Single);
    }
}
