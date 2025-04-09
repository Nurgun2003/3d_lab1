using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Media3D;

namespace lab1.My
{
    public class TruncatedPyramid : ModelVisual3D
    {
        public TruncatedPyramid()
        {
            Draw(_sideCount, _radius, _height, _truncate);
        }
        private short _sideCount = 4;
        public short SideCount
        {
            get => _sideCount;
            set
            {
                _sideCount = Math.Max(value, (short)3);
                Draw(_sideCount, _radius, _height, _truncate);
            }
        }
        private double _radius;
        public double Radius
        {
            get => _radius;
            set
            {
                _radius = value;
                Draw(_sideCount, _radius, _height, _truncate);
            }
        }
        private double _height;
        public double Height
        {
            get => _height;
            set
            {
                _height = value;
                Draw(_sideCount, _radius, _height, _truncate);
            }
        }
        private double _truncate;
        public double Truncate
        {
            get => _truncate;
            set
            {
                _truncate = value;
                Draw(_sideCount, _radius, _height, _truncate);
            }
        }
        private Brush _color;
        public Brush Color
        {
            get => _color;
            set
            {
                _color = value;
                Draw(_sideCount, _radius, _height, _truncate);
            }
        }

        //Добавление грани
        private static GeometryModel3D AddFace(Point3D[] points, Material material)
        {
            Point3DCollection positions = new Point3DCollection();
            for (int i = 2; i < points.Length; i++)
            {
                positions.Add(points[0]);
                positions.Add(points[i-1]);
                positions.Add(points[i]);
            }
            GeometryModel3D geometryModel3D = new GeometryModel3D()
            {
                Geometry = new MeshGeometry3D()
                {
                    Positions = positions
                },
                Material = material
            };
            return geometryModel3D;
        }
        private void Draw(short sideCount, double radius, double height, double truncate)
        {
            Point3D[] points = new Point3D[sideCount*2];
            for (int i = 0; i < sideCount; i++)
            {
                points[i * 2] = new Point3D(Math.Cos(2 * Math.PI / sideCount * i) * radius, 0, Math.Sin(2 * Math.PI / sideCount * i) * radius);
                points[i * 2 + 1] = new Point3D(Math.Cos(2 * Math.PI / sideCount * i) * radius * truncate, height * (1 - truncate), Math.Sin(2 * Math.PI / sideCount * i) * radius * truncate);
            }
            Model3DGroup m3dg = new Model3DGroup();
            // Добавление граней
            // Нижнее основание
            DiffuseMaterial material = new DiffuseMaterial(RandBrush());
            GeometryModel3D face = AddFace(points.Where((p, i) => i % 2 == 0).ToArray(), material);
            m3dg.Children.Add(face);
            // Верхнее основание
            material = new DiffuseMaterial(RandBrush());
            face = AddFace(points.Where((p, i) => i % 2 == 1).Reverse().ToArray(), material);
            m3dg.Children.Add(face);
            // Боковые грани
            for (int i = 0; i < sideCount; i++)
            {
                material = new DiffuseMaterial(RandBrush());
                face = AddFace(new Point3D[]
                    {
                        points[i*2],
                        points[i*2+1],
                        points[i+1 < sideCount ? (i+1)*2+1 : 1],
                        points[i+1 < sideCount ? (i+1)*2 : 0]
                    },
                    material
                );
                m3dg.Children.Add(face);
            }
            //Сохранение данных объекта
            Content = m3dg;
        }
        private Random rnd = new Random();
        private Brush RandBrush()
        {
            if (Color != null)
                return Color;
            return new SolidColorBrush(System.Windows.Media.Color.FromRgb((byte)rnd.Next(), (byte)rnd.Next(), (byte)rnd.Next()));
        }
    }
}
