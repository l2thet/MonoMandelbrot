using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Threading.Tasks;
using Color = Microsoft.Xna.Framework.Color;

namespace MonoMandelbrot
{
    public class MandelbrotRenderer : DrawingSurface
    {
        private readonly int _width;
        private readonly int _height;
        private Texture2D _texture;

        private const float MAXCOLOR = 255.0f;
        private const int SETLIMIT = 4;
        private const int MAXITERATIONS = 3000;

        public Texture2D Texture => _texture;

        public MandelbrotRenderer(GraphicsDeviceManager gdm, int width, int height) : base(gdm)
        {
            _width = width;
            _height = height;
            _texture = new Texture2D(gdm.GraphicsDevice, width, height);
            GenerateFractal();
        }

        private int MandelbrotCalculation(Vector2 start)
        {
            var squared = new Vector2(start.X, start.Y);
            var count = 0;
            while (count++ < MAXITERATIONS)
            {

                float x2 = squared.X * squared.X;
                float y2 = squared.Y * squared.Y;

                if ((x2 + y2) > SETLIMIT)
                { // not in the set 
                    return count;
                }

                // square of a complex (x + yi) = (x*x - y*y, 2*x*y)
                var sx = x2 - y2;
                var sy = (2 * squared.X * squared.Y);

                // add original point 
                squared.X = sx + start.X;
                squared.Y = sy + start.Y;
            }

            return 0;
        }

        public void GenerateFractal()
        {
            Color[] data = new Color[_width * _height];

            ParallelOptions options = new ParallelOptions { MaxDegreeOfParallelism = Environment.ProcessorCount };
            Parallel.For(0, _height, y =>
            {
                for (int x = 0; x < _width; x++)
                {
                    int iterations = MandelbrotCalculation(canvas_to_logical(x, y));

                    if (iterations == 0)
                    {
                        data[y * _width + x] = Color.Black;
                        continue;
                    }

                    float t = iterations / MAXCOLOR;
                    int r = (int)(9 * (1 - t) * t * t * t * MAXCOLOR);
                    int g = (int)(15 * (1 - t) * (1 - t) * t * t * MAXCOLOR);
                    int b = (int)(8.5 * (1 - t) * (1 - t) * (1 - t) * t * MAXCOLOR);

                    data[y * _width + x] = new Color(r, g, b);
                }
            });

            _texture.SetData(data);
        }
    }
}
