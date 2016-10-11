/*
 * Created by SharpDevelop.
 * User: А.Скрипкин
 * Date: 11.10.2016
 * Time: 10:00
 */
using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using NLog;
using OpenTK;

namespace GLCapsule
{
    /// <summary>
    /// Description of ObjLoader.
    /// </summary>
    public class ObjFile 
    {
        
        Logger logger = LogManager.GetCurrentClassLogger();
        
        private readonly char[] DELIM = {' ','\t','\r','\n'};
        
        private class ObjIndex
        {
            private readonly int vi;
            private readonly int ti;
            private readonly int ni;

            public ObjIndex(int vi, int ti, int ni)
            {
                this.vi = vi;
                this.ti = ti;
                this.ni = ni;
                this.VNumber = 0;
            }
            
            public int Vi { get { return vi; } }

            public int Ti { get { return ti; } }

            public int Ni { get { return ni; } }

            public UInt32 VNumber  { get; set; }
        }

        private class ObjFloatArray
        {
            private readonly float[] data;
            
            public ObjFloatArray(float[] dt)
            {
                data = new float[dt.Length];
                dt.CopyTo(data,0);
            }
            
            public Vector3 Vec3 
            { 
            	get 
            	{
            		return new Vector3(data[0],data[1],data[2]);
            	}
            }

            public Vector2 Vec2 
            { 
            	get 
            	{
            		return new Vector2(data[0],data[1]);
            	}
            }

            public Vector4 Vec4 
            { 
            	get 
            	{
            		return new Vector4(data[0],data[1],data[2],data[3]);
            	}
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
         
        public void GetGeometry(out Vertex[] vertx, out UInt32[] idxs)
        {
        	var vList = new List<Vertex>();
        	var iList = new List<UInt32>();
        	UInt32 counter = 0;
        	foreach(ObjIndexArray iarr in faces)
        	{
        		foreach(ObjIndex oi in iarr.Data)
        		{
        			++counter;
        			vList.Add(new Vertex { Xyz = vertexes[oi.Vi-1].Vec3, Uv = textures[oi.Ti-1].Vec2} ); //, Norm = normals[oi.Ni-1].Vec3 } );
        			oi.VNumber = counter;
        			iList.Add(oi.VNumber);
        		}
        	}
        	
        	vertx = vList.ToArray();
        	idxs = iList.ToArray();
        }
        
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
                dt[i-idx] = ((float.TryParse(arr[i],NumberStyles.AllowLeadingSign|NumberStyles.AllowDecimalPoint,CultureInfo.GetCultureInfo("en-US"),out t))) ? t : 0.0f;
            return dt;
        }

        private static ObjIndex[] ConvertArrayToIndex(string[] arr, uint idx)
        {
            ObjIndex[] dt = new ObjIndex[arr.Length-idx];
            int[] v = new int[3];
            for(uint i=0; i<3; i++)
                v[i] = 0;
            for(uint i=idx; i<arr.Length; i++)
            {
                string[] sv = arr[i].Split('/');
                int t = 0;
                for(uint j=0; j<sv.Length && j<3; j++)
                    v[j] = (int.TryParse(sv[j],out t)) ? t : 0;
                dt[i-idx] = new ObjIndex(v[0],v[1],v[2]);
            }
            return dt;
        }
    }
}
