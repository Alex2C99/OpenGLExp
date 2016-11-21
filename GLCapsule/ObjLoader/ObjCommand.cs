/*
 * Created by SharpDevelop.
 * User: А.Скрипкин
 * Date: 13.10.2016
 * Time: 9:23
 */
using System;

namespace GLCapsule.ObjLoader
{
    /// <summary>
    /// Description of ObjCommand.
    /// </summary>
    public class ObjCommand<CommandParameter>
    {
        public enum ObjCommandType { VertexData, TextureData, NormalData, IndexData,  LoadMaterial, UseMaterial}
        
        private readonly CommandParameter[] parameters;
        
        public ObjCommand(ObjCommandType tp, params CommandParameter[] prm)
        {
            Type = tp;
            parameters = prm;
        }
        
        public ObjCommandType Type { get; private set; }
        public CommandParameter[] Params { get { return parameters; } }
    }
}
