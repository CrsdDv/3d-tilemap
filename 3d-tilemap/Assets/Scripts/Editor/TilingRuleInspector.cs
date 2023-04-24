using UnityEditor;
using UnityEditor.UIElements;
using UnityEngine.UIElements;

[CustomEditor(typeof(TilingRule))]
public class TilingRuleInspector : Editor
{
    public VisualTreeAsset m_InspectorXML;

    public override VisualElement CreateInspectorGUI()
    {
        // Create a new VisualElement to be the root of our inspector UI
        VisualElement myInspector = new VisualElement();

        myInspector.Bind(new SerializedObject(target));

        // Load from default reference
        m_InspectorXML.CloneTree(myInspector);

        // Return the finished inspector UI
        return myInspector;
    }
}
