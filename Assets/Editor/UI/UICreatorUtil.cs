#if UNITY_EDITOR 
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using System.IO;

using UnityEditor;


public class UICreatorUtil 
{
    public static void GenUICtrlFile(string filePath, string className) {
        if (File.Exists(Application.dataPath + filePath + className + ".cs")) {
            return;
        }

        StreamWriter sw = null;
        sw = new StreamWriter(Application.dataPath + filePath + className + ".cs");

        sw.WriteLine(
            "using UnityEngine;\nusing System.Collections;\nusing UnityEngine.UI;\nusing System.Collections.Generic;\n\n");

        sw.WriteLine("public class " + className + " : UICtrl {" + "\n");

        sw.WriteLine("\t" + "public override void Awake() {");
        sw.WriteLine("\t\t" + "base.Awake();" + "\n");
        sw.WriteLine("\t" + "}" + "\n");

        sw.WriteLine("\t" + "void Start() {");
        sw.WriteLine("\t" + "}" + "\n");
        sw.WriteLine("}");

        sw.Flush();
        sw.Close();
        
    }
}
#endif
