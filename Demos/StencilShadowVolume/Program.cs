﻿using CSharpGL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace StencilShadowVolume
{
    static class Program
    {
        /// <summary>
        /// 应用程序的主入口点。
        /// </summary>
        [STAThread]
        static void Main(string[] args)
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            string filename = string.Empty;
            //if (args == null || args.Length < 1) { filename = "dragon.obj"; }
            //if (args == null || args.Length < 1) { filename = "buddha.obj"; }
            if (args == null || args.Length < 1)
            {
                string folder = System.Windows.Forms.Application.StartupPath;
                filename = System.IO.Path.Combine(folder, "bunny.obj");
            }

            var parser = new ObjVNFParser(true);
            ObjVNFResult result = parser.Parse(filename);
            if (result.Error != null)
            {
                MessageBox.Show(result.Error.ToString());
            }
            else
            {
                IModelProvider provider = new AdjacentTriangleModelProvider(result.Mesh);
                var info = new ModelInfo(provider, AdjacentTriangleModel.strPosition, AdjacentTriangleModel.strNormal, provider.Size);
                //Application.Run(new FormAdjacentTriangles(info));
                Application.Run(new Form0SilhouetteDetection(info));
                Application.Run(new Form1ExtrudeVolume(info));
                Application.Run(new Form2ShadowVolume(info));
            }
        }
    }

    public class AdjacentTriangleModelProvider : IModelProvider
    {
        private ObjVNFMesh mesh;

        public AdjacentTriangleModelProvider(ObjVNFMesh mesh)
        {
            this.mesh = mesh;
        }
        #region IModelProvider 成员

        public IBufferSource Model
        {
            get { return new AdjacentTriangleModel(mesh); }
        }

        public vec3 Size
        {
            get { return this.mesh.Size; }
        }

        #endregion
    }
    public interface IModelProvider
    {
        IBufferSource Model { get; }

        vec3 Size { get; }
    }

    public class ModelInfo
    {
        public readonly IModelProvider modelProvider;
        public readonly string position;
        public readonly string normal;
        public readonly vec3 size;

        public ModelInfo(IModelProvider modelProvider, string position, string normal, vec3 size)
        {
            this.modelProvider = modelProvider;
            this.position = position;
            this.normal = normal;
            this.size = size;
        }
    }
}
