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
        
        public void Load()
        {
            
        }
        
        public delegate void GeomeryReadEvent(ObjCommand<float> command);
        public delegate void StringReadEvent(ObjCommand<string> command);
        public delegate void IndexReadEvent(ObjCommand<IndexTriple> command);
        
        public GeomeryReadEvent OnGeometryCommand { get; set; }
        public IndexReadEvent   OnIndexCommand { get; set; }
        public StringReadEvent  OnStringCommand { get; set; }
    }
}
