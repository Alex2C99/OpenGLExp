/*
 * Created by SharpDevelop.
 * User: А.Скрипкин
 * Date: 13.10.2016
 * Time: 9:42
 */
using System;

namespace GLCapsule.ObjLoader
{
    /// <summary>
    /// Description of ObjScript.
    /// </summary>
    public class ObjScript
    {
        public delegate void DrawEvent();
        public delegate void SetMaterialEvent();
        
        public ObjScript()
        {
        }
        
        public void Execute()
        {
            
        }
        
        public DrawEvent OnDraw { get; set; }
        public SetMaterialEvent OnMaterial { get; set; }
    }
}
