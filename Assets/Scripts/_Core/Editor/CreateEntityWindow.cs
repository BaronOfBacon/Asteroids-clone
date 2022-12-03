using System;
using UnityEditor;
using UnityEngine;
using System.IO;
using System.Linq;
using System.Text;

namespace Core.Editor
{
    public class CreateEntityWindow : EditorWindow
    {
        private static string _templatesPath => Application.dataPath + "/Scripts/_Core/Editor/Templates/";
        
        private static string _pathSettingsGroupName = "Path settings";
        private static string _pathToRootFolder => Application.dataPath + "/Scripts";
        private static string _pathToCoreFiles => Application.dataPath + "/Scripts/_Core/";

        private string _pathToContainingFolder => _pathToRootFolder + $"/{_newEntityName}";
        
        private string _newEntityName = string.Empty;
        private bool _presenterToggleState = true;
        private bool _factoryToggleState = true;
        private bool _facadeToggleState = true;
        private bool _modelToggleState = true;
        private bool _viewToggleState = true;

        [MenuItem("Window/Entity")]
        public static void ShowWindow()
        {
            GetWindow<CreateEntityWindow>("New Entity");
        }
        
        private void OnGUI()
        {
            GUILayout.Space(20);
            
            _newEntityName = EditorGUILayout.TextField("Name", _newEntityName);

            GUILayout.Space(10);
            
            if (GUILayout.Button("Test"))
            {
                if (!Directory.Exists(_pathToContainingFolder))
                {
                    CreateNewEntity();
                }
                else
                {
                    var dialogueText = string.IsNullOrEmpty(_newEntityName) 
                        ? "Empty name isn't allowed!" 
                        : "The entity with this name already exists!";
                    EditorUtility.DisplayDialog("Oops!", dialogueText, "Close");
                }
            }
        }
        
        private void CreateNewEntity()
        {
            if (Directory.Exists(_pathToCoreFiles))
            {
                Directory.CreateDirectory(_pathToContainingFolder);
                
                var filePaths = Directory.EnumerateFiles(_templatesPath);

                filePaths = filePaths.Where(name => !name.Contains(".meta"));
                
                string coreNamespaceString = $"{Application.productName}.{_newEntityName}";
                string name;
                
                foreach (var path in filePaths)
                {
                    var fileName = Path.GetFileName(path);

                    var newFileName = fileName.Replace("_Template", "");
                    
                    if(fileName.Contains("asmdef"))
                    {
                        fileName =$"/_{_newEntityName}.asmdef";
                        name = newFileName;
                    }
                    else
                    {
                        fileName = $"/{newFileName}.cs";
                        name = _newEntityName;
                    }

                    var newFilePath = _pathToContainingFolder + fileName;
                    var text = File.ReadAllText(path);
                    
                    text = text.Replace("NAMESPACE", coreNamespaceString);
                    text = text.Replace("NAME", name);

                    using FileStream fs = File.Create(newFilePath,1024, FileOptions.WriteThrough);
                    Byte[] bytes = new UTF8Encoding(true).GetBytes(text);
                    fs.Write(bytes,0,text.Length);
                }
            }
            
            AssetDatabase.Refresh();
        }

    }
}
