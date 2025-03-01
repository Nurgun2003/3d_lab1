using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;
using System.Windows.Media.Media3D;

namespace lab1.My
{
    public class Octahedron : ModelVisual3D
    {
        private readonly static Brush _defaultColor = Brushes.Gray;

        public Octahedron()
        {
            Draw(_size, _pos, _frontlefttop, _frontrighttop, _frontleftbottom, _frontrightbottom, _backlefttop, _backrighttop, _backleftbottom, _backrightbottom);
        }
        //размер по умолчанию
        private double _size = 0.5;
        //поле задания размера
        public double Size
        {
            get => _size;
            set
            {
                _size = value;
                //после изменения значения поля фиксируем изменения
                Draw(_size, _pos, _frontlefttop, _frontrighttop, _frontleftbottom, _frontrightbottom, _backlefttop, _backrighttop, _backleftbottom, _backrightbottom);
            }
        }
        //положение центра октаэдра
        private Point3D _pos;
        public Point3D Position
        {
            get => _pos;
            set
            {
                _pos = value;
                //после изменения значения поля фиксируем изменения
                Draw(_size, _pos, _frontlefttop, _frontrighttop, _frontleftbottom, _frontrightbottom, _backlefttop, _backrighttop, _backleftbottom, _backrightbottom);
            }
        }
        // Материалы граней цвета граней
        // Кисть для закраски передней грани
        private Brush _frontlefttop = _defaultColor;
        //цвет передней грани
        public Brush FrontLeftTop
        {
            get => _frontlefttop;
            set
            {
                _frontlefttop = value;
                Draw(_size, _pos, _frontlefttop, _frontrighttop, _frontleftbottom, _frontrightbottom, _backlefttop, _backrighttop, _backleftbottom, _backrightbottom);
            }
        }
        private Brush _frontrighttop = _defaultColor;
        public Brush FrontRightTop
        {
            get => _frontrighttop;
            set
            {
                _frontrighttop = value;
                Draw(_size, _pos, _frontlefttop, _frontrighttop, _frontleftbottom, _frontrightbottom, _backlefttop, _backrighttop, _backleftbottom, _backrightbottom);
            }
        }
        private Brush _frontleftbottom = _defaultColor;
        public Brush FrontLeftBottom
        {
            get => _frontleftbottom;
            set
            {
                _frontleftbottom = value;
                Draw(_size, _pos, _frontlefttop, _frontrighttop, _frontleftbottom, _frontrightbottom, _backlefttop, _backrighttop, _backleftbottom, _backrightbottom);
            }
        }
        private Brush _frontrightbottom = _defaultColor;
        public Brush FrontRightBottom
        {
            get => _frontrightbottom;
            set
            {
                _frontrightbottom = value;
                Draw(_size, _pos, _frontlefttop, _frontrighttop, _frontleftbottom, _frontrightbottom, _backlefttop, _backrighttop, _backleftbottom, _backrightbottom);
            }
        }
        private Brush _backlefttop = _defaultColor;
        public Brush BackLeftTop
        {
            get => _backlefttop;
            set
            {
                _backlefttop = value;
                Draw(_size, _pos, _frontlefttop, _frontrighttop, _frontleftbottom, _frontrightbottom, _backlefttop, _backrighttop, _backleftbottom, _backrightbottom);
            }
        }
        private Brush _backrighttop = _defaultColor;
        public Brush BackRightTop
        {
            get => _backrighttop;
            set
            {
                _backrighttop = value;
                Draw(_size, _pos, _frontlefttop, _frontrighttop, _frontleftbottom, _frontrightbottom, _backlefttop, _backrighttop, _backleftbottom, _backrightbottom);
            }
        }
        private Brush _backleftbottom = _defaultColor;
        public Brush BackLeftBottom
        {
            get => _backleftbottom;
            set
            {
                _backleftbottom = value;
                Draw(_size, _pos, _frontlefttop, _frontrighttop, _frontleftbottom, _frontrightbottom, _backlefttop, _backrighttop, _backleftbottom, _backrightbottom);
            }
        }
        private Brush _backrightbottom = _defaultColor;
        public Brush BackRightBottom
        {
            get => _backrightbottom;
            set
            {
                _backrightbottom = value;
                Draw(_size, _pos, _frontlefttop, _frontrighttop, _frontleftbottom, _frontrightbottom, _backlefttop, _backrighttop, _backleftbottom, _backrightbottom);
            }
        }

        //Добавление грани
        private static GeometryModel3D AddFace(
                        Point3D point1,
                        Point3D point2,
                        Point3D point3,  //вершины грани
                        Material material)
        {
            GeometryModel3D geometryModel3D = new GeometryModel3D()
            {
                Geometry = new MeshGeometry3D()
                {
                    Positions = new Point3DCollection()
                    {
                        point1,
                        point2,
                        point3, //вершины треугольника
                    }
                },
                Material = material //цвет грани
            };
            return geometryModel3D;
        }
        private void Draw(
                        double size,
                        Point3D pos,
                        Brush frontlefttop,
                        Brush frontrighttop,
                        Brush frontleftbottom,
                        Brush frontrightbottom,
                        Brush backlefttop,
                        Brush backrighttop,
                        Brush backleftbottom,
                        Brush backrightbottom)
        {
            // Отсчёт точек по 6 направлениям.
            // Размерности граней симметричны в обе стороны в абсолютных величинах.
            double absX = size / 2;
            double absY = size / 2;
            double absZ = size / 2;
            Point3D front = new Point3D(pos.X, pos.Y, absZ + pos.Z);
            Point3D top = new Point3D(pos.X, absY + pos.Y, pos.Z);
            Point3D left = new Point3D(-absX + pos.X, pos.Y, pos.Z);
            Point3D right = new Point3D(absX + pos.X, pos.Y, pos.Z);
            Point3D bottom = new Point3D(pos.X, -absY + pos.Y, pos.Z);
            Point3D back = new Point3D(pos.X, pos.Y, -absZ + pos.Z);
            Model3DGroup m3dg = new Model3DGroup();
            // Добавление граней
            // 1 Передняя левая верхняя
            DiffuseMaterial material = new DiffuseMaterial(frontlefttop);
            GeometryModel3D faceFrontLeftTop = AddFace(front, top, left, material);
            m3dg.Children.Add(faceFrontLeftTop);
            // 2 Передняя правая верхняя
            material = new DiffuseMaterial(frontrighttop);
            GeometryModel3D faceFrontRightTop = AddFace(front, right, top, material);
            m3dg.Children.Add(faceFrontRightTop);
            // 3 Передняя левая нижняя
            material = new DiffuseMaterial(frontleftbottom);
            GeometryModel3D faceFrontLeftBottom = AddFace(front, left, bottom, material);
            m3dg.Children.Add(faceFrontLeftBottom);
            // 4 Передняя правая нижняя
            material = new DiffuseMaterial(frontrightbottom);
            GeometryModel3D faceFrontRightBottom = AddFace(front, bottom, right, material);
            m3dg.Children.Add(faceFrontRightBottom);
            // 5 Задняя левая верхняя
            material = new DiffuseMaterial(backlefttop);
            GeometryModel3D faceBackLeftTop = AddFace(back, left, top, material);
            m3dg.Children.Add(faceBackLeftTop);
            // 6 Задняя правая верхняя
            material = new DiffuseMaterial(backrighttop);
            GeometryModel3D faceBackRightTop = AddFace(back, top, right, material);
            m3dg.Children.Add(faceBackRightTop);
            // 7 Задняя левая нижняя
            material = new DiffuseMaterial(backleftbottom);
            GeometryModel3D faceBackLeftBottom = AddFace(back, left, bottom, material);
            m3dg.Children.Add(faceBackLeftBottom);
            // 8 Задняя правая нижняя
            material = new DiffuseMaterial(backrightbottom);
            GeometryModel3D faceBackRightBottom = AddFace(back, bottom, right, material);
            m3dg.Children.Add(faceBackRightBottom);
            //Сохранение данных объекта
            Content = m3dg;
        }
    }
}
