using System;
using System.Collections.Generic;
using System.DirectoryServices.ActiveDirectory;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventHandling.Objects {
	internal class Obj : BaseObject {
		public Obj(float x, float y, float r, float angle) : base(x, y, angle) {
			this.r = r;
			this.originR = r;
		}

		public virtual void ReSpawn(float width, float height) {
			this.x = (new Random()).Next((int)this.r * 2, (int)(width - this.r * 2));
			this.y = (new Random()).Next((int)this.r * 2, (int)(height - this.r * 2));
			this.r = this.originR;
		}

		public override void Render(Graphics g) {
			if (inShadow) {
				color = Color.White;
			} else {
				color = Color.Green;
			}

			g.FillEllipse(
				new SolidBrush(color),
				x - r, y - r,
				r*2, r*2
			);
			g.DrawString(
				((int)r).ToString(),
				new Font("Verdana", 8), // шрифт и размер
				new SolidBrush(Color.Green), // цвет шрифта
				x + r, y + r // точка в которой нарисовать текст
			);
		}
	}
}
