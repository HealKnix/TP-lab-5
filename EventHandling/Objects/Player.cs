using System.Media;
using System.Numerics;

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
			if (this.inShadow) {
				this.color = Color.Yellow;
			} else {
				this.color = Color.Orange;
			}
			g.FillEllipse(
				new SolidBrush(this.color),
				-this.r, -this.r,
				this.r *2, this.r *2
			);
			g.DrawEllipse(
				new Pen(Color.Black, 2),
				-this.r, -this.r,
				this.r * 2, this.r * 2
			);
		}
	}
	internal class Player : BaseObject {
		public float dx;
		public float dy;
		public float vX;
		public float vY;
		public bool shoot = true;
		public float shootDelay = 5f;
		public List<Bullet> bullets = new List<Bullet>();

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

			float length = MathF.Sqrt(this.dx * this.dx + this.dy * this.dy);
			if (length < this.r + obj.r) {
				return true;
			}

			return false;
		}

		public void Update(Marker marker, Shadow shadow, PictureBox pbMain) {
			this.inShadow = shadow.OverlapsRect(this);
			if (shootDelay <= 0) {
				this.shoot = true;
				this.shootDelay = 5f;
			} else if (!shoot) {
				this.shootDelay -= 0.1f;
			}

			this.dx = marker.x - this.x;
			this.dy = marker.y - this.y;

			float length = MathF.Sqrt(this.dx * this.dx + this.dy * this.dy);

			if (length > marker.r / 2) {
				this.dx /= length;
				this.dy /= length;

				this.vX += this.dx * 0.5f;
				this.vY += this.dy * 0.5f;

				this.angle = 90 - MathF.Atan2(this.vX, this.vY) * 180 / MathF.PI;
			} else {
				marker.isExist = false;
				marker.x = this.x;
				marker.y = this.y;
				this.dx = 0;
				this.dy = 0;
			}

			for (int i = 0; i < bullets.Count; i++) {
				this.bullets[i].x += this.bullets[i].dx * 15f;
				this.bullets[i].y += this.bullets[i].dy * 15f;

				if (this.bullets[i].x >= pbMain.Width || this.bullets[i].x + this.bullets[i].r <= 0) {
					bullets.Remove(bullets[i]);
					break;
				} else if (this.bullets[i].y <= 0 || this.bullets[i].y + this.bullets[i].r >= pbMain.Height) {
					bullets.Remove(bullets[i]);
					break;
				}
			}

			this.vX += -this.vX * 0.1f;
			this.vY += -this.vY * 0.1f;

			this.x += this.vX;
			this.y += this.vY;

			if (this.x - this.r < 0) {
				this.x = this.r;
			} else if (this.x + this.r > pbMain.Width) {
				this.x = pbMain.Width - this.r;
			}

			if (this.y - this.r < 0) {
				this.y = this.r;
			} else if (this.y + this.r > pbMain.Height) {
				this.y = pbMain.Height - this.r;
			}
		}

		public void Shoot() {
			if (this.shoot) {
				this.shoot = false;
				bullets.Add(new Bullet(this.x, this.y, 7.5f, 0) {
					dx = this.dx == 0 ? MathF.Cos((this.angle * MathF.PI) / 180) : this.dx,
					dy = this.dy == 0 ? MathF.Sin((this.angle * MathF.PI) / 180) : this.dy
				});
				SoundPlayer soundPlayer = new SoundPlayer("shoot.wav");
				soundPlayer.Play();
			}
		}

		public override void Render(Graphics g) {
			if (this.inShadow) {
				this.color = Color.White;
				g.DrawImage(Image.FromFile(Environment.CurrentDirectory + @$"\src\tank_shadow.png"), new Point((int)-r, (int)-r));
			} else {
				this.color = Color.DeepSkyBlue;
				g.DrawImage(Image.FromFile(Environment.CurrentDirectory + @$"\src\tank.png"), new Point((int)-r, (int)-r));
			}
			if (!this.shoot) {
				g.DrawString(
					((int)this.shootDelay).ToString(),
					new Font("Verdana", 8),
					new SolidBrush(Color.Green),
					this.r, this.r
				);
			}
		}
	}
}
