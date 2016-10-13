/*
 * Created by SharpDevelop.
 * User: А.Скрипкин
 * Date: 13.10.2016
 * Time: 9:25
 */
using System;

namespace GLCapsule.ObjLoader
{
    /// <summary>
    /// Description of IndexTriple.
    /// </summary>
    public class IndexTriple
    {
        public IndexTriple(string triple)
        {
        }
        
        public int Iv { get; private set; }
        public int It { get; private set; }
        public int In { get; private set; }
    }
}
