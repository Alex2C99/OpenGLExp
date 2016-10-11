/*
 * Created by SharpDevelop.
 * User: А.Скрипкин
 * Date: 11.10.2016
 * Time: 10:00
 */
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using NLog;

namespace GLCapsule
{
    /// <summary>
    /// Description of ObjLoader.
    /// </summary>
    public class ObjFile : IEnumerable<Vertex>
    {
        
        Logger logger = LogManager.GetCurrentClassLogger();
        
        private class VertexEnumerator : IEnumerator<Vertex>
        {
            
            private ObjFile file;
            private int currentFace;
            
            public VertexEnumerator(ObjFile file)
            {
                this.file = file;
                currentFace = 0;
            }
            
            #region IEnumerator implementation
            public bool MoveNext()
            {
                throw new NotImplementedException();
            }
            public void Reset()
            {
                throw new NotImplementedException();
            }

            object IEnumerator.Current { get { return Current; } }

            #endregion
            #region IDisposable implementation
            public void Dispose()
            {
                throw new NotImplementedException();
            }
            #endregion
            #region IEnumerator implementation
            public Vertex Current { get {  throw new NotImplementedException(); } }
            #endregion
        }
        
        private class ObjIndex
        {
            private readonly uint vi;
            private readonly uint ti;
            private readonly uint ni;

            public ObjIndex(uint vi, uint ti, uint ni)
            {
                this.vi = vi;
                this.ti = ti;
                this.ni = ni;
            }
            
            public uint Vi { get { return vi; } }

            public uint Ti { get { return ti; } }

            public uint Ni { get { return ni; } }
        }

        private class ObjFloatArray
        {
            private readonly float[] data;
            
            ObjFloatArray(float[] dt)
            {
                data = new float[dt.Length];
                dt.CopyTo(data,0);
            }

            public float[] Data { get { return data; } }
        }
        
        private class ObjIndexArray
        {
            private readonly ObjIndex[] data;
            
            ObjIndexArray(ObjIndex[] dt)
            {
                data = new ObjIndex[dt.Length];
                dt.CopyTo(data,0);
            }

            public ObjIndex[] Data { get { return data; } }
        }

        private List<ObjIndexArray>  faces;
        private List<ObjFloatArray>  vertexes;
        private List<ObjFloatArray>  textures;
        private List<ObjFloatArray>  normals;
        
        private delegate void ParseString(string str);
        private Dictionary<string,ParseString> parseMethods;

        public ObjFile(string fn)
        {
            parseMethods["v"] = ParseVertex;
            parseMethods["vt"] = ParseTexture;
            parseMethods["vn"] = ParseNormal;
            parseMethods["f"] = ParseFace;
            
            string str;
            using(var fi = new StreamReader(fn))
            {
                while(null!=(str = fi.ReadLine().Trim(' ','\t','\r','\n')))
                {
                    string key = str.Split(' ','\t')[0].Trim(' ','\t','\r','\n');
                    if(parseMethods.ContainsKey(key))
                        parseMethods[key](str);
                    else
                        logger.Info(string.Format("Unimplemented key {0} in file {1}",key, fn));
                }
            }
        }
               
        #region IEnumerable implementation
        public IEnumerator<Vertex> GetEnumerator()
        {
            return new VertexEnumerator(this);
        }
        #endregion
        #region IEnumerable implementation
        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        {
            return this.GetEnumerator();
        }
        #endregion
        
        private void ParseVertex(string s)
        {
            
        }

        private void ParseTexture(string s)
        {
            
        }
        
        private void ParseNormal(string s)
        {
            
        }
        
        private void ParseFace(string s)
        {
            
        }

    }
}
