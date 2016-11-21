/*
 * Created by SharpDevelop.
 * User: А.Скрипкин
 * Date: 13.10.2016
 * Time: 10:01
 */
using System;
using System.IO;

namespace GLCapsule.ObjLoader
{
    /// <summary>
    /// Description of ObjBuilder.
    /// </summary>
    public class ObjBuilder
    {
        private readonly ObjFileReader reader;
        private ObjScriptBuilder scriptBuilder;
        
        public ObjBuilder(string fname)
        {
            reader = new ObjFileReader(fname);
            reader.OnGeometryCommand = AddGeometry;
            reader.OnIndexCommand = AddIndex;
            reader.OnStringCommand = ParseString;
            
            scriptBuilder = new ObjScriptBuilder();
        }

        public ObjBuilder(Stream istream)
        {
            reader = new ObjFileReader(istream);
            reader.OnGeometryCommand = AddGeometry;
            reader.OnIndexCommand = AddIndex;
            reader.OnStringCommand = ParseString;

            scriptBuilder = new ObjScriptBuilder();
        }
        
        private void AddGeometry(ObjCommand<float> command)
        {
            
        }
        
        private void ParseString(ObjCommand<string> command)
        {
            
        }
        
        private void AddIndex(ObjCommand<IndexTriple> command)
        {
            
        }
        
        public ObjScript Script { get { return scriptBuilder.Script; } }
    }
}
