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
        
        private readonly char[] DELIM = {' ','\t','\r','\n'};
        
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
                this.VNumber = 0;
            }
            
            public uint Vi { get { return vi; } }

            public uint Ti { get { return ti; } }

            public uint Ni { get { return ni; } }

            public uint VNumber  { get; set; }
        }

        private class ObjFloatArray
        {
            private readonly float[] data;
            
            public ObjFloatArray(float[] dt)
            {
                data = new float[dt.Length];
                dt.CopyTo(data,0);
            }

            public float[] Data { get { return data; } }
        }
        
        private class ObjIndexArray
        {
            private readonly ObjIndex[] data;
            
            public ObjIndexArray(ObjIndex[] dt)
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
            
            faces = new List<ObjIndexArray>();
            vertexes = new List<ObjFloatArray>();
            textures = new List<ObjFloatArray>();
            normals = new List<ObjFloatArray>();
            
            parseMethods = new Dictionary<string, ParseString>();
            parseMethods["v"] = ParseVertex;
            parseMethods["vt"] = ParseTexture;
            parseMethods["vn"] = ParseNormal;
            parseMethods["f"] = ParseFace;
            
            string str = "";
            using(var fi = new StreamReader(fn))
            {
                while(!fi.EndOfStream)
                {
                    str = fi.ReadLine().Trim();
                    str = DelComment(str);
                    if(str.Length==0)
                        continue;
                    string key = str.Split(DELIM,StringSplitOptions.RemoveEmptyEntries)[0].Trim();
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
            string[] arr = s.Split(DELIM,StringSplitOptions.RemoveEmptyEntries);
            if(arr.Length<4)
            {
                logger.Error("Vertex data too small");
                return;
            }
            vertexes.Add(new ObjFloatArray(ConvertArrayToFloat(arr,1)));
        }

        private void ParseTexture(string s)
        {
            string[] arr = s.Split(DELIM,StringSplitOptions.RemoveEmptyEntries);
            if(arr.Length<3)
            {
                logger.Error("Texture data too small");
                return;
            }
            textures.Add(new ObjFloatArray(ConvertArrayToFloat(arr,1)));
        }
        
        private void ParseNormal(string s)
        {
            string[] arr = s.Split(DELIM,StringSplitOptions.RemoveEmptyEntries);
            if(arr.Length<4)
            {
                logger.Error("Normal data too small");
                return;
            }
            normals.Add(new ObjFloatArray(ConvertArrayToFloat(arr,1)));
        }
        
        private void ParseFace(string s)
        {
            string[] arr = s.Split(DELIM,StringSplitOptions.RemoveEmptyEntries);
            if(arr.Length<4)
            {
                logger.Error("Facet data too small");
                return;
            }
            faces.Add(new ObjIndexArray(ConvertArrayToIndex(arr,1)));
            
        }
        
        private static string DelComment(string s)
        {
            return (s.IndexOf('#')!=-1) ? s.Substring(0,s.IndexOf('#')).Trim() : s;
        }
        
        private static float[] ConvertArrayToFloat(string[] arr, uint idx)
        {
            float[] dt = new float[arr.Length-idx];
            float t;
            for(uint i=idx; i<arr.Length; i++)
                dt[i-idx] = ((float.TryParse(arr[i],out t))) ? t : 0.0f;
            return dt;
        }

        private static ObjIndex[] ConvertArrayToIndex(string[] arr, uint idx)
        {
            ObjIndex[] dt = new ObjIndex[arr.Length-idx];
            uint[] v = new uint[3];
            for(uint i=0; i<3; i++)
                v[i] = 0;
            for(uint i=idx; i<arr.Length; i++)
            {
                string[] sv = arr[i].Split('/');
                uint t = 0;
                for(uint j=0; j<sv.Length && j<3; j++)
                    v[j] = (uint.TryParse(sv[j],out t)) ? t : 0;
                dt[i-idx] = new ObjIndex(v[0],v[1],v[2]);
            }
            return dt;
        }
    }
}
