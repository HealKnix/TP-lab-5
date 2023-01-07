using System.Drawing.Drawing2D;

namespace EventHandling.Objects {
	internal class BaseObject {
		public float x;
		public float y;
		public float originR;
		public float r;
		public bool inShadow;
		public float angle;
		public Color color;

		public BaseObject(float x, float y, float angle) {
			this.x = x;
			this.y = y;
			this.angle = angle;
			this.inShadow = false;
			this.color = Color.Yellow;
		}

		public void SetPos(float x, float y) {
			this.x = x;
			this.y = y;
		}

		public Matrix GetTransform() {
			Matrix matrix = new Matrix();
			matrix.Translate(x, y);
			matrix.Rotate(angle);

			return matrix;
		}

		public virtual void Render(Graphics g) {}
	}
}
