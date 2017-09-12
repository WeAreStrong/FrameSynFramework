using UnityEngine;
using TrueSync;

[ExecuteInEditMode]
[RequireComponent(typeof(TSTransform))]
public class CpEditorTransformDataToRunTime : MonoBehaviour
{
    [SerializeField]
    [HideInInspector]
    [AddTracking]
    private TSVector mEditorValue;

	// Update is called once per frame
	void Update()
    {
        if (Application.isPlaying == false)
        {
            mEditorValue = transform.position.ToTSVector();
        }
        else
        {
            TSTransform ts = GetComponent<TSTransform>();
            ts.position = mEditorValue;
            Destroy(this);
        }
	}
}
