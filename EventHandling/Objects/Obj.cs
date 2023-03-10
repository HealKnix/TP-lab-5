namespace EventHandling.Objects {
	internal class Obj : BaseObject {
		public float originR;

		public Obj(float x, float y, float r, float angle) : base(x, y, angle) {
			this.r = r;
			this.originR = r;
		}

		public void Update(Player player, PictureBox pbMain, RichTextBox richTextBox) {
			if (player.OverlapsCircle(this)) {
				SetPos(
					new Random().Next((int)this.originR, (int)(pbMain.Width - this.originR)),
					new Random().Next((int)this.originR, (int)(pbMain.Height - this.originR))
				);
				if (player.inShadow) {
					Form1.score += 2;
				} else {
					Form1.score++;
				}
				richTextBox.Text += $"[{DateTime.Now:HH:mm:ss:ff}]\nИгрок пересекся с {this}\n";
			}
			for (int i = 0; i < player.bullets.Count; i++) {
				if (player.bullets[i].OverlapsCircle(this)) {
					this.SetPos(
						new Random().Next((int)this.originR, (int)(pbMain.Width - this.originR)),
						new Random().Next((int)this.originR, (int)(pbMain.Height - this.originR))
					);
					if (player.inShadow) {
						Form1.score += 2;
					} else {
						Form1.score++;
					}
					richTextBox.Text += $"[{DateTime.Now:HH:mm:ss:ff}]\nПуля пересеклась с {this}\n";
				}
			}

			this.r -= 0.15f;
			if (this.r < 0) {
				SetPos(
					new Random().Next((int)this.originR, (int)(pbMain.Width - this.originR)),
					new Random().Next((int)this.originR, (int)(pbMain.Height - this.originR))
				);
				richTextBox.Text += $"[{DateTime.Now:HH:mm:ss:ff}]\n{this} обновился\n";
			}
		}

		public new void SetPos(float x, float y) {
			this.x = x;
			this.y = y;
			this.r = this.originR;
		}

		public override void Render(Graphics g) {
			if (this.inShadow) {
				this.color = Color.White;
			} else {
				this.color = Color.Green;
			}

			g.FillEllipse(
				new SolidBrush(this.color),
				this.x - this.r, this.y - this.r,
				this.r *2, this.r *2
			);
			g.DrawString(
				((int)this.r).ToString(),
				new Font("Verdana", 8),
				new SolidBrush(Color.Green),
				this.x + this.r, this.y + this.r
			);
		}
	}
}
