using System;
using System.Collections.Generic;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

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
			color = Color.Yellow;
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
