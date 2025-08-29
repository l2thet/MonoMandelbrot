using Microsoft.Xna.Framework;

namespace MonoMandelbrot
{
    public class DrawingSurface
    {
        protected private GraphicsDeviceManager gdm;

        public Vector2 tl = new Vector2(-2.0f, 2.0f);
        public Vector2 br = new Vector2(2.0f, -2.0f);

        public Vector2 tl0;
        public Vector2 br0;

        public float xscale = 0f;
        public float yscale = 0f;
        public float width = 0f;
        public float height = 0f;

        public DrawingSurface(GraphicsDeviceManager gdm)
        {
            this.tl0 = tl;
            this.br0 = br;
            this.gdm = gdm;
            this.init_viewport();
        }

        public void init_viewport()
        {
            this.width = this.br.X - this.tl.X;
            this.height = this.tl.Y - this.br.Y;
            this.xscale = this.gdm.PreferredBackBufferWidth / (this.br.X - this.tl.X);
            this.yscale = this.gdm.PreferredBackBufferHeight / (this.tl.Y - this.br.Y);
        }

        public Vector2 canvas_to_logical(float x, float y)
        {
            return new Vector2(
                x / this.xscale + this.tl.X,
                (this.tl.Y - this.br.Y) - (y / this.yscale - this.br.Y)
            );
        }

        public void zoom(Vector2 center, float z)
        {
            var dx = this.width * z;
            var dy = this.height * z;
            this.tl = new Vector2(center.X - this.width / 2 + dx, center.Y + this.height / 2 - dy);
            this.br = new Vector2(center.X + this.width / 2 - dx, center.Y - this.height / 2 + dy);
            this.init_viewport();
        }

        public void reset_view()
        {
            this.tl = this.tl0;
            this.br = this.br0;
            this.init_viewport();
        }
    }
}
