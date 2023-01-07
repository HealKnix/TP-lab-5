namespace EventHandling.Objects {
	internal class Obj : BaseObject {
		public Obj(float x, float y, float r, float angle) : base(x, y, angle) {
			this.r = r;
			this.originR = r;
		}

		public void Update(Player player, PictureBox pbMain, Label label, RichTextBox richTextBox) {
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
				label.Text = $"Очки: {Form1.score}";
				richTextBox.Text += $"[{DateTime.Now:HH:mm:ss:ff}]\nИгрок пересекся с {this}\n";
			}
			if (player.bullet != null && player.bullet.OverlapsCircle(this)) {
				this.SetPos(
					new Random().Next((int)this.originR, (int)(pbMain.Width - this.originR)),
					new Random().Next((int)this.originR, (int)(pbMain.Height - this.originR))
				);
				if (player.inShadow) {
					Form1.score += 2;
				} else {
					Form1.score++;
				}
				label.Text = $"Очки: {Form1.score}";
				richTextBox.Text += $"[{DateTime.Now:HH:mm:ss:ff}]\nПуля пересеклась с {this}\n";
			}

			this.r -= 0.15f;

			if (this.r < 0) {
				SetPos(
					new Random().Next((int)this.originR, (int)(pbMain.Width - this.originR)),
					new Random().Next((int)this.originR, (int)(pbMain.Height - this.originR))
				);
				// richTextBox1.Text += $"[{DateTime.Now:HH:mm:ss:ff}]\n{this} обновился\n";
			}
		}

		public new void SetPos(float x, float y) {
			this.x = x;
			this.y = y;
			this.r = this.originR;
		}

		public virtual void ReSpawn(float width, float height) {
			this.x = (new Random()).Next((int)this.r * 2, (int)(width - this.r * 2));
			this.y = (new Random()).Next((int)this.r * 2, (int)(height - this.r * 2));
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
				new Font("Verdana", 8), // шрифт и размер
				new SolidBrush(Color.Green), // цвет шрифта
				this.x + this.r, this.y + this.r // точка в которой нарисовать текст
			);
		}
	}
}
