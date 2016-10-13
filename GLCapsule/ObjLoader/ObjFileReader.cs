/*
 * Created by SharpDevelop.
 * User: А.Скрипкин
 * Date: 13.10.2016
 * Time: 9:15
 */
using System;
using System.IO;
using GLCapsule.ObjLoader;

namespace GLCapsule.ObjLoader
{
    /// <summary>
    /// Description of ObjFileReader.
    /// </summary>
    public class ObjFileReader
    {
        public ObjFileReader(string fname)
        {
        }

        public ObjFileReader(Stream istream)
        {
        }
        
        public delegate void GeomeryReadEvent(ObjCommand<float> command);
        public delegate void StringReadEvent(ObjCommand<string> command);
        public delegate void FaceReadEvent(ObjCommand<IndexTriple> command);
        
        public GeomeryReadEvent OnVertex { get; set; }
        public GeomeryReadEvent OnTexture { get; set; }
        public GeomeryReadEvent OnNormal { get; set; }
        public FaceReadEvent    OnFace { get; set; }
    }
}
