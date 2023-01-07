using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.AccessControl;
using System.Text;
using System.Threading.Tasks;

namespace EventHandling.Objects {
	internal class Shadow : BaseObject {
		public float width;
		public float height;

		public Shadow(float x, float y, float width, float height, float angle) : base(x, y, angle) {
			this.width = width;
			this.height = height;
		}

		public bool OverlapsRect(BaseObject obj) {
			if (obj.x + obj.r > x && obj.x - obj.r < x + width) {
				return true;
			}

			return false;
		}

		public override void Render(Graphics g) {
			g.FillRectangle(
				new SolidBrush(Color.Black),
				x, y,
				width, height
			);
		}
	}
}
