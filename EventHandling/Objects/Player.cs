using System;
using System.CodeDom;
using System.Collections.Generic;
using System.DirectoryServices.ActiveDirectory;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace EventHandling.Objects {
	internal class Bullet : BaseObject {
		public float dx;
		public float dy;

		public Bullet(float x, float y, float r, float angle) : base(x, y, angle) {
			this.r = r;
		}

		public bool OverlapsCircle(BaseObject obj) {
			float tempDx = obj.x - this.x;
			float tempDy = obj.y - this.y;

			float length = MathF.Sqrt(tempDx * tempDx + tempDy * tempDy);
			if (length < this.r + obj.r) {
				return true;
			}

			return false;
		}

		public override void Render(Graphics g) {
			if (inShadow) {
				color = Color.Yellow;
			} else {
				color = Color.Orange;
			}
			g.FillEllipse(
				new SolidBrush(color),
				-r, -r,
				r*2, r*2
			);
			g.DrawEllipse(
				new Pen(Color.Black, 2),
				-r, -r,
				r * 2, r * 2
			);
		}
	}
	internal class Player : BaseObject {
		public float dx;
		public float dy;
		public float vX;
		public float vY;
		public Bullet bullet;

		public Player(float x, float y, float r, float angle) : base(x, y, angle) {
			this.r = r;
			this.dx = 0;
			this.dy = 0;
			this.vX = 0;
			this.vY = 0;
		}

		public bool OverlapsCircle(BaseObject obj) {
			this.dx = obj.x - this.x;
			this.dy = obj.y - this.y;

			float length = MathF.Sqrt(dx * dx + dy * dy);
			if (length < this.r + obj.r) {
				return true;
			}

			return false;
		}

		public void Shoot() {
			bullet = new Bullet(this.x, this.y, 7.5f, 0);
			bullet.dx = this.dx == 0 ? MathF.Cos((this.angle * MathF.PI) / 180) : this.dx;
			bullet.dy = this.dy == 0 ? MathF.Sin((this.angle * MathF.PI) / 180) : this.dy;
		}

		public override void Render(Graphics g) {
			if (inShadow) {
				color = Color.White;
				g.DrawImage(Image.FromFile(Environment.CurrentDirectory + @$"\src\tank_shadow.png"), new Point((int)-r, (int)-r));
			} else {
				color = Color.DeepSkyBlue;
				g.DrawImage(Image.FromFile(Environment.CurrentDirectory + @$"\src\tank.png"), new Point((int)-r, (int)-r));
			}
		}
	}
}
