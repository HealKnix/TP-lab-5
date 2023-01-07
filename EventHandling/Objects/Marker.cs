namespace EventHandling.Objects {
	internal class Marker : BaseObject {
		public bool isExist;

		public Marker(float x, float y, float r, float angle) : base(x, y, angle) {
			this.r = r;
			this.isExist = false;
		}

		public new void SetPos(float x, float y) {
			this.isExist = true;
			this.x = x;
			this.y = y;
		}

		public override void Render(Graphics g) {
			if (inShadow) {
				this.color = Color.White;
			} else {
				this.color = Color.Red;
			}

			g.DrawEllipse(
				new Pen(this.color, 2),
				-this.r, -this.r,
				this.r *2, this.r *2
			);
			g.DrawEllipse(
				new Pen(this.color, 2),
				-this.r /2f, -this.r /2f,
				this.r, this.r
			);
			g.DrawEllipse(
				new Pen(this.color, 2),
				-this.r / 5f, -this.r /5f,
				this.r /2.5f, this.r /2.5f
			);
		}
	}
}
