using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventHandling.Objects {
	internal class Marker : BaseObject {
		public bool isExist;
		public Marker(float x, float y, float r, float angle) : base(x, y, angle) {
			this.r = r;
			this.isExist = false;
		}

		public override void Render(Graphics g) {
			if (inShadow) {
				color = Color.White;
			} else {
				color = Color.Red;
			}

			g.DrawEllipse(
				new Pen(color, 2),
				-r, -r,
				r*2, r*2
			);
			g.DrawEllipse(
				new Pen(color, 2),
				-r/2f, -r/2f,
				r, r
			);
			g.DrawEllipse(
				new Pen(color, 2),
				-r/ 5f, -r/5f,
				r/2.5f, r/2.5f
			);
		}
	}
}
