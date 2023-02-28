using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VFX;


public class MysticController : MonoBehaviour
{


    [SerializeField]
    VisualEffect _objectVFX;

    [SerializeField]
    VisualEffect _vectorFieldVFX;

    [SerializeField]
    VisualEffect _beamVFX;

    [SerializeField]
    Transform _handPosSample;

    [SerializeField]
    float _interactRadius = 1f;

    float _absorbProcess = 0;

    bool _elementCubeActive = true;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (_elementCubeActive)
        {
            if (Vector3.Distance(_handPosSample.position, this.transform.position + (Vector3.up * 1f)) < _interactRadius)
            {
                _absorbProcess += Time.deltaTime * 0.5f;
                if (_absorbProcess > 1)
                {
                    _absorbProcess = 1;
                    // do sth when collection done
                    Debug.Log("Done Collection! Added object to your wallet!");
                    FindObjectOfType<ExploreSceneManager>().OpenPopupWindows();
                    _elementCubeActive = false;
                }
            }
            else
            {
                _absorbProcess -= Time.deltaTime * 0.5f;
                if (_absorbProcess < 0)
                {
                    _absorbProcess = 0;
                }
            }

            _vectorFieldVFX.SetFloat("Spawn Rate", (1 - _absorbProcess) * 20000f);
            _objectVFX.SetFloat("Alpha", (1 - _absorbProcess) * 1);
            _beamVFX.SetFloat("Alpha", (1 - _absorbProcess) * 1);
        }

    }
}
