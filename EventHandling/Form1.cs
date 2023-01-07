using EventHandling.Objects;
using System.Media;

namespace EventHandling {
	public partial class Form1 : Form {
		List<Obj> obj = new List<Obj>();
		
		Player player;
		Shadow shadow;
		Marker marker;

		static public int score = 0;
		
		public Form1() {
			InitializeComponent();

			player = new Player(pbMain.Width / 2, pbMain.Height / 2, 20, 0);
			marker = new Marker(player.x, player.y, 20, 0);
			shadow = new Shadow(-pbMain.Width / 3, 0, pbMain.Width / 3, pbMain.Height, 0);

			for (int i = 0; i < 2; i++) {
				obj.Add(new Obj(
					new Random().Next(20, pbMain.Width - 20),
					new Random().Next(20, pbMain.Height - 20),
					20, 0)
				);
			}
		}

		private void pbMain_Paint(object sender, PaintEventArgs e) {
			Graphics g = e.Graphics;
			g.Clear(Color.White);

			shadow.Update(pbMain);
			shadow.Render(g);

			ObjRender(g);

			if (marker.isExist) {
				g.Transform = marker.GetTransform();
				marker.Render(g);
			}

			g.Transform = player.GetTransform();
			player.Update(marker, shadow, pbMain);
			player.Render(g);

			if (player.bullets != null) {
				for (int i = 0; i < player.bullets.Count; i++) {
					g.Transform = player.bullets[i].GetTransform();
					player.bullets[i].Render(g);
				}
			}
		}

		private void ObjRender(Graphics g) {
			for (int i = 0; i < obj.Count; i++) {
				obj[i].Update(player, pbMain, label1, richTextBox1);
				obj[i].inShadow = shadow.OverlapsRect(obj[i]);
				obj[i].Render(g);
			}
		}

		private void pbMain_MouseClick(object sender, MouseEventArgs e) {
			if (e.Button == MouseButtons.Left) {
				marker.SetPos(e.X, e.Y);
			}
		}

		private void timer1_Tick_1(object sender, EventArgs e) {
			pbMain.Invalidate();
		}

		private void Form1_KeyDown(object sender, KeyEventArgs e) {
			if (e.KeyCode == Keys.Space) {
				player.Shoot();
				e.SuppressKeyPress = true;
			}
		}
	}
}