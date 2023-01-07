namespace EventHandling.Objects {
	internal class Shadow : BaseObject {
		public float width;
		public float height;

		public Shadow(float x, float y, float width, float height, float angle) : base(x, y, angle) {
			this.width = width;
			this.height = height;
		}

		public void Update(PictureBox pbMain) {
			if (this.x > pbMain.Width) {
				this.x = -this.width;
			}
			this.x += 2;
		}

		public bool OverlapsRect(BaseObject obj) {
			if (obj.x + obj.r > this.x && obj.x - obj.r < this.x + this.width) {
				return true;
			}
			return false;
		}

		public override void Render(Graphics g) {
			g.FillRectangle(
				new SolidBrush(Color.Black),
				this.x, this.y,
				this.width, this.height
			);
		}
	}
}
