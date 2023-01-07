using EventHandling.Objects;
using System.Drawing.Drawing2D;
using System.Media;
using System.Windows.Forms;

namespace EventHandling {
	public partial class Form1 : Form {
		List<BaseObject> obj = new List<BaseObject>();
		
		Player player;
		Shadow shadow;
		Marker marker;

		int score = 0;
		
		public Form1() {
			InitializeComponent();

			player = new Player(pbMain.Width / 2, pbMain.Height / 2, 20, 0);

			marker = new Marker(player.x, player.y, 20, 0);

			shadow = new Shadow(-pbMain.Width / 3, 0, pbMain.Width / 3, pbMain.Height, 0);

			obj.Add(new Obj(
				(new Random()).Next(20, pbMain.Width - 20), 
				(new Random()).Next(20, pbMain.Height - 20),
				20, 0)
			);
			obj.Add(new Obj(
				(new Random()).Next(20, pbMain.Width - 20),
				(new Random()).Next(20, pbMain.Height - 20),
				20, 0)
			);
		}

		private void pbMain_Paint(object sender, PaintEventArgs e) {
			Graphics g = e.Graphics;
			g.Clear(Color.White);

			if (shadow.x > pbMain.Width) {
				shadow.x = -shadow.width;
			}

			shadow.x += 2;

			shadow.Render(g);

			for (int i = 0; i < obj.Count; i++) {
				if (player.OverlapsCircle(obj[i])) {
					obj[i].SetPos(
						(new Random()).Next((int)obj[i].originR, (int)(pbMain.Width - obj[i].originR)), 
						(new Random()).Next((int)obj[i].originR, (int)(pbMain.Height - obj[i].originR))
					);
					obj[i].r = obj[i].originR;
					if (player.inShadow) {
						score += 2;
					} else {
						score++;
					}
					label1.Text = $"Очки: {score}";
					richTextBox1.Text += $"[{DateTime.Now:HH:mm:ss:ff}]\nИгрок пересекся с {obj[i]}\n";
				}
				if (player.bullet != null) {
					if (player.bullet.OverlapsCircle(obj[i])) {
						obj[i].SetPos(
							(new Random()).Next((int)obj[i].originR, (int)(pbMain.Width - obj[i].originR)),
							(new Random()).Next((int)obj[i].originR, (int)(pbMain.Height - obj[i].originR))
						);
						obj[i].r = obj[i].originR;
						if (player.inShadow) {
							score += 2;
						} else {
							score++;
						}
						label1.Text = $"Очки: {score}";
						richTextBox1.Text += $"[{DateTime.Now:HH:mm:ss:ff}]\nПуля пересеклась с {obj[i]}\n";
					}
				}

				obj[i].r -= 0.15f;
				if (obj[i].r < 0) {
					obj[i].SetPos(
						(new Random()).Next((int)obj[i].originR, (int)(pbMain.Width - obj[i].originR)),
						(new Random()).Next((int)obj[i].originR, (int)(pbMain.Height - obj[i].originR))
					);
					obj[i].r = obj[i].originR;
					richTextBox1.Text += $"[{DateTime.Now:HH:mm:ss:ff}]\n{obj[i]} обновился\n";
				}
				obj[i].inShadow = shadow.OverlapsRect(obj[i]);
				obj[i].Render(g);
			}

			UpdatePlayer();

			if (marker.isExist) {
				g.Transform = marker.GetTransform();
				//player.bullet.inShadow = shadow.OverlapsRect(player.bullet);
				marker.Render(g);
			}

			g.Transform = player.GetTransform();
			player.inShadow = shadow.OverlapsRect(player);
			player.Render(g);

			//richTextBox1.Text = $"Координаты игрока: [{Math.Round(player.x, 2)}; {Math.Round(player.y, 2)}]\n";

			if (player.bullet != null) {
				g.Transform = player.bullet.GetTransform();
				richTextBox1.Text = $"Игрок\nangle: {player.angle}\nПуля\ndx: {player.bullet.dx}, dy: {player.bullet.dy}";
				player.bullet.Render(g);
			}

			//richTextBox1.Text = $"Игрок\nangle: {player.angle}\ndx: {player.dx}, dy: {player.dy}";
		}

		private void pbMain_MouseClick(object sender, MouseEventArgs e) {
			marker.isExist = true;
			marker.SetPos(e.X, e.Y);
		}

		private void UpdatePlayer() {
			player.dx = marker.x - player.x;
			player.dy = marker.y - player.y;

			float length = MathF.Sqrt(player.dx * player.dx + player.dy * player.dy);

			if (length > marker.r / 2) {
				player.dx /= length;
				player.dy /= length;

				player.vX += player.dx * 0.5f;
				player.vY += player.dy * 0.5f;

				player.angle = 90 - MathF.Atan2(player.vX, player.vY) * 180 / MathF.PI;
			} else {
				//richTextBox1.Text += $"[{DateTime.Now:HH:mm:ss:ff}]\nИгрок пересекся с {marker}\n";
				marker.isExist = false;
				marker.x = player.x;
				marker.y = player.y;
				player.dx = 0;
				player.dy = 0;
			}

			if (player.bullet != null) {
				player.bullet.x += player.bullet.dx * 15f;
				player.bullet.y += player.bullet.dy * 15f;
			}

			player.vX += -player.vX * 0.1f;
			player.vY += -player.vY * 0.1f;

			player.x += player.vX;
			player.y += player.vY;

			if (player.x - player.r < 0) {
				player.x = player.r;
			} else if (player.x + player.r > pbMain.Width) {
				player.x = pbMain.Width - player.r;
			}

			if (player.y - player.r < 0) {
				player.y = player.r;
			} else if (player.y + player.r > pbMain.Height) {
				player.y = pbMain.Height - player.r;
			}
		}

		private void timer1_Tick_1(object sender, EventArgs e) {
			pbMain.Invalidate();
		}

		private void Form1_KeyDown(object sender, KeyEventArgs e) {
			if (e.KeyCode == Keys.Space) {
				player.Shoot();
				e.SuppressKeyPress = true;
				SoundPlayer soundPlayer = new SoundPlayer("shoot.wav");
				soundPlayer.Play();
			}
		}
	}
}